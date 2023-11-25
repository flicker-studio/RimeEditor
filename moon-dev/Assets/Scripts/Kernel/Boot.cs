using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moon.Kernel.Service;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moon.Kernel
{
    /// <summary>
    ///     The population class of the entire game system is responsible for initialization at the lowest level.
    /// </summary>
    internal static class Boot
    {
        private const string PersistenceSceneName = "Persistent";
        internal static readonly List<IService> SystemServices = new();

        /// <summary>
        ///     Callback invoked when all assemblies are loaded and preloaded assets are initialized.
        ///     At this time the objects of the first scene have not been loaded yet.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        private static async void BootLoader()
        {
            Debug.Log("<color=green>[SYS]</color> System is Booting...");

            Debug.Log("<color=green>[SERVICE]</color> Initializing service...");

            await InstanceService();
            await Task.WhenAll(StartServiceAsync());

            Debug.Log("<color=green>[SERVICE]</color> Initialization is complete.");
        }


        /// <summary>
        ///     Instantiate all service classes and initialize them.
        /// </summary>
        private static IEnumerable<Task> StartServiceAsync()
        {
            return SystemServices.Select(service => service.StartTask());
        }

        private static Task InstanceService()
        {
            var task = Task.Run(() =>
                {
                    var serviceAssembly = typeof(IService).Assembly;
                    var assemblyTypes = serviceAssembly.GetTypes();
                    var serviceTypes = assemblyTypes.Where(type => type.GetInterfaces().Contains(typeof(IService))).ToArray();

                    foreach (var service in serviceTypes)
                    {
                        var typeName = service.ToString();

                        if (serviceAssembly.CreateInstance(typeName) is not IService instance)
                            throw new WarningException($"There has some error in {service} class");

                        SystemServices.Add(instance);
                    }
                }
            );
            return task;
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