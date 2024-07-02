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
    ///     This service is used for scene management, including loading and unloading
    /// </summary>
    [UsedImplicitly, SystemService]
    public sealed class SceneService : Service
    {
        /// <summary>
        ///     Get the currently active scene
        /// </summary>
        [UsedImplicitly]
        public Scene ActiveScene => SceneManager.GetActiveScene();

        /// <summary>
        ///     Get all current scenes
        /// </summary>
        [UsedImplicitly]
        public List<Scene> CurrentScenes { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        [UsedImplicitly]
        public Scene PersistenceScene { get; private set; }

        /// <summary>
        /// </summary>
        public const string PersistenceSceneName = "Persistent";

        /// <summary>
        ///     Unload tag scene and load next scene asynchronously
        /// </summary>
        /// <remarks>Use Forget method to return void</remarks>
        /// <param name="loadName">scene name to load</param>
        [UsedImplicitly]
        public async UniTask TransitionActiveScene(string loadName)
        {
            var unloadName = ActiveScene.name;

            try
            {
                await SceneManager.LoadSceneAsync(loadName, LoadSceneMode.Additive);
                await SceneManager.UnloadSceneAsync(unloadName);
            }
            catch (Exception e)
            {
                throw new Exception();
            }

            //select the newest scene and set it active
            var targetScene = SceneManager.GetSceneByName(loadName);
            SceneManager.SetActiveScene(targetScene);
        }

        /// <summary>
        ///     Try to load a scene
        /// </summary>
        /// <param name="name">The name of the scene</param>
        /// <returns>If it loads successfully, the scene will be returned</returns>
        /// <exception cref="Exception">An exception is thrown when the scene loads incorrectly</exception>
        [UsedImplicitly]
        public async UniTask<Scene> TryLoadScene(string name)
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
            }

            return target;
        }

        /// <summary>
        ///     Try to load a scene and set it active
        /// </summary>
        /// <param name="name">The name of the scene</param>
        /// <returns>If it loads successfully, the scene will be returned</returns>
        /// <exception cref="Exception">An exception is thrown when the scene loads incorrectly</exception>
        [UsedImplicitly]
        public async UniTask<Scene> TryLoadActiveScene(string name)
        {
            var target = await TryLoadScene(name);
            SceneManager.SetActiveScene(target);
            return target;
        }

        ~SceneService()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(disposing: false) is optimal in terms of
            // readability and maintainability.
            Dispose(disposing: false);
        }

        internal override async UniTask Run()
        {
            PersistenceScene = await TryLoadScene(PersistenceSceneName);
/*
            if (Explorer.Settings.MoonSetting.AutoStartScene)
            {
                await TryLoadScene(Explorer.Settings.MoonSetting.startScene);
            }
*/
            SceneManager.activeSceneChanged += OnActiveSceneChange;
            SceneManager.sceneUnloaded      += OnSceneUnload;
            SceneManager.sceneLoaded        += OnSceneLoad;
        }

        internal override async Task Abort()
        {
            await Task.Run(OnStop);
        }

        internal override void OnStart()
        {
        }

        internal override void OnStop()
        {
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
        }
    }
}