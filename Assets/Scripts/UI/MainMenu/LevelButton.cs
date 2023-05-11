using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private Image _levelCompletePicture;

        public void Initialize(int levelNumber, bool isComplete)
        {
            SetText(levelNumber);
            SetComplete(isComplete);
        }

        public event UnityAction ButtonClocked
        {
            add => _button.onClick.AddListener(value);
            remove => _button.onClick.RemoveListener(value);
        }

        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void SetText(int levelNumber) => _buttonText.text = $"Level {levelNumber}";
        private void SetComplete(bool value) => _levelCompletePicture.gameObject.SetActive(value);
    }
}