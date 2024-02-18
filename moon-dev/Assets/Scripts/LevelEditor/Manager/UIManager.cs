using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LevelEditor
{
    public class UIManager : IManager
    {
        public ActionPanel        GetActionPanel        { get; }
        public ControlHandlePanel GetControlHandlePanel { get; }
        public ItemTransformPanel GetItemTransformPanel { get; }
        public HierarchyPanel     GetHierarchyPanel     { get; }
        public ItemWarehousePanel GetItemWarehousePanel { get; }
        public InspectorPanel     GetInspectorPanel     { get; }
        public LevelPanel         GetLevelPanel         { get; }
        public AreaPanel          GetAreaPanel          { get; }
        public LevelManagerPanel  GetLevelManagerPanel  { get; }
        public LevelSettingPanel  GetLevelSettingPanel  { get; }

        public UIManager(RectTransform levelEditorCanvasRect, UISetting uiSetting)
        {
            GetActionPanel        = new ActionPanel(levelEditorCanvasRect, uiSetting);
            GetControlHandlePanel = new ControlHandlePanel(levelEditorCanvasRect, uiSetting);
            GetItemTransformPanel = new ItemTransformPanel(levelEditorCanvasRect, uiSetting);
            GetHierarchyPanel     = new HierarchyPanel(levelEditorCanvasRect, uiSetting);
            GetItemWarehousePanel = new ItemWarehousePanel(levelEditorCanvasRect, uiSetting);
            GetAreaPanel          = new AreaPanel(levelEditorCanvasRect, uiSetting);
            GetLevelPanel         = new LevelPanel(levelEditorCanvasRect, uiSetting);
            GetInspectorPanel     = new InspectorPanel(levelEditorCanvasRect, uiSetting);
            GetLevelManagerPanel  = new LevelManagerPanel(levelEditorCanvasRect, uiSetting);
            GetLevelSettingPanel  = new LevelSettingPanel(levelEditorCanvasRect, uiSetting);
        }

        
        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }
    }
}