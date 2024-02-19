using Frame.StateMachine;
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

        private          Information _information;
        private readonly Context     _context = new();
        private          IState      _browseState;

        private void OnEnable()
        {
            input                         =  FindObjectOfType<PlayerInput>();
            input.actions["Redo"].started += Test;
        }

        private void Awake()
        {
            _information = Controller.Information;
            _information.EnableExcute();
            var levelEditorTransform = transform as RectTransform;

            _browseState = new BrowseState
                (
                 _information,
                 null,
                 levelEditorTransform,
                 _information.UI
                );

            _browseState.Handle(_context);
        }

        private void Test(InputAction.CallbackContext callbackContext)
        {
            Debug.Log(callbackContext.startTime);
        }

        private void Update()
        {
            _context.Update?.Invoke();
        }

        private void OnDestroy()
        {
        }
    }
}