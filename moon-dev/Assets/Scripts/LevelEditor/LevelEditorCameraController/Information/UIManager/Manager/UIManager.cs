using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    public class UIManager
    {
        public ActionPanel GetActionPanel => m_actionPanel;

        public ControlHandlePanel GetControlHandlePanel => m_controlHandlePanel;

        public ItemTransformPanel GetItemTransformPanel => m_itemTransformPanel;

        public HierarchyPanel GetHierarchyPanel => m_hierarchyPanel;

        public ItemWarehousePanel GetItemWarehousePanel => m_itemWarehousePanel;

        public InspectorPanel GetInspectorPanel => m_inspectorPanel;

        public LevelPanel GetLevelPanel => m_levelPanel;

        public AreaPanel GetAreaPanel => m_areaPanel;

        public LevelManagerPanel GetLevelManagerPanel => m_levelManagerPanel;

        public LevelSettingPanel GetLevelSettingPanel => m_levelSettingPanel;

        private ActionPanel m_actionPanel;

        private ControlHandlePanel m_controlHandlePanel;

        private ItemTransformPanel m_itemTransformPanel;

        private HierarchyPanel m_hierarchyPanel;

        private ItemWarehousePanel m_itemWarehousePanel;

        private AreaPanel m_areaPanel;

        private LevelManagerPanel m_levelManagerPanel;

        private LevelPanel m_levelPanel;

        private InspectorPanel m_inspectorPanel;

        private LevelSettingPanel m_levelSettingPanel;

        public UIManager(RectTransform levelEditorCanvasRect)
        {
            var uiProperty = Explorer.TryGetSetting<UIProperty>();
            m_actionPanel = new ActionPanel(levelEditorCanvasRect, uiProperty);
            m_controlHandlePanel = new ControlHandlePanel(levelEditorCanvasRect, uiProperty);
            m_itemTransformPanel = new ItemTransformPanel(levelEditorCanvasRect, uiProperty);
            m_hierarchyPanel = new HierarchyPanel(levelEditorCanvasRect, uiProperty);
            m_itemWarehousePanel = new ItemWarehousePanel(levelEditorCanvasRect, uiProperty);
            m_areaPanel = new AreaPanel(levelEditorCanvasRect, uiProperty);
            m_levelPanel = new LevelPanel(levelEditorCanvasRect, uiProperty);
            m_inspectorPanel = new InspectorPanel(levelEditorCanvasRect, uiProperty);
            m_levelManagerPanel = new LevelManagerPanel(levelEditorCanvasRect, uiProperty);
            m_levelSettingPanel = new LevelSettingPanel(levelEditorCanvasRect, uiProperty);
        }
    }
}