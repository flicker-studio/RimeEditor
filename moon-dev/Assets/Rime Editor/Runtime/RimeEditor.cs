using LevelEditor;
using LevelEditor.Command;
using LevelEditor.State;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = LevelEditor.State.Context;

namespace RimeEditor.Runtime
{
    /// <summary>
    ///     A Mono behavior that is unique to the editor scene
    /// </summary>
    internal class RimeEditor : MonoBehaviour
    {
        public static RimeEditor Instance;

        /// <summary>
        ///     Information Center, a large number of configuration files are included.
        /// </summary>
        internal static readonly Information Configure = new();

        public           PlayerInput        input;
        public           LevelEditorSetting LevelEditorSetting;
        private readonly Context            _context = new();
        private          IState             _browseState;
        private          IState             _editorState;

        private void Awake()
        {
            Instance = this;
            _        = Configure.Init(LevelEditorSetting);

            input                         =  FindObjectOfType<PlayerInput>();
            input.actions["Redo"].started += Test;
            input.actions["Undo"].started += Test2;

            _browseState = new BrowseState(transform as RectTransform);
            _editorState = new EditorState(Configure.UI);

            StateSwitch<BrowseState>();
        }

        private void Update()
        {
            _context.Update?.Invoke();
        }

        private void Test(InputAction.CallbackContext callbackContext)
        {
            CommandInvoker.Redo();
        }

        private void Test2(InputAction.CallbackContext callbackContext)
        {
            CommandInvoker.Undo();
        }

        internal void StateSwitch<T>() where T : IState
        {
            var type = typeof(T);
            if (type == typeof(BrowseState))
                _browseState.Handle(_context);
            else if (type == typeof(EditorState)) _editorState.Handle(_context);
        }
    }
}