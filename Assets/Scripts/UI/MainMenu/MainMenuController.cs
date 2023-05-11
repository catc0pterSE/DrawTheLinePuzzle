using Data;
using Infrastructure.GameObjectFactory;
using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using Infrastructure.Progression;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Utility.Static;

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private LayoutGroup _layoutGroup;
        [SerializeField] private Button _quitButton;

        private LevelButton[] _levelButtons;
        private GameStateMachine _gameStateMachine;
        private LevelsCompleteData _levelsCompleteData;
        private IGameObjectFactory _gameObjectFactory;

        private void OnEnable() => _quitButton.onClick.AddListener(QuitGame);

        private void OnDisable() => _quitButton.onClick.RemoveListener(QuitGame);

        private void QuitGame()
        {
            Application.Quit();
        }

        public void Initialize(GameStateMachine gameStateMachine, Services services)
        {
            _gameObjectFactory = services.Single<IGameObjectFactory>();
            _gameStateMachine = gameStateMachine;
            _levelsCompleteData = services.Single<IProgressionService>().LevelsCompleteData;
            InitializeLevelButtons();
        }

        private void InitializeLevelButtons()
        {
            _levelButtons = new LevelButton[NumericConstants.TotalLevelsNumber];

            for (int i = 0; i < _levelButtons.Length; i++)
            {
                int levelNumber = i + 1;
                bool isComplete = _levelsCompleteData.Get(levelNumber);
                _levelButtons[i] = _gameObjectFactory.CreateLevelButton(_layoutGroup.transform);
                _levelButtons[i].Initialize(levelNumber, isComplete);
                _levelButtons[i].ButtonClocked += () => LoadLevel(levelNumber);
            }
        }

        private void LoadLevel(int levelNumber) =>
            _gameStateMachine.Enter<LoadLevelState, int>(levelNumber);
    }
}