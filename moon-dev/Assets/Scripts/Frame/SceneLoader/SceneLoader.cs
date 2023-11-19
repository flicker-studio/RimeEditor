using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Frame.Tool
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        public async UniTask EnterScene(string sceneName)
        {
            await LoadScene(sceneName);
        }

        public async UniTask AddScene(string sceneName)
        {
            SetCurrentSceneObjectsActive(false);
            await LoadScene(sceneName,LoadSceneMode.Additive);
        }

        public async UniTask RemoveCurrentScene()
        {
            Scene currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            
            await SceneManager.UnloadSceneAsync(currentScene);
            
            SetCurrentSceneObjectsActive(true);
        }
        
        private void SetCurrentSceneObjectsActive(bool value)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.isLoaded)
            {
                foreach (GameObject go in scene.GetRootGameObjects())
                {
                    go.SetActive(value);
                }
            }
        }
        
        private async UniTask LoadScene(string sceneName,LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            await SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }
    }
}
