using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Moon.Kernel.Service
{
    /// <inheritdoc />
    /// <summary>
    ///     Use this class to switch scenes
    /// </summary>
    public sealed class SceneService : IService
    {
        /// <summary>
        ///     Get the currently active scene
        /// </summary>
        public Scene ActiveScene => m_activeScene;

        private Scene m_activeScene;

        private readonly List<string> m_sceneNameList = new();


        /// <inheritdoc />
        public async Task StartTask()
        {
            var tasks = new List<UniTask> { VariableInitialization(), Fun() };

            await UniTask.WhenAll(tasks);
        }

        /// <inheritdoc />
        public async Task AbortTask()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        ///     Unload tag scene and load next scene asynchronously
        /// </summary>
        /// <remarks>Use Forget method to return void</remarks>
        /// <param name="loadName">scene name to load</param>
        /// <param name="postAction">method to do after switch</param>
        /// <param name="unloadName">scene name to unload, default is the active scene</param>
        /// <exception cref="Exception"> </exception>
        public async UniTask TransitionScene(string loadName, UnityAction postAction, string unloadName = null)
        {
            unloadName ??= m_activeScene.name;

            if (!m_sceneNameList.Contains(loadName)) throw new Exception($"{loadName} is not exist!");

            //asynchronous loading and unloading
            var tasks = new List<UniTask>
            {
                SceneManager.UnloadSceneAsync(unloadName).ToUniTask(),
                SceneManager.LoadSceneAsync(loadName, LoadSceneMode.Additive).ToUniTask()
            };

            await UniTask.WhenAll(tasks);

            //select the newest scene and set it active
            var targetScene = SceneManager.GetSceneByName(loadName);
            SceneManager.SetActiveScene(targetScene);
            postAction?.Invoke();
        }


        private async UniTask VariableInitialization()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenePath = new string[sceneCount];

            for (var i = 0; i < sceneCount; i++) scenePath[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            m_sceneNameList.AddRange(scenePath.Select(path => path[(path.LastIndexOf('/') + 1)..]));

            await UniTask.CompletedTask;
        }


        private async UniTask Fun()
        {
            m_activeScene = SceneManager.GetActiveScene();
            SceneManager.activeSceneChanged += OnActiveSceneChange;
            await UniTask.CompletedTask;
        }

        private void OnActiveSceneChange(Scene current, Scene next)
        {
            if (m_activeScene == current)
                m_activeScene = next;
            else
                throw new Exception($"{current} scene mismatch!");
        }
    }
}