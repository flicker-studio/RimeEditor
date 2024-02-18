using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LevelEditor
{
    public class UIManager : IManager
    {
        internal ActionPanel        GetActionPanel        { get; }
        internal ControlHandlePanel GetControlHandlePanel { get; }
        internal ItemTransformPanel GetItemTransformPanel { get; }
        internal HierarchyPanel     GetHierarchyPanel     { get; }
        internal ItemWarehousePanel GetItemWarehousePanel { get; }
        internal InspectorPanel     GetInspectorPanel     { get; }
        internal LevelPanel         GetLevelPanel         { get; }
        internal AreaPanel          GetAreaPanel          { get; }
        internal LevelManagerPanel  GetLevelManagerPanel  { get; }
        internal LevelSettingPanel  GetLevelSettingPanel  { get; }

        public UIManager(RectTransform levelEditorCanvasRect, UISetting uiSetting)
        {
            //GetActionPanel        = new GameObject().AddComponent<ActionPanel>();
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