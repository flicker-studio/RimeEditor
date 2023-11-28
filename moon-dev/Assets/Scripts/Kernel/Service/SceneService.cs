using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Moon.Kernel.Service
{
    /// <summary>
    ///     Use this class to switch scenes
    /// </summary>
    /// <inheritdoc cref="Moon.Kernel.Service.IService" />
    [UsedImplicitly]
    public sealed class SceneService : ServiceBase, IService
    {
        /// <summary>
        ///     Get the currently active scene
        /// </summary>
        public Scene ActiveScene => m_activeScene;

        private readonly bool m_isInstanced;

        private Scene m_activeScene;

        private readonly List<string> m_sceneNameList = new();


        #region API

        /// <inheritdoc />
        public async Task Run()
        {
            //  var uniTasks = new List<UniTask> { VariableInitialization(), Fun() };

            await VariableInitialization();
            await Task.Run(OnStart);
        }

        /// <inheritdoc />
        public Task Abort()
        {
            return Task.Run(OnStop);
        }


        /// <summary>
        ///     Unload tag scene and load next scene asynchronously
        /// </summary>
        /// <remarks>Use Forget method to return void</remarks>
        /// <param name="loadName">scene name to load</param>
        /// <param name="postAction">method to do after switch</param>
        /// <param name="unloadName">scene name to unload, default is the active scene</param>
        /// <exception cref="Exception"> </exception>
        public async UniTask TransitionScene(string loadName, Action postAction, string unloadName = null)
        {
            unloadName ??= m_activeScene.name;

            if (!m_sceneNameList.Contains(loadName))
            {
                throw new Exception($"{loadName} is not exist!");
            }

            //asynchronous loading and unloading
            var tasks = new List<UniTask>
            {
                SceneManager.LoadSceneAsync(loadName, LoadSceneMode.Additive).ToUniTask(),
                SceneManager.UnloadSceneAsync(unloadName).ToUniTask()
            };

            await UniTask.WhenAll(tasks);

            //select the newest scene and set it active
            var targetScene = SceneManager.GetSceneByName(loadName);
            SceneManager.SetActiveScene(targetScene);
            postAction?.Invoke();
        }

        #endregion

        /// <inheritdoc />
        protected override void OnStart()
        {
        }

        /// <inheritdoc />
        protected override void OnStop()
        {
            Dispose(true);
        }

        /// <inheritdoc />
        protected override void Dispose(bool all)
        {
            throw new NotImplementedException();
        }

        private async UniTask VariableInitialization()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenePath = new string[sceneCount];

            for (var i = 0; i < sceneCount; i++) scenePath[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            m_sceneNameList.AddRange(scenePath.Select(path => path[(path.LastIndexOf('/') + 1)..]));

            var persistenceScene = SceneManager.GetSceneByName(Boot.PersistenceSceneName);

            if (!persistenceScene.IsValid())
            {
                await SceneManager.LoadSceneAsync(Boot.PersistenceSceneName, LoadSceneMode.Single).ToUniTask();
            }

            var startSceneName = m_sceneNameList[1];
            var startScene = SceneManager.GetSceneByName(startSceneName);

            if (!startScene.IsValid())
            {
                await SceneManager.LoadSceneAsync(startSceneName, LoadSceneMode.Additive);
                startScene = SceneManager.GetSceneByName(startSceneName);
            }

            SceneManager.SetActiveScene(startScene);

            await Fun();
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
            {
                m_activeScene = next;
            }
            else
            {
                throw new Exception($"{current} scene mismatch!");
            }
        }
    }
}