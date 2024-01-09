using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Moon.Kernel.Attribute;
using UnityEngine.SceneManagement;

namespace Moon.Kernel.Service
{
    /// <inheritdoc />
    /// <summary>
    ///   This service is used for scene management, including loading and unloading
    /// </summary>
    [UsedImplicitly, SystemService(typeof(SceneService))]
    public sealed class SceneService : Service
    {
        /// <summary>
        ///     Get the currently active scene
        /// </summary>
        public Scene ActiveScene => m_activeScene;

        /// <summary>
        ///     Get all current scenes
        /// </summary>
        [UsedImplicitly]
        public List<Scene> CurrentScenes { get; } = new();

        /// <summary>
        /// </summary>
        public const string PersistenceSceneName = "Persistent";

        private Scene m_activeScene;

        #region public API

        /// <summary>
        ///     Unload tag scene and load next scene asynchronously
        /// </summary>
        /// <remarks>Use Forget method to return void</remarks>
        /// <param name="loadName">scene name to load</param>
        public async UniTask TransitionActiveScene(string loadName)
        {
            var unloadName = m_activeScene.name;

            await SceneManager.LoadSceneAsync(loadName, LoadSceneMode.Additive);
            await SceneManager.UnloadSceneAsync(unloadName);

            //select the newest scene and set it active
            var targetScene = SceneManager.GetSceneByName(loadName);
            SceneManager.SetActiveScene(targetScene);
        }

        #endregion

        internal async override Task Run()
        {
            await TryLoadScene(PersistenceSceneName);
            await TryLoadScene(Explorer.Settings.MoonSetting.startScene);

            SceneManager.activeSceneChanged += OnActiveSceneChange;
            SceneManager.sceneUnloaded += OnSceneUnload;
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        internal async override Task Abort()
        {
            await Task.Run(OnStop);
        }

        internal override void OnStart()
        {
        }

        internal override void OnStop()
        {
        }

        internal override void Dispose(bool all)
        {
            throw new NotImplementedException();
        }

        private async UniTask TryLoadScene(string name)
        {
            var target = SceneManager.GetSceneByName(name);

            if (!target.IsValid())
            {
                try
                {
                    await SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
                    target = SceneManager.GetSceneByName(name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception($"Can't load target scene named {name}");
                }

                CurrentScenes.Add(SceneManager.GetSceneByName(name));
                SceneManager.SetActiveScene(target);
                m_activeScene = target;
            }
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