using Frame.Tool;
using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Scene entrance
    /// </summary>
    internal class Controller : Singleton<Controller>
    {
        /// <summary>
        ///     Information Center, a large number of configuration files are included.
        /// </summary>
        internal static readonly Information Configure = new();
        
        /// <summary>
        /// The current root object, which is the parent of all objects in the scene.
        /// </summary>
        internal static GameObject RootObject;
        
        /// <summary>
        ///     A Mono behavior that is unique to this scene.
        /// </summary>
        internal static Behaviour Behaviour;
        
        /// <summary>
        ///     Preload setting files
        /// </summary>
        public async void AssetsLoaderAsync()
        {
            await Explorer.BootCompletionTask;
            RootObject = GameObject.FindGameObjectWithTag("Temp_Editor");
            
            await Configure.Init();
            Behaviour         = RootObject.AddComponent<Behaviour>();
            Behaviour.enabled = true;
        }
    }
}