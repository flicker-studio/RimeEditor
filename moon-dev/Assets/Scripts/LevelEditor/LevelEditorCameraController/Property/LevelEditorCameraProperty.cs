using System;
using Editor;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomProperty/LevelEditorCameraProperty",order = 3,fileName = "LevelEditorCameraProperty")]
public class LevelEditorCameraProperty : ScriptableObject
{
    [Header("相机运动属性")]
    public CameraMotionProperty GetCameraMotionProperty;
    
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
}
