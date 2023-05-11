using Gameplay.Player;
using Infrastructure.Services;
using UI.MainMenu;
using UnityEngine;

namespace Infrastructure.GameObjectFactory
{
    public interface IGameObjectFactory : IService
    {
        public LevelButton CreateLevelButton(Transform parent);

        public Player CreatePlayer( Vector3 at);
    }
}