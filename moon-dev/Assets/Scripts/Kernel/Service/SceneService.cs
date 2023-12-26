using System;
using System.Collections.Generic;
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
    public sealed class SceneService : Service, IService
    {
        /// <summary>
        ///     Get the currently active scene
        /// </summary>
        public Scene ActiveScene => m_activeScene;

        /// <inheritdoc />
        public bool IsRunning => m_isInstanced && m_isActive;

        /// <summary>
        ///     Get all current scenes
        /// </summary>
        public List<Scene> CurrentScenes { get; } = new();

        /// <summary>
        /// </summary>
        public const string PersistenceSceneName = "Persistent";

        private readonly bool m_isInstanced;

        private readonly bool m_isActive;

        private Scene m_activeScene;


        #region public API

        /// <summary>
        ///     Unload tag scene and load next scene asynchronously
        /// </summary>
        /// <remarks>Use Forget method to return void</remarks>
        /// <param name="loadName">scene name to load</param>
        /// <param name="postAction">method to do after switch</param>
        public async UniTask TransitionActiveScene(string loadName, Action postAction)
        {
            var unloadName = m_activeScene.name;

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

        #region internal API

        /// <inheritdoc />
        internal async override Task Run()
        {
            await Initialization();
            await Task.Run(OnStart);
        }

        /// <inheritdoc />
        internal async override Task Abort()
        {
            await Task.Run(OnStop);
        }

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

        #endregion


        private void SetupActiveScene()
        {
            //TODO:use profiles
        }

        private async UniTask Initialization()
        {
            //Collection names of scene

            var persistenceScene = SceneManager.GetSceneByName(PersistenceSceneName);

            if (!persistenceScene.IsValid())
            {
                await SceneManager.LoadSceneAsync(PersistenceSceneName, LoadSceneMode.Additive);
                CurrentScenes.Add(SceneManager.GetSceneByName(PersistenceSceneName));
                persistenceScene = SceneManager.GetSceneByName(PersistenceSceneName);
            }

            SetupActiveScene();

            SceneManager.activeSceneChanged += OnActiveSceneChange;
            SceneManager.sceneUnloaded += OnSceneUnload;
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
        {
            switch (loadSceneMode)
            {
                case LoadSceneMode.Single:
                    CurrentScenes.Clear();
                    CurrentScenes.Add(scene);
                    break;
                case LoadSceneMode.Additive:
                    CurrentScenes.Add(scene);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(loadSceneMode), loadSceneMode, null);
            }
        }

        private void OnSceneUnload(Scene scene)
        {
            CurrentScenes.Remove(scene);
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