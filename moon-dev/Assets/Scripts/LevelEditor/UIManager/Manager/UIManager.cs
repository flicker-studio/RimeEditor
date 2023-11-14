using UnityEngine;
using UnityEngine.UIElements;

namespace LevelEditor
{
    public class UIManager
    {
        public ActionPanel GetActionPanel => m_actionPanel;

        public ControlHandlePanel GetControlHandlePanel => m_controlHandlePanel;

        public ItemTransformPanel GetItemTransformPanel => m_itemTransformPanel;

        public HierarchyPanel GetHierarchyPanel => m_hierarchyPanel;

        public ItemWarehousePanel GetItemWarehousePanel => m_itemWarehousePanel;

        public AreaPanel GetAreaPanel => m_areaPanel;
    
        private ActionPanel m_actionPanel;
    
        private ControlHandlePanel m_controlHandlePanel;
    
        private ItemTransformPanel m_itemTransformPanel;

        private HierarchyPanel m_hierarchyPanel;

        private ItemWarehousePanel m_itemWarehousePanel;

        private AreaPanel m_areaPanel;

        public UIManager(RectTransform levelEditorCanvasRect, UIProperty uiProperty)
        {
            m_actionPanel = new ActionPanel(levelEditorCanvasRect, uiProperty);
            m_controlHandlePanel = new ControlHandlePanel(levelEditorCanvasRect, uiProperty);
            m_itemTransformPanel = new ItemTransformPanel(levelEditorCanvasRect, uiProperty);
            m_hierarchyPanel = new HierarchyPanel(levelEditorCanvasRect, uiProperty);
            m_itemWarehousePanel = new ItemWarehousePanel(levelEditorCanvasRect, uiProperty);
            m_areaPanel = new AreaPanel(levelEditorCanvasRect, uiProperty);
        }
    }
}
