using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kernel.Scene
{
    public class SceneLoader : ISceneLoader
    {
        public void Load(string sceneName, Action loadingCompleted)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
            loading.completed += _ => loadingCompleted?.Invoke();
        }
    }
}