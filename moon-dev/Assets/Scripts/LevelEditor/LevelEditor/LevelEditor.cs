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
        if (InputManager.Instance.GetDebuggerNum1Down)
        {
            m_levelEditorCommandManager.Undo();
        }
        if (InputManager.Instance.GetDebuggerNum2Down)
        {
            m_levelEditorCommandManager.Redo();
        }
    }
}
