using System;
using Configs;
using Infrastructure.AssetProvider;
using UnityEngine;
using Utility.Static.StringNames;

namespace Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private LineDrawer _lineDrawer;
        [SerializeField] private RouteFollower _routeFollower;

        public void Initialize(PlayerType type, IAssetProvider assetProvider)
        {
            Sprite sprite = assetProvider.Provide<PlayerTypeSpriteConfig>(ResourcePaths.PlayerSpriteConfigPath)
                .Get(type);
            _spriteRenderer.sprite = sprite;
            _lineDrawer.Initialize(type, assetProvider);
        }

        public bool IsRouteSet => _lineDrawer.IsRouteSet;
        public bool IsReached => _routeFollower.IsReached;

        public Vector3[] Route => _lineDrawer.GetRoute();

        public event Action Crashed;

        public event Action RouteSet
        {
            add => _lineDrawer.RouteSet += value;
            remove => _lineDrawer.RouteSet -= value;
        }
        
        public event Action Reached
        {
            add => _routeFollower.Reached += value;
            remove => _routeFollower.Reached -= value;
        }

        public void Move()
        {
            _routeFollower.StartMoving(_lineDrawer.GetRoute());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Player>(out _))
            {
                Crashed?.Invoke();
            }
        }
    }
}