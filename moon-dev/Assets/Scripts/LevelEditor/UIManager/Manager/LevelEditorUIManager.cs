using UnityEngine;

public class LevelEditorUIManager
{
    public ActionPanel GetActionPanel => m_actionPanel;

    public ControlHandlePanel GetControlHandlePanel => m_controlHandlePanel;

    public ItemTransformPanel GetItemTransformPanel => m_itemTransformPanel;
    
    private ActionPanel m_actionPanel;
    
    private ControlHandlePanel m_controlHandlePanel;
    
    private ItemTransformPanel m_itemTransformPanel;

    public LevelEditorUIManager(RectTransform levelEditorCanvasRect, LevelEditorUIProperty levelEditorUIProperty)
    {
        m_actionPanel = new ActionPanel(levelEditorCanvasRect, levelEditorUIProperty);
        m_controlHandlePanel = new ControlHandlePanel(levelEditorCanvasRect, levelEditorUIProperty);
        m_itemTransformPanel = new ItemTransformPanel(levelEditorCanvasRect, levelEditorUIProperty);
    }
}
