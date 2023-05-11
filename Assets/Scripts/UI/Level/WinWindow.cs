using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility.Extensions;

namespace UI.Level
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _menButton;

        public void HideNextLLevelButton() => _nextLevelButton.DisableObject();

        public event UnityAction NextLevelButtonPressed
        {
            add => _nextLevelButton.onClick.AddListener(value);
            remove => _nextLevelButton.onClick.RemoveListener(value);
        }
        
        public event UnityAction MenuButtonPressed
        {
            add => _menButton.onClick.AddListener(value);
            remove => _menButton.onClick.RemoveListener(value);
        }

        private void OnDestroy()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
            _menButton.onClick.RemoveAllListeners();
        }
    }
}