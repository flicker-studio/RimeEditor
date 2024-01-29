using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    //TODO: rename
    public class LevelEditorBehaviour : MonoBehaviour
    {
        private CommandInvoker m_commandInvoker;

        private Information m_information;

        private LevelEditorController m_controller;

        private MotionController m_motionController;

        private void OnEnable()
        {
            m_controller = LevelEditorController.Instance;
            m_commandInvoker = m_controller.CommandInvoker;
            m_information = m_controller.Information;
            m_motionController = m_controller.MotionController;

            if (m_commandInvoker != null)
            {
                m_commandInvoker.CommandSet.EnableExcute?.Invoke();
            }
        }


        private void Update()
        {
            //TODO:目前与输入框互动时Redo和Undo会有BUG，出于架构考虑，暂时在想解决办法，在想用不用全局事件
            var zButtonDown = Frame.Tool.InputManager.Instance.GetZButtonDown;

            if (Frame.Tool.InputManager.Instance.GetCtrlButton && Frame.Tool.InputManager.Instance.GetShiftButton && zButtonDown)

                // if(InputManager.Instance.GetDebuggerNum2Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandInvoker.CommandSet.GetRedo?.Invoke();
            }
            else if (Frame.Tool.InputManager.Instance.GetCtrlButton && zButtonDown)

                // }else if(InputManager.Instance.GetDebuggerNum1Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandInvoker.CommandSet.GetUndo?.Invoke();
            }
        }

        private void LateUpdate()
        {
            m_motionController.Motion(m_information);
        }
    }
}