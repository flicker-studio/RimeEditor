using Frame.StateMachine;
using Frame.Tool;
using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    /// Scene entrance
    /// </summary>
    public class LevelEditorController : Singleton<LevelEditorController>
    {
        public readonly Information      Information = new();
        public          MotionController MotionController;
        public          GameObject       RootObject;

        /// <summary>
        /// Preload setting files
        /// </summary>
        public async void AssetsLoaderAsync()
        {
            await Explorer.BootCompletionTask;
            RootObject = GameObject.FindGameObjectWithTag("Temp_Editor").GetComponent<Collect>().target;
            await Information.Init();
            MotionController = new MotionController(Information);
            MotionController.ChangeMotionState(typeof(CameraDefultState));
            MotionController.ChangeMotionState(typeof(LevelManagerPanelShowState));
            RootObject.SetActive(true);
        }
    }
}