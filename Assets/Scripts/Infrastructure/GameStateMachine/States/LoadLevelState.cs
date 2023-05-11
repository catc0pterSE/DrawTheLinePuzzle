using System;
using Gameplay.Scene;
using Infrastructure.AssetProvider;
using Infrastructure.GameObjectFactory;
using Infrastructure.Progression;
using Infrastructure.SceneManagement;
using Modules.StateMachine;
using UnityEngine;
using Utility.Static.StringNames;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IParameterState<int>
    {
        private readonly Services.Services _services;
        private readonly GameStateMachine _gameStateMachine;

        private int _levelNumber;
        private SceneLoader _sceneLoader;

        public LoadLevelState(SceneLoader sceneLoader, Services.Services services, GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _services = services;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(int levelNumber)
        {
            _levelNumber = levelNumber;
            _sceneLoader.LoadScene(String.Format(SceneNames.Level, levelNumber), OnSceneLoaded);
        }

        public void Exit()
        {
        }

        private void OnSceneLoaded()
        {
            WindowsManager windowsManager = GameObject.FindObjectOfType<WindowsManager>();
            windowsManager.Initialize(_gameStateMachine, _levelNumber);
            
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            levelManager.Initialize
            (
                _levelNumber, _services.Single<IGameObjectFactory>(),
                _services.Single<IAssetProvider>(),
                _services.Single<IProgressionService>()
            );
        }
    }
}