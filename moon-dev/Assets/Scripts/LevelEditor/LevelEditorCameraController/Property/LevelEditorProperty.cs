using System;
using System.Collections.Generic;
using Editor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CustomProperty/LevelEditorCameraProperty",order = 3,fileName = "LevelEditorCameraProperty")]
public class LevelEditorProperty : ScriptableObject
{
    public CameraMotionProperty GetCameraMotionProperty;

    public RotationDragProperty GetRotationDragProperty;

    public OutlineProperty GetOutlineProperty;

    public SelectionProperty GetSelectionProperty;

    public UIProperty GetUIProperty;
    
    [Serializable]
    public struct CameraMotionProperty
    {
        [CustomLabel("编辑器相机Z距离变化速率"),Range(1,20)]
        public float CAMERA_Z_CHANGE_SPEED;
        [CustomLabel("编辑器相机Z方向最小距离"),Range(-100,100)]
        public float CAMERA_MIN_Z;
        [CustomLabel("编辑器相机Z方向最大距离"),Range(-100,100)]
        public float CAMERA_MAX_Z;
    }
    [Serializable]
    public struct RotationDragProperty
    {
        [CustomLabel("旋转拖拽速度"),Range(0.01f,1)]
        public float ROTATION_SPEED;
    }
    [Serializable]
    public struct OutlineProperty
    {
        [CustomLabel("描边模式")]
        public OUTLINEMODE OUTLINE_MODE;
        [CustomLabel("描边颜色")]
        public Color OUTLINE_COLOR;
        [CustomLabel("描边线宽")]
        public float OUTLINE_WIDTH;
    }
    
    [Serializable]
    public struct SelectionProperty
    {
        [CustomLabel("裁切框最小检测尺寸")]
        public Vector2 SELECTION_MIN_SIZE;
        [CustomLabel("裁切框颜色")]
        public Color SELECTION_COLOR;
    }
    [Serializable]
    public struct UIProperty
    {
        [CustomLabel("选择框UI名字")]
        public string SELECTION_UI_NAME;
        [CustomLabel("坐标系UI名字")]
        public string POSITION_AXIS;
        [CustomLabel("坐标系X轴名字")] 
        public string POSITION_AXIS_X;
        [CustomLabel("坐标系Y轴名字")] 
        public string POSITION_AXIS_Y;
        [CustomLabel("坐标系XY轴名字")] 
        public string POSITION_AXIS_XY;
        [CustomLabel("旋转轴UI名字")]
        public string ROTATION_AXIS;
        [CustomLabel("顶部面板UI名字")]
        public string TOP_PANEL;
        [CustomLabel("操作栏UI名字")]
        public string ACTION_PANEL;
        [CustomLabel("撤回按钮名字")]
        public string UNDO_BUTTON;
        [CustomLabel("恢复按钮名字")]
        public string REDO_BUTTON;
        [CustomLabel("视图按钮名字")]
        public string VIEW_BUTTON;
        [CustomLabel("坐标按钮名字")]
        public string POSITON_BUTTON;
        [CustomLabel("旋转按钮名字")]
        public string ROTATION_BUTTON;
        [CustomLabel("缩放按钮名字")]
        public string SCALE_BUTTON;
        [CustomLabel("矩形按钮名字")]
        public string RECT_BUTTON;
    }
}
