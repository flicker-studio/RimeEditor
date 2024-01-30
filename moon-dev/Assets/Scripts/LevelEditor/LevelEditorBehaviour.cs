using Frame.StateMachine;
using LevelEditor.Command;
using UnityEngine;

namespace LevelEditor
{
    //TODO: rename
    public class LevelEditorBehaviour : MonoBehaviour
    {
        private Information m_information;

        private LevelEditorController m_controller;

        private MotionController m_motionController;

        private void OnEnable()
        {
            m_controller = LevelEditorController.Instance;
            m_information = m_controller.Information;
            m_motionController = m_controller.MotionController;
            m_information.EnableExcute();
        }


        private void Update()
        {
            //TODO:目前与输入框互动时Redo和Undo会有BUG，出于架构考虑，暂时在想解决办法，在想用不用全局事件
            var zButtonDown = Frame.Tool.InputManager.Instance.GetZButtonDown;

            if (Frame.Tool.InputManager.Instance.GetCtrlButton && Frame.Tool.InputManager.Instance.GetShiftButton && zButtonDown)

                // if(InputManager.Instance.GetDebuggerNum2Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                CommandInvoker.Redo();
            }
            else if (Frame.Tool.InputManager.Instance.GetCtrlButton && zButtonDown)

                // }else if(InputManager.Instance.GetDebuggerNum1Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                CommandInvoker.Undo();
            }
        }

        private void LateUpdate()
        {
            m_motionController.Motion(m_information);
        }
    }
}