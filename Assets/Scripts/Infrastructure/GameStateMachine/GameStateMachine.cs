using System;
using System.Collections.Generic;
using Infrastructure.EntryPoint;
using Infrastructure.GameStateMachine.States;
using Infrastructure.SaveLoad;
using Infrastructure.SceneManagement;
using Modules.StateMachine;
using UnityEngine;

namespace Infrastructure.GameStateMachine
{
    public class GameStateMachine : Modules.StateMachine.StateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, Services.Services services, IApplicationQuitHandler applicationQuitHandler)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services) ,
                [typeof(LoadMainMenuState)] = new LoadMainMenuState(sceneLoader, this, services),
                [typeof(MenuState)] = new MenuState(),
                [typeof(LoadLevelState)] = new LoadLevelState(sceneLoader, services, this),
                [typeof(GameplayLoopState)] = new GameplayLoopState(),
                [typeof(CloseGameState)] = new CloseGameState(services)
             };

            applicationQuitHandler.ApplicationQuit += Enter<CloseGameState>;
        }
    }
}