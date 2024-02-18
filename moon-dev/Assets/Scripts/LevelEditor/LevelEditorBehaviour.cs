using Frame.StateMachine;
using LevelEditor.State;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = LevelEditor.State.Context;

namespace LevelEditor
{
    //TODO: rename
    public class LevelEditorBehaviour : MonoBehaviour
    {
        private Information m_information;

        private LevelEditorController m_controller;

        public PlayerInput input;

        private Context _context = new();

        private IState _browseState;

        private void OnEnable()
        {
            m_controller  = LevelEditorController.Instance;
            m_information = m_controller.Information;
            m_information.EnableExcute();

            input.actions["Redo"].started += Test;
            _browseState                  =  new BrowseState(m_information, null);
            _browseState.Handle(_context);
        }

        private void Test(InputAction.CallbackContext callbackContext)
        {
            Debug.Log(callbackContext.startTime);
        }
    }
}