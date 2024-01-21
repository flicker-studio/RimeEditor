using System;
using Editor;
using Frame.Tool.Popover;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "CustomProperty/LevelEditorUIProperty",order = 4,fileName = "LevelEditorUIProperty")]
    public class UIProperty : ScriptableObject
    {
        [field:SerializeField,Header("关卡管理面板属性")]
        public LevelManagerPanelUI GetLevelManagerPanelUI { get; private set; }
        [field:SerializeField,Header("关卡设置面板属性")]
        public LevelSettingPanelUI GetLevelSettingPanelUI { get; private set; }
        [field:SerializeField,Header("关卡面板属性")] 
        public LevelPanelUI GetLevelPanelUI { get; private set; }
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
        
        [field:SerializeField,Header("检视面板属性")]
        public InspectorUI GetInspectorPanelUI { get; private set; }
        [field:SerializeField,Header("弹窗属性")]
        public PopoverProperty GetPopoverProperty { get; private set; }
        
                
        [Serializable]
        public struct LevelPanelUI
        {
            [field:SerializeField,Header("关卡面板UI名字")]
            public LevelPanelUIName GetLevelPanelUIName{ get; private set; }
        }
        
        [Serializable]
        public struct ControlHandleUI
        {
            [field:SerializeField,Header("控制柄UI名字")]
            public ControlHandleUIName GetControlHandleUIName{ get; private set; }
            [field:SerializeField,Header("选择框UI属性")]
            public SelectionProperty GetSelectionProperty{ get; private set; }
            [field:SerializeField,Header("旋转轴属性")] 
            public RotationDragProperty GetRotationDragProperty{ get; private set; }
            
            [field:SerializeField,Header("缩放轴属性")] 
            public ScaleDragProperty GetScaleDragProperty { get;private set; }
            
            [field:SerializeField,Header("网格吸附属性")] 
            public GridSnappingProperty GetGridSnappingProperty { get;private set; }
            
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
        public struct LevelSettingPanelUI
        {
            [field:SerializeField,Header("关卡设置面板UI名字")]
            public LevelSettingPanelUIName GetLevelSettingPanelUIName { get; private set; }
        }
        
        [Serializable]
        public struct AreaPanelUI
        {
            [field:SerializeField,Header("区域面板UI名字")]
            public AreaPanelUIName GetAreaPanelUIName { get; private set; }
        }
        
        [Serializable]
        public struct InspectorUI
        {
            [field:SerializeField,Header("检视面板UI名字")]
            public InspectorPanelUIName GetInspectorPanelUIName { get; private set; }
            
            [field:SerializeField,Header("检视项属性")]
            public InspectorItemProperty GetInspectorItemProperty{ get; private set; }
        }
        
        [Serializable]
        public struct LevelManagerPanelUI
        {
            [field:SerializeField,Header("关卡管理面板UI名字")]
            public LevelManagerPanelUIName GetLevelManagerPanelUIName { get; private set; }
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
        public struct ScaleDragProperty
        {
            [field:SerializeField,CustomLabel("缩放轴拖拽速度"),Range(0.01f,1)]
            public float SCALE_SPEED { get; private set; }
            [field:SerializeField,CustomLabel("中心点轴显示补偿"),Range(0.01f,5)]
            public float CENTER_AXIS_COMPENSATION { get; private set; }
        }
        
        [Serializable]
        public struct GridSnappingProperty
        {
            [field:SerializeField,CustomLabel("网格颜色")]
            public Color GRID_COLOR { get; private set; }
            [field:SerializeField,CustomLabel("网格初始大小")]
            public int GRID_SIZE { get; private set; }
            [field:SerializeField,CustomLabel("单位网格尺寸")]
            public float CELL_SIZE { get; set; }
            [field:SerializeField,CustomLabel("网格扩张系数")]
            public int GROWTH_FACTOR { get; private set; }
            [field:SerializeField,CustomLabel("旋转单位量")]
            public int ROTATION_UNIT { get; private set; }
        }
        
        [Serializable]
        public struct MouseCursorProperty
        {
            [field:SerializeField,CustomLabel("鼠标光标边界检测距离补偿")]
            public Vector2 CURSOR_BOUND_CHECK_COMPENSATION { get; private set; }
        }
        [Serializable]
        public struct LevelPanelUIName
        {
            [field: SerializeField, CustomLabel("关卡全局名字")]
            public string LEVEL_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("保存按钮名字")]
            public string SAVE_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("设置按钮名字")]
            public string SETTING_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("释放按钮名字")]
            public string RELEASE_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("运行按钮名字")]
            public string PLAY_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("离开按钮名字")]
            public string EXIT_BUTTON{ get; private set; }
        }
        
        [Serializable]
        public struct PopoverProperty
        {
            [field: SerializeField, CustomLabel("弹窗错误颜色")]
            public Color POPOVER_ERROR_COLOR{ get; private set; }
            [field: SerializeField, CustomLabel("弹窗成功颜色")]
            public Color POPOVER_SUCCESS_COLOR{ get; private set; }
            [field: SerializeField, CustomLabel("关卡未命名")]
            public string POPOVER_TEXT_LEVEL_NAME_MISSING{ get; private set; }
            [field: SerializeField, CustomLabel("请选择一个关卡")]
            public string POPOVER_TEXT_CHOOSE_LEVEL_ERROR{ get; private set; }
            [field: SerializeField, CustomLabel("无法打开图片")]
            public string CANT_LOAD_IMAGE_ERROR{ get; private set; }
            [field: SerializeField, CustomLabel("删除成功")]
            public string POPOVER_DELETE_SUCCESS{ get; private set; }
            [field: SerializeField, CustomLabel("检测到错误关卡文件夹")]
            public string CHECK_ERROR_LEVEL_DIRECTORY{ get; private set; }
            [field: SerializeField, CustomLabel("保存成功")]
            public string POPOVER_TEXT_SAVE_SUCCESS{ get; private set; }
            [field: SerializeField, CustomLabel("持续时间")]
            public float DURATION{ get; private set; }
            [field: SerializeField, CustomLabel("尺寸")]
            public Vector2 SIZE{ get; private set; }
            [field: SerializeField, CustomLabel("出现位置")]
            public POPOVERLOCATION POPOVER_LOCATION{ get; private set; }
        }
        
        [Serializable]
        public struct LevelManagerPanelUIName
        {
            [field: SerializeField, CustomLabel("全屏面板名字")]
            public string FULL_PANEL{ get; private set; }
            [field: SerializeField, CustomLabel("关卡管理器界面根节点名字")]
            public string PANEL_ROOT{ get; private set; }
            [field: SerializeField, CustomLabel("关卡列表栏名字")]
            public string LEVEL_LIST_CONTENT{ get; private set; }
            [field: SerializeField, CustomLabel("打开按钮名字")]
            public string OPEN_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("创建按钮名字")]
            public string CREATE_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("编辑器协议按钮名字")]
            public string DECLARATION_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("离开按钮名字")]
            public string EXIT_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("关卡名字")]
            public string LEVEL_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("作者名字")]
            public string AUTHOR_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("修改/创建日期名字")]
            public string DATE_TIME{ get; private set; }
            [field: SerializeField, CustomLabel("关卡介绍名字")]
            public string INSTRODUCTION{ get; private set; }
            [field: SerializeField, CustomLabel("版本名字")]
            public string VERSION{ get; private set; }
            [field: SerializeField, CustomLabel("关卡封面图片名字")]
            public string LEVEL_COVER_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("子关卡数量名字")]
            public string SUB_LEVEL_NUMBER{ get; private set; }
            [field: SerializeField, CustomLabel("删除按钮名字")]
            public string DELETE_LEVEL_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("列表关卡图标")]
            public string ITEM_LEVEL_ICON{ get; private set; }
            [field: SerializeField, CustomLabel("列表关卡名字")]
            public string ITEM_LEVEL_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("列表关卡路径名字")]
            public string ITEM_LEVEL_PATH{ get; private set; }
            [field: SerializeField, CustomLabel("列表滚动面板名字")]
            public string SCROLL_RECT{ get; private set; }
            [field: SerializeField, CustomLabel("刷新按钮名字")]
            public string REFRESH_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("打开本地文件按钮名字")]
            public string OEPN_LOCAL_DIRECTORY_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("创意工坊按钮名字")]
            public string WORKS_SHOP_BUTTON{ get; private set; }
            [field: SerializeField, CustomLabel("本地关卡按钮名字")]
            public string LOCAL_LEVEL_BUTTON{ get; private set; }
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
        public struct LevelSettingPanelUIName
        {
            [field: SerializeField, CustomLabel("弹出界面根节点名字")]
            public string POPOVER_PANEL{ get; private set; }
            [field: SerializeField, CustomLabel("关卡设置面板名字")]
            public string SETTING_PANEL{ get; private set; }
            [field: SerializeField, CustomLabel("关闭按钮名字")]
            public string CLOSE_BUTTON_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("保存按钮名字")]
            public string SAVE_BUTTON_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("图片上传按钮名字")]
            public string COVER_IMAGE_NAME{ get; private set; }
            [field: SerializeField, CustomLabel("关卡名字输入框名字")]
            public string LEVEL_NAME_INPUTFIELD{ get; private set; }
            [field: SerializeField, CustomLabel("作者名字输入框名字")]
            public string AUTHOR_NAME_INPUTFIELD{ get; private set; }
            [field: SerializeField, CustomLabel("版本号输入框名字")]
            public string VERSION_INPUTFIELD{ get; private set; }
            [field: SerializeField, CustomLabel("简介输入框名字")]
            public string INTRODUCTION_INPUTFIELD{ get; private set; }
        }
        
        [Serializable]
        public struct ControlHandleUIName
        {
            [field:SerializeField,CustomLabel("选择框UI名字")]
            public string SELECTION_UI_NAME{ get; private set; }
            [field:SerializeField,CustomLabel("网格吸附UI名字")]
            public string GRID_UI_NAME{ get; private set; }
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
            [field:SerializeField,CustomLabel("缩放轴UI名字")]
            public string SCALE_AXIS{ get; private set; }
            [field:SerializeField,CustomLabel("缩放轴X名字")]
            public string SCALE_AXIS_X{ get; private set; }
            [field:SerializeField,CustomLabel("缩放轴Y名字")]
            public string SCALE_AXIS_Y{ get; private set; }
            [field:SerializeField,CustomLabel("缩放轴XY名字")]
            public string SCALE_AXIS_XY{ get; private set; }
            [field:SerializeField,CustomLabel("缩放轴X头部名字")]
            public string SCALE_AXIS_X_HEAD{ get; private set; }
            [field:SerializeField,CustomLabel("缩放轴X躯干名字")]
            public string SCALE_AXIS_X_BODY{ get; private set; }
            [field:SerializeField,CustomLabel("缩放轴Y头部名字")]
            public string SCALE_AXIS_Y_HEAD{ get; private set; }
            [field:SerializeField,CustomLabel("缩放轴Y躯干名字")]
            public string SCALE_AXIS_Y_BODY{ get; private set; }
            [field:SerializeField,CustomLabel("矩形框UI名字名字")]
            public string RECT_AXIS{ get; private set; }
            [field:SerializeField,CustomLabel("矩形右上角名字")]
            public string RECT_TOP_RIGHT_CORNER{ get; private set; }
            [field:SerializeField,CustomLabel("矩形左上角名字")]
            public string RECT_TOP_LEFT_CORNER{ get; private set; }
            [field:SerializeField,CustomLabel("矩形右下角名字")]
            public string RECT_BOTTOM_RIGHT_CORNER{ get; private set; }
            [field:SerializeField,CustomLabel("矩形左下角名字")]
            public string RECT_BOTTOM_LEFT_CORNER{ get; private set; }
            [field:SerializeField,CustomLabel("矩形上边名字")]
            public string RECT_TOP_EDGE{ get; private set; }
            [field:SerializeField,CustomLabel("矩形下边名字")]
            public string RECT_BOTTOM_EDGE{ get; private set; }
            [field:SerializeField,CustomLabel("矩形右边名字")]
            public string RECT_RIGHT_EDGE{ get; private set; }
            [field:SerializeField,CustomLabel("矩形左边名字")]
            public string RECT_LEFT_EDGE{ get; private set; }
            [field:SerializeField,CustomLabel("矩形中心名字")]
            public string RECT_CENTER{ get; private set; }
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
        public struct InspectorPanelUIName
        {
            [field:SerializeField,CustomLabel("检视面板根节点名字")] 
            public string INSPECTOR_ROOT { get; set; }
            [field:SerializeField,CustomLabel("检视面板内容名字")] 
            public string INSPECTOR_CONTENT { get; set; }
            [field:SerializeField,CustomLabel("描述内容名字")] 
            public string DESCRIBE_TEXT { get; set; }
        }
        
        [Serializable]
        public struct ItemNodeProperty
        {
            [field:SerializeField,CustomLabel("高光颜色")] 
            public Color HIGH_LIGHTED_COLOR{ get; private set; }
            [field:SerializeField,CustomLabel("选中颜色")] 
            public Color SELECTED_COLOR{ get; private set; }
        }
        [Serializable]
        public struct InspectorItemProperty
        {
            [field: SerializeField, CustomLabel("布尔项文字名字")]
            public string BOOLEAN_ITEM_TEXT;
        }
    }

}