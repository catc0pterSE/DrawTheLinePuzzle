using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using UI.Level;
using UnityEngine;
using Utility.Extensions;
using Utility.Static;

namespace Scene
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField] private WinWindow _winWindow;
        [SerializeField] private LostWindow _lostWindow;
        
        private int _levelNumber;
        private GameStateMachine _gameStateMachine;

        public void Initialize(GameStateMachine gameStateMachine, int levelNumber)
        {
            HideWindows();
            _gameStateMachine = gameStateMachine;
            _levelNumber = levelNumber;
            _gameStateMachine = gameStateMachine;
            SubscribeOnButtons();
        }

        public void ShowWinWindow()
        {
            _winWindow.EnableObject();
            
            if (_levelNumber == NumericConstants.TotalLevelsNumber)
                _winWindow.HideNextLLevelButton();
        }
        
        public void ShowLostWindow() => _lostWindow.EnableObject();
        
        private void HideWindows()
        {
            _winWindow.DisableObject();
            _lostWindow.DisableObject();
        }

        private void SubscribeOnButtons()
        {
            if (_levelNumber < NumericConstants.TotalLevelsNumber)
                _winWindow.NextLevelButtonPressed += () => EnterLoadLevelState(_levelNumber + 1);

            _winWindow.MenuButtonPressed += EnterLoadMainMenuState;
            _lostWindow.MenuButtonPressed += EnterLoadMainMenuState;
            _lostWindow.ReplayButtonPressed += () => EnterLoadLevelState(_levelNumber);
        }

        private void EnterLoadMainMenuState() =>
            _gameStateMachine.Enter<LoadMainMenuState>();

        private void EnterLoadLevelState(int levelNumber) =>
            _gameStateMachine.Enter<LoadLevelState, int>(levelNumber);
    }
}