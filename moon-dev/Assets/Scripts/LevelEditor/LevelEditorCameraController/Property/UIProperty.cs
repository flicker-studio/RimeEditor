using System;
using Editor;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "CustomProperty/LevelEditorUIProperty",order = 4,fileName = "LevelEditorUIProperty")]
    public class UIProperty : ScriptableObject
    {
        [field:SerializeField,Header("控制柄面板属性")] 
        public ControlHandleUI GetControlHandleUI { get; private set; }
    
        [field:SerializeField,Header("操作面板属性")]
        public ActionPanelUI GetActionPanelUI { get; private set; }
        
        [field:SerializeField,Header("几何变换面板属性")]
        public ItemTransformPanelUI GetItemTransformPanelUI { get; private set; }
        
        [field:SerializeField,Header("层级面板属性")]
        public HierarchyPanelUI GetHierarchyPanelUI { get; private set; }
        
        [field:SerializeField,Header("物件仓库面板属性")]
        public ItemWarehousePanelUI GetItemWarehousePanelUI { get; private set; }
        
        [field:SerializeField,Header("区域面板属性")]
        public AreaPanelUI GetAreaPanelUI { get; private set; }
        
        [Serializable]
        public struct ControlHandleUI
        {
            [field:SerializeField,Header("控制柄UI名字")]
            public ControlHandleUIName GetControlHandleUIName{ get; private set; }
            [field:SerializeField,Header("选择框UI属性")]
            public SelectionProperty GetSelectionProperty{ get; private set; }
            [field:SerializeField,Header("旋转轴属性")] 
            public RotationDragProperty GetRotationDragProperty{ get; private set; }
            
            [field:SerializeField,Header("鼠标光标属性")]
            public MouseCursorProperty GetMouseCursorProperty { get; private set; }
        }
        
        [Serializable]
        public struct ActionPanelUI
        {
            [field:SerializeField,Header("操作面板UI名字")]
            public ActionPanelUIName GetActionPanelUIName { get; private set; }
        }
        
        [Serializable]
        public struct ItemTransformPanelUI
        {
            [field:SerializeField,Header("几何变换面板UI名字")]
            public ItemTransformPanelUIName GetItemTransformPanelUIName { get; private set; }
        }
        [Serializable]
        public struct HierarchyPanelUI
        {
            [field:SerializeField,Header("层级面板UI名字")]
            public HierarchyPanelUIName GetHierarchyPanelUIName { get; private set; }
            
            [field:SerializeField,Header("层级项属性")]
            public ItemNodeProperty GetItemNodeProperty { get; private set; }
        }
        
        [Serializable]
        public struct ItemWarehousePanelUI
        {
            [field:SerializeField,Header("物件仓库面板UI名字")]
            public ItemWarehousePanelUIName GetItemWarehousePanelUIName { get; private set; }
            [field:SerializeField,Header("物件仓库属性")]
            public ItemWarehouseProperty GetItemWarehouseProperty { get; private set; }
        }
        
        [Serializable]
        public struct AreaPanelUI
        {
            [field:SerializeField,Header("区域面板UI名字")]
            public AreaPanelUIName GetAreaPanelUIName { get; private set; }
        }
        
        [Serializable]
        public struct SelectionProperty
        {
            [field:SerializeField,CustomLabel("选择框最小检测尺寸")]
            public Vector2 SELECTION_MIN_SIZE{ get; private set; }
            [field:SerializeField,CustomLabel("选择框颜色")]
            public Color SELECTION_COLOR{ get; private set; }
        }
        
        [Serializable]
        public struct RotationDragProperty
        {
            [field:SerializeField,CustomLabel("旋转轴拖拽速度"),Range(0.01f,1)]
            public float ROTATION_SPEED { get; private set; }
        }
        
        [Serializable]
        public struct MouseCursorProperty
        {
            [field:SerializeField,CustomLabel("鼠标光标边界检测距离补偿")]
            public Vector2 CURSOR_BOUND_CHECK_COMPENSATION { get; private set; }
        }
        
        [Serializable]
        public struct ItemWarehousePanelUIName
        {
            [field: SerializeField, CustomLabel("弹出界面根节点名字")]
            public string POPOVER_PANEL{ get; private set; }
            [field: SerializeField, CustomLabel("物件仓库面板名字")]
            public string ITEM_WAREHOURSE_PANEL{ get; private set; }
            [field: SerializeField, CustomLabel("物件栏滚动条名字")]
            public string ITEM_DETAIL_GROUP_SCROLL_BAR{ get; private set; }
            [field: SerializeField, CustomLabel("物件栏滚动视图名字")]
            public string ITEM_DETAIL_GROUP_SCROLL_VIEW{ get; private set; }
            [field: SerializeField, CustomLabel("搜索栏名字")]
            public string SEARCH_INPUT_FIELD{ get; private set; }
            [field: SerializeField, CustomLabel("清空搜索栏按钮名字")]
            public string CLEAR_SEARCH_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("选择物体文本提示名字")]
            public string SELECT_TEXT{ get; private set; }
            [field: SerializeField, CustomLabel("找不到物体提示文字")]
            public string NOTHING_FIND_TEXT{ get; private set; }
            [field: SerializeField, CustomLabel("创建物体按钮名字")]
            public string CREATE_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("关闭仓库按钮名字")]
            public string CLOSE_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("物件种类侧边栏名字")]
            public string ITEM_TYPE_GROUP{ get; private set; }
            [field: SerializeField, CustomLabel("物件种类侧边栏预制体文字名字")]
            public string ITEM_TYPE_TEXT{ get; private set; }
            [field: SerializeField, CustomLabel("物件详细归纳栏名字")]
            public string ITEM_DETAIL_GROUP_CONTENT{ get; private set; }
            [field: SerializeField, CustomLabel("归纳栏预制体提示字名字")]
            public string ITEM_DETAIL_GROUP_PREFAB_TEXT{ get; private set; }
            [field: SerializeField, CustomLabel("归纳栏预制体内容名字")]
            public string ITEM_DETAIL_GROUP_PREFAB_CONTENT{ get; private set; }
            [field: SerializeField, CustomLabel("物品格子预制体图片名字")]
            public string ITEM_LATTICE_IMAGE{ get; private set; }
            [field: SerializeField, CustomLabel("物品格子预制体文字名字")]
            public string ITEM_LATTICE_TEXT{ get; private set; }
        } 
        
        [Serializable]
        public struct ItemWarehouseProperty
        {
            [field: SerializeField, CustomLabel("物件文件根目录索引")]
            public string ITEM_ROOT_PATH{ get; private set; }
        }
        
        [Serializable]
        public struct ControlHandleUIName
        {
            [field:SerializeField,CustomLabel("选择框UI名字")]
            public string SELECTION_UI_NAME{ get; private set; }
            [field:SerializeField,CustomLabel("坐标系UI名字")]
            public string POSITION_AXIS{ get; private set; }
            [field:SerializeField,CustomLabel("坐标系X轴名字")] 
            public string POSITION_AXIS_X{ get; private set; }
            [field:SerializeField,CustomLabel("坐标系Y轴名字")] 
            public string POSITION_AXIS_Y{ get; private set; }
            [field:SerializeField,CustomLabel("坐标系XY轴名字")] 
            public string POSITION_AXIS_XY{ get; private set; }
            [field:SerializeField,CustomLabel("旋转轴UI名字")]
            public string ROTATION_AXIS{ get; private set; }
        }
        
        [Serializable]
        public struct ActionPanelUIName
        {
            [field:SerializeField,CustomLabel("撤回按钮名字")]
            public string UNDO_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("恢复按钮名字")]
            public string REDO_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("视图按钮名字")]
            public string VIEW_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("坐标按钮名字")]
            public string POSITON_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("旋转按钮名字")]
            public string ROTATION_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("缩放按钮名字")]
            public string SCALE_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("矩形按钮名字")]
            public string RECT_BUTTON{ get; private set; }
        }
        
        [Serializable]
        public struct ItemTransformPanelUIName
        {
            [field:SerializeField,CustomLabel("几何变换面板名字")]
            public string ROOT_PANEL{ get; private set; }
            [field:SerializeField,CustomLabel("编辑按钮名字")]
            public string EDIT_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("位置X输入框名字")] 
            public string POSITION_INPUT_X{ get; private set; }
            [field:SerializeField,CustomLabel("位置Y输入框名字")] 
            public string POSITION_INPUT_Y{ get; private set; }
            [field:SerializeField,CustomLabel("位置Z输入框名字")] 
            public string POSITION_INPUT_Z{ get; private set; }
            [field:SerializeField,CustomLabel("旋转X输入框名字")] 
            public string ROTATION_INPUT_X{ get; private set; }
            [field:SerializeField,CustomLabel("旋转Y输入框名字")] 
            public string ROTATION_INPUT_Y{ get; private set; }
            [field:SerializeField,CustomLabel("旋转Z输入框名字")] 
            public string ROTATION_INPUT_Z{ get; private set; }
            [field:SerializeField,CustomLabel("缩放X输入框名字")] 
            public string SCALE_INPUT_X{ get; private set; }
            [field:SerializeField,CustomLabel("缩放Y输入框名字")] 
            public string SCALE_INPUT_Y{ get; private set; }
            [field:SerializeField,CustomLabel("缩放Z输入框名字")] 
            public string SCALE_INPUT_Z{ get; private set; }
        }
        
        [Serializable]
        public struct HierarchyPanelUIName
        {
            [field:SerializeField,CustomLabel("层级展示面板名字")] 
            public string HIERARCHY_VIEW_CONTENT{ get; private set; }
            [field:SerializeField,CustomLabel("滚动视图名字")] 
            public string SCROLL_VIEW{ get; private set; }
            [field:SerializeField,CustomLabel("添加按钮名字")] 
            public string ADD_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("删除按钮名字")] 
            public string DELETE_BUTTON{ get; private set; }
        }
        [Serializable]
        public struct AreaPanelUIName
        {
            [field:SerializeField,CustomLabel("描述文字名字")] 
            public string DESCRIBE_TEST{ get; private set; }
            [field:SerializeField,CustomLabel("下拉列表名字")] 
            public string AREA_DROP_DOWN{ get; private set; }
            [field:SerializeField,CustomLabel("添加区域按钮")] 
            public string ADD_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("删除区域按钮")] 
            public string DELETE_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("管理区域按钮")] 
            public string MANAGE_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("区域设置按钮")] 
            public string AREA_SETTING_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("环境设置按钮")] 
            public string ENVIRONMENT_SETTING_BUTTON{ get; private set; }
        }
        
        [Serializable]
        public struct ItemNodeProperty
        {
            [field:SerializeField,CustomLabel("高光颜色")] 
            public Color HIGH_LIGHTED_COLOR{ get; private set; }
            [field:SerializeField,CustomLabel("选中颜色")] 
            public Color SELECTED_COLOR{ get; private set; }
        }
    }

}