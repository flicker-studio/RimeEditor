using Frame.Tool;
using UnityEngine;

namespace LevelEditor
{
    //TODO: rename
    public class EditorManager : MonoBehaviour
    {
        private EditorController m_editorController;

        private CommandInvoker m_commandInvoker;

        void Start()
        {
            m_commandInvoker = new CommandInvoker();
            m_editorController = new EditorController(transform as RectTransform, m_commandInvoker.CommandSet);
        }

        private void LateUpdate()
        {
            m_editorController.LateUpdate();
        }

        private void Update()
        {
            //TODO:目前与输入框互动时Redo和Undo会有BUG，出于架构考虑，暂时在想解决办法，在想用不用全局事件
            bool zButtonDown = InputManager.Instance.GetZButtonDown;

            if (InputManager.Instance.GetCtrlButton && InputManager.Instance.GetShiftButton && zButtonDown)

                // if(InputManager.Instance.GetDebuggerNum2Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandInvoker.CommandSet.GetRedo?.Invoke();
            }
            else if (InputManager.Instance.GetCtrlButton && zButtonDown)

                // }else if(InputManager.Instance.GetDebuggerNum1Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandInvoker.CommandSet.GetUndo?.Invoke();
            }
        }

        private void OnEnable()
        {
            if (m_commandInvoker != null) m_commandInvoker.CommandSet.EnableExcute?.Invoke();
        }
    }
}