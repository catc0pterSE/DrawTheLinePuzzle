﻿using System;
using Cysharp.Threading.Tasks;
using UI.Loading;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        private readonly LoadingCurtain _loadingCurtain;

        public SceneLoader(LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }
        
        public async void LoadScene(string name, Action onLoaded = null)
        {
            await _loadingCurtain.FadeInAsync();
            await SceneManager.LoadSceneAsync(name);
            onLoaded?.Invoke();
            _loadingCurtain.FadeOut();
        }
    }
}