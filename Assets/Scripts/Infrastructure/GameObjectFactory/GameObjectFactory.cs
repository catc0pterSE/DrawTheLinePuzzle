using System;
using Configs;
using Infrastructure.AssetProvider;
using Scene;
using UI.MainMenu;
using UnityEngine;
using Utility.Static.StringNames;

namespace Infrastructure.GameObjectFactory
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameObjectFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public LevelButton CreateLevelButton(Transform parent = null) =>
            GameObject.Instantiate(_assetProvider.Provide<LevelButton>(ResourcePaths.LevelButtonPath), parent);


        public Player CreatePlayer(Vector3 at) =>
            GameObject.Instantiate(_assetProvider.Provide<Player>(ResourcePaths.Player), at, Quaternion.identity);
    }
}