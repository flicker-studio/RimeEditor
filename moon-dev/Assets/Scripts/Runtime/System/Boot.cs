using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moon.Runtime.System
{
    /// <summary>
    ///     The population class of the entire game system is responsible for initialization at the lowest level.
    /// </summary>
    public static class Boot
    {
        private const string PersistenceSceneName = "Persistent";

        /// <summary>
        ///     Callback invoked when all assemblies are loaded and preloaded assets are initialized.
        ///     At this time the objects of the first scene have not been loaded yet.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static async void BootLoader()
        {
            Debug.Log("<color=green>[SYS]</color> System is Booting...");
            await StartServiceAsync();
        }


        /// <summary>
        ///     Instantiate all service classes and initialize them.
        /// </summary>
        private static async Task StartServiceAsync()
        {
            Debug.Log("<color=green>[SERVICE]</color> Initializing service...");

            await Task.CompletedTask;

            Debug.Log("<color=green>[SERVICE]</color> Initialization is complete.");
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void PreCheck()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenePath = new string[sceneCount];

            for (var i = 0; i < sceneCount; i++) scenePath[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            var sceneName = scenePath.Select(path => path[(path.LastIndexOf('/') + 1)..]).ToArray();

            if (!sceneName.Contains(PersistenceSceneName))
                throw new Exception($"{PersistenceSceneName} scene is lose!");
        }
#endif
    }
}