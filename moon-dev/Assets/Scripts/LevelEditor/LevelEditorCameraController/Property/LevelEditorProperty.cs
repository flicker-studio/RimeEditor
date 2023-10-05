using System;
using Editor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CustomProperty/LevelEditorCameraProperty",order = 3,fileName = "LevelEditorCameraProperty")]
public class LevelEditorProperty : ScriptableObject
{
    public CameraMotionProperty GetCameraMotionProperty;

    public OutlineProperty GetOutlineProperty;

    public SelectionProperty GetSelectionProperty;
    
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
}
