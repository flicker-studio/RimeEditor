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
            m_editorController = new EditorController(transform as RectTransform,m_commandManager.Excute);
        
        }
    
        private void LateUpdate()
        {
            m_editorController.LateUpdate();
        }

        private void Update()
        {
            //TODO:目前与输入框互动时Redo和Undo会有BUG，出于架构考虑，暂时在想解决办法，在想用不用全局事件
            bool zButtonDown = InputManager.Instance.GetZButtonDown;
            if (zButtonDown && InputManager.Instance.GetCtrlButton && InputManager.Instance.GetShiftButton)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandManager.Redo();
            }else if (zButtonDown && InputManager.Instance.GetCtrlButton)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandManager.Undo();
            }
        }
    }
}
