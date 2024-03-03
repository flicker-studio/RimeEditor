using Frame.Tool;
using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    ///     Scene entrance
    /// </summary>
    internal class Controller : Singleton<Controller>
    {
        /// <summary>
        ///     Information Center
        /// </summary>
        internal static readonly Information Information = new();

        internal static GameObject RootObject;
        internal static Behaviour  Behaviour;

        /// <summary>
        ///     Preload setting files
        /// </summary>
        public async void AssetsLoaderAsync()
        {
            await Explorer.BootCompletionTask;
            RootObject = GameObject.FindGameObjectWithTag("Temp_Editor");

            await Information.Init();
            Behaviour         = RootObject.AddComponent<Behaviour>();
            Behaviour.enabled = true;
        }
    }
}