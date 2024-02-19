using Frame.StateMachine;
using Frame.Tool;
using LevelEditor.State;
using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    /// Scene entrance
    /// </summary>
    public class Controller : Singleton<Controller>
    {
        /// <summary>
        ///     Information Center
        /// </summary>
        public static readonly Information Information = new();

        public   MotionController MotionController;
        public   GameObject       RootObject;
        internal Behaviour        Behaviour;

        /// <summary>
        /// Preload setting files
        /// </summary>
        public async void AssetsLoaderAsync()
        {
            await Explorer.BootCompletionTask;

            RootObject        = GameObject.FindGameObjectWithTag("Temp_Editor");
            Behaviour         = RootObject.AddComponent<Behaviour>();
            Behaviour.enabled = true;
            RootObject.SetActive(true);
            await Information.Init();

            MotionController = new MotionController(Information);
            MotionController.ChangeMotionState(typeof(CameraDefultState));
            MotionController.ChangeMotionState(typeof(BrowseState));
        }
    }
}