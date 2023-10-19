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
        
        [Serializable]
        public struct ControlHandleUI
        {
            [field:SerializeField,Header("控制柄UI名字")]
            public ControlHandleUIName GetControlHandleUIName{ get; private set; }
            [field:SerializeField,Header("选择框UI属性")]
            public SelectionProperty GetSelectionProperty{ get; private set; }
            [field:SerializeField,Header("旋转轴属性")] 
            public RotationDragProperty GetRotationDragProperty{ get; private set; }
            [field:SerializeField,Header("描边属性")]
            public OutlineProperty GetOutlineProperty{ get; private set; }
            
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
            [field:SerializeField,Header("几何变换面板名字")]
            public ItemTransformPanelUIName GetItemTransformPanelUIName { get; private set; }
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
        public struct OutlineProperty
        {
            [field:SerializeField,CustomLabel("描边模式")]
            public OUTLINEMODE OUTLINE_MODE{ get; private set; }
            [field:SerializeField,CustomLabel("描边颜色")]
            public Color OUTLINE_COLOR{ get; private set; }
            [field:SerializeField,CustomLabel("描边线宽")]
            public float OUTLINE_WIDTH{ get; private set; }
        }
        [Serializable]
        public struct MouseCursorProperty
        {
            [field:SerializeField,CustomLabel("鼠标光标边界检测距离补偿")]
            public Vector2 CURSOR_BOUND_CHECK_COMPENSATION { get; private set; }
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
    }

}