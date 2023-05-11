using Infrastructure.SceneManagement;
using Modules.StateMachine;
using UI.Loading;
using UI.MainMenu;
using UnityEngine;
using Utility.Static.StringNames;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadMainMenuState: IParameterlessState
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly Services.Services _services;

        public LoadMainMenuState(SceneLoader sceneLoader, GameStateMachine gameStateMachine, Services.Services services)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _services = services;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(SceneNames.MainMenu, OnSceneLoaded);
        }

        public void Exit()
        {
            
        }
        
        private void OnSceneLoaded()
        {
           GameObject.FindObjectOfType<MainMenuController>().Initialize(_gameStateMachine, _services);
           _gameStateMachine.Enter<MenuState>();
        }
    }
}