using Frame.StateMachine;
using Frame.Tool;
using LevelEditor.State;
using Moon.Kernel;
using UnityEngine;
using Context = LevelEditor.State.Context;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    /// Scene entrance
    /// </summary>
    public class LevelEditorController : Singleton<LevelEditorController>
    {
        public static readonly Information Information = new();

        public MotionController MotionController;
        public GameObject       RootObject;

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
            MotionController.ChangeMotionState(typeof(BrowseState));
            RootObject.SetActive(true);
        }
    }
}