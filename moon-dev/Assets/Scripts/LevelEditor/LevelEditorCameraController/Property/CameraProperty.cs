using System;
using Kernel.Tool;
using Moon.Kernel.Attribute;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "CustomProperty/LevelEditorCameraProperty", order = 3, fileName = "LevelEditorCameraProperty")]
    public class CameraProperty : ScriptableObject
    {
        [Header("相机运动属性")] public CameraMotionProperty GetCameraMotionProperty;

        [Header("描边属性")] public OutlineProperty GetOutlineProperty;

        [Serializable]
        public struct CameraMotionProperty
        {
            [CustomLabel("编辑器相机Z距离变化速率"), Range(1, 20)]
            public float CAMERA_Z_CHANGE_SPEED;

            [CustomLabel("编辑器相机Z方向最小距离"), Range(-100, 100)]
            public float CAMERA_MIN_Z;

            [CustomLabel("编辑器相机Z方向最大距离"), Range(-100, 100)]
            public float CAMERA_MAX_Z;
        }

        [Serializable]
        public struct OutlineProperty
        {
            [field: SerializeField, CustomLabel("描边模式")]
            public OUTLINEMODE OUTLINE_MODE { get; private set; }

            [field: SerializeField, CustomLabel("描边颜色")]
            public Color OUTLINE_COLOR { get; private set; }

            [field: SerializeField, CustomLabel("描边线宽")]
            public float OUTLINE_WIDTH { get; private set; }
        }
    }
}