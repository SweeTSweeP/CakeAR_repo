using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Loaders.SceneLoader
{
    public class SceneLoader
    {
        public void Load(string name, Action onLoaded = null) =>
            LoadScene(name, onLoaded);
        
        private async void LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                
                return;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            await UniTask.WaitWhile(() => !waitNextScene.isDone);
            
            onLoaded?.Invoke();
        }
    }
}