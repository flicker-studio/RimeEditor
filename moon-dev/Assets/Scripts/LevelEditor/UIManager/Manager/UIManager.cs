using UnityEngine;

namespace LevelEditor
{
    public class UIManager
    {
        public ActionPanel GetActionPanel => m_actionPanel;

        public ControlHandlePanel GetControlHandlePanel => m_controlHandlePanel;

        public ItemTransformPanel GetItemTransformPanel => m_itemTransformPanel;
    
        private ActionPanel m_actionPanel;
    
        private ControlHandlePanel m_controlHandlePanel;
    
        private ItemTransformPanel m_itemTransformPanel;

        public UIManager(RectTransform levelEditorCanvasRect, UIProperty uiProperty)
        {
            m_actionPanel = new ActionPanel(levelEditorCanvasRect, uiProperty);
            m_controlHandlePanel = new ControlHandlePanel(levelEditorCanvasRect, uiProperty);
            m_itemTransformPanel = new ItemTransformPanel(levelEditorCanvasRect, uiProperty);
        }
    }
}
