using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    //TODO: rename
    public class LevelEditorBehaviour : MonoBehaviour
    {
        private Information m_information;

        private LevelEditorController m_controller;

        public PlayerInput input;

        private void OnEnable()
        {
            m_controller  = LevelEditorController.Instance;
            m_information = m_controller.Information;
            m_information.EnableExcute();

            input.actions["Redo"].started += Test;
        }

        private void Test(InputAction.CallbackContext callbackContext)
        {
            Debug.Log(callbackContext.startTime);
        }
    }
}