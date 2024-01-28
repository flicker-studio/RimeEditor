using UnityEngine;

namespace LevelEditor
{
    public class UIManager : IManager
    {
        public ActionPanel GetActionPanel { get; }

        public ControlHandlePanel GetControlHandlePanel { get; }

        public ItemTransformPanel GetItemTransformPanel { get; }

        public HierarchyPanel GetHierarchyPanel { get; }

        public ItemWarehousePanel GetItemWarehousePanel { get; }

        public InspectorPanel GetInspectorPanel { get; }

        public LevelPanel GetLevelPanel { get; }

        public AreaPanel GetAreaPanel { get; }

        public LevelManagerPanel GetLevelManagerPanel { get; }

        public LevelSettingPanel GetLevelSettingPanel { get; }

        public UIManager(RectTransform levelEditorCanvasRect, UIProperty uiProperty)
        {
            GetActionPanel = new ActionPanel(levelEditorCanvasRect, uiProperty);
            GetControlHandlePanel = new ControlHandlePanel(levelEditorCanvasRect, uiProperty);
            GetItemTransformPanel = new ItemTransformPanel(levelEditorCanvasRect, uiProperty);
            GetHierarchyPanel = new HierarchyPanel(levelEditorCanvasRect, uiProperty);
            GetItemWarehousePanel = new ItemWarehousePanel(levelEditorCanvasRect, uiProperty);
            GetAreaPanel = new AreaPanel(levelEditorCanvasRect, uiProperty);
            GetLevelPanel = new LevelPanel(levelEditorCanvasRect, uiProperty);
            GetInspectorPanel = new InspectorPanel(levelEditorCanvasRect, uiProperty);
            GetLevelManagerPanel = new LevelManagerPanel(levelEditorCanvasRect, uiProperty);
            GetLevelSettingPanel = new LevelSettingPanel(levelEditorCanvasRect, uiProperty);
        }
    }
}