using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Gameplay.Scene;
using Infrastructure.AssetProvider;
using UnityEngine;
using Utility.Static.StringNames;

namespace Gameplay.Player
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _drawFrequencySeconds = 0.1f;
        [SerializeField] private float _minimalDistance = 0.2f;

        private Coroutine _drawPathRoutine;
        private WaitForSeconds _waitForDrawDelay;
        private Camera _camera;
        private PlayerType _type;
        public bool IsRouteSet { get; private set; } = false;

        public void Initialize(PlayerType type, IAssetProvider assetProvider)
        {
            _type = type;
            Gradient gradient = assetProvider.Provide<PlayerTypeGradientConfig>(ResourcePaths.LineGradientConfigPath)
                .Get(type);
            _lineRenderer.colorGradient = gradient;
        }

        public event Action RouteSet;

        private void Awake()
        {
            _waitForDrawDelay = new WaitForSeconds(_drawFrequencySeconds);
            _camera = Camera.main;
        }
        
        private void OnMouseDown()
        {
            if (IsRouteSet)
                return;

            if (_drawPathRoutine != null)
                StopCoroutine(_drawPathRoutine);

            _drawPathRoutine = StartCoroutine(DrawPath());
        }
        
        private void OnMouseUp()
        {
            if (IsRouteSet)
                return;

            StopCoroutine(_drawPathRoutine);
            Collider2D col = Physics2D.OverlapPoint(GetPointerPosition());

            if (col != null && col.TryGetComponent(out EndPoint endPoint) && endPoint.Type.HasFlag(_type) &&
                endPoint.IsOccupied == false)
            {
                IsRouteSet = true;
                endPoint.Occupy();
                RouteSet?.Invoke();
            }
            else
            {
                _lineRenderer.positionCount = 0;
            }
        }

        public Vector3[] GetRoute()
        {
            Vector3[] route = new Vector3[_lineRenderer.positionCount];
            _lineRenderer.GetPositions(route);

            return route;
        }

        private IEnumerator DrawPath()
        {
            Vector3 currentPosition = transform.position;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(currentPosition);

            while (true)
            {
                Vector3 position = GetPointerPosition();

                if (Vector3.Distance(positions.LastOrDefault(), position) >= _minimalDistance)
                    positions.Add(position);

                _lineRenderer.positionCount = positions.Count;
                _lineRenderer.SetPositions(positions.ToArray());
                yield return _waitForDrawDelay;
            }
        }

        private Vector3 GetPointerPosition()
        {
            Vector3 rawPointerPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(rawPointerPosition.x, rawPointerPosition.y, transform.position.z);
        }
    }
}