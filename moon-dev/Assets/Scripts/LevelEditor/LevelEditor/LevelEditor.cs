using Frame.Tool;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    private LevelEditorController m_editorController;
    private LevelEditorCommandManager m_levelEditorCommandManager;
    void Start()
    {
        m_levelEditorCommandManager = new LevelEditorCommandManager();
        m_editorController = new LevelEditorController(transform as RectTransform,m_levelEditorCommandManager.Excute);
        
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
            m_levelEditorCommandManager.Redo();
        }else if (zButtonDown && InputManager.Instance.GetCtrlButton)
        {
            // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
            m_levelEditorCommandManager.Undo();
        }
    }
}
