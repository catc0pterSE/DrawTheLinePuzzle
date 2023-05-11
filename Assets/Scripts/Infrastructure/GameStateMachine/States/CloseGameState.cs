using Infrastructure.SaveLoad;
using Modules.StateMachine;
using UnityEngine;

namespace Infrastructure.GameStateMachine.States
{
    public class CloseGameState : IParameterlessState
    {
        private readonly Services.Services _services;

        public CloseGameState(Services.Services services)
        {
            _services = services;
        }

        public void Enter() 
        {
            _services.Single<ISaveLoadService>().SaveAll();
        }
        
        public void Exit() {}
    }
}