using System;
using Frame.Tool;
using UnityEngine;

namespace LevelEditor
{
    public class EditorManager : MonoBehaviour
    {
        private EditorController m_editorController;
        private CommandManager m_commandManager;
        
        void Start()
        {
            m_commandManager = new CommandManager();
            m_editorController = new EditorController(transform as RectTransform,m_commandManager.CommandSet);
        }
    
        private void LateUpdate()
        {
            m_editorController.LateUpdate();
        }

        private void Update()
        {
            //TODO:目前与输入框互动时Redo和Undo会有BUG，出于架构考虑，暂时在想解决办法，在想用不用全局事件
            bool zButtonDown = InputManager.Instance.GetZButtonDown;
            // if (zButtonDown && InputManager.Instance.GetCtrlButton && InputManager.Instance.GetShiftButton)
            if(InputManager.Instance.GetDebuggerNum2Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandManager.CommandSet.GetRedo?.Invoke();
            // }else if (zButtonDown && InputManager.Instance.GetCtrlButton)
            }else if(InputManager.Instance.GetDebuggerNum1Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandManager.CommandSet.GetUndo?.Invoke();
            }
        }

        private void OnEnable()
        {
            if(m_commandManager != null) m_commandManager.CommandSet.EnableExcute?.Invoke();
        }
    }
}
