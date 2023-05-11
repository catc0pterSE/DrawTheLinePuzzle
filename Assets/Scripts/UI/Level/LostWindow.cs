using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Level
{
    public class LostWindow : MonoBehaviour
    {
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _menButton;

        public event UnityAction ReplayButtonPressed
        {
            add => _replayButton.onClick.AddListener(value);
            remove => _replayButton.onClick.RemoveListener(value);
        }
        
        public event UnityAction MenuButtonPressed
        {
            add => _menButton.onClick.AddListener(value);
            remove => _menButton.onClick.RemoveListener(value);
        }

        private void OnDisable()
        {
            _replayButton.onClick.RemoveAllListeners();
            _menButton.onClick.RemoveAllListeners();
        }
    }
}