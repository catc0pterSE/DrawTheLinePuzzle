using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Player
{
    public class RouteFollower: MonoBehaviour
    {
        [SerializeField] private float _travelTime = 5f;
        
        private Coroutine _movingRoutine;

        public bool IsReached { get; private set; }
            
        public event Action Reached;
        
        public void StartMoving(Vector3[] route)
        {
            if (_movingRoutine!=null)
                StopCoroutine(_movingRoutine);

            _movingRoutine = StartCoroutine(Move(route));
        }

        private IEnumerator Move(Vector3[] route)
        {
            WaitForSeconds delay = new WaitForSeconds(_travelTime / route.Length);

            foreach (var point in route)
            {
                transform.position = point;
                yield return delay;
            }

            IsReached = true;
            Reached?.Invoke();
        }
    }
}