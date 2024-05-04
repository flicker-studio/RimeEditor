using Frame.StateMachine;
using LevelEditor.Command;
using LevelEditor.State;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = LevelEditor.State.Context;

namespace LevelEditor
{
    /// <summary>
    ///     A Mono behavior that is unique to the current scene
    /// </summary>
    internal class Behaviour : MonoBehaviour
    {
        public PlayerInput input;
        
        private readonly Context _context = new();
        private          IState  _browseState;
        private          IState  _editorState;
        
        private void OnEnable()
        {
            input                         =  FindObjectOfType<PlayerInput>();
            input.actions["Redo"].started += Test;
            input.actions["Undo"].started += Test2;
            
            var information = Controller.Configure;
            
            _browseState = new BrowseState(transform as RectTransform);
            _editorState = new EditorState(information.UI);
            
            StateSwitch<BrowseState>();
        }
        
        private void Test(InputAction.CallbackContext callbackContext)
        {
            CommandInvoker.Redo();
        }
        
        private void Test2(InputAction.CallbackContext callbackContext)
        {
            CommandInvoker.Undo();
        }
        
        private void Update()
        {
            _context.Update?.Invoke();
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