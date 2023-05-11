using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Configs;
using Infrastructure.AssetProvider;
using Infrastructure.GameObjectFactory;
using Infrastructure.Progression;
using UnityEngine;
using Utility.Extensions;
using Utility.Static.StringNames;

namespace Scene
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private StartPoint[] _startPoints;
        [SerializeField] private EndPoint[] _endPoints;
        [SerializeField] private WindowsManager _windowsManager;

        private List<Player> _players = new List<Player>();
        private IGameObjectFactory _gameObjectFactory;
        private IAssetProvider _assetProvider;
        private int _levelNumber;
        private IProgressionService _progressionService;

        public void Initialize(int levelNumber, IGameObjectFactory gameObjectFactory, IAssetProvider assetProvider,
            IProgressionService progressionService)
        {
            _progressionService = progressionService;
            _levelNumber = levelNumber;
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            SpawnPlayers();
            SubscribeOnPlayers();
            InitializeEndPoints();
            Time.timeScale = 1;
        }

        private void Awake()
        {
            if (_startPoints.Length > _endPoints.Length)
                throw new WarningException("Every character should have end point");
        }

        private void OnDestroy()
        {
            UnsubscribeFromPlayers();
        }

        private void SpawnPlayers() =>
            _startPoints.Map(point =>
            {
                PlayerType type = point.Type;
                Player player = _gameObjectFactory.CreatePlayer(point.transform.position);
                player.Initialize(type, _assetProvider);
                _players.Add(player);
            });

        private void InitializeEndPoints()
        {
            _endPoints.Map(ennPoint =>
            {
                Sprite sprite = _assetProvider
                    .Provide<PlayerTypeSpriteConfig>(ResourcePaths.EndPointSpriteConfigPath).Get(ennPoint.Type);
                ennPoint.Initialize(sprite);
            });
        }

        private void SubscribeOnPlayers() =>
            _players.Map(player =>
            {
                player.RouteSet += OnRouteSet;
                player.Reached += OnReached;
                player.Crashed += OnCrash;
            });

        private void UnsubscribeFromPlayers() =>
            _players.Map(player =>
            {
                player.RouteSet -= OnRouteSet;
                player.Reached -= OnReached;
                player.Crashed -= OnCrash;
            });

        private void OnCrash()
        {
            Time.timeScale = 0;
            _windowsManager.ShowLostWindow();
        }

        private void OnReached()
        {
            if (_players.All(player => player.IsReached) == false)
                return;

            Time.timeScale = 0;
            _progressionService.LevelsCompleteData.Set(_levelNumber, true);
            _windowsManager.ShowWinWindow();
        }

        private void OnRouteSet()
        {
            if (_players.All(player => player.IsRouteSet) == false)
                return;

            _players.Map(player => player.Move());
        }
    }
}