using System;
using Editor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Slicer.Data
{
    [CreateAssetMenu(menuName = "CustomProperty/SlicerProperty",order = 2,fileName = "SlicerProperty")]
    public class SlicerProperty : ScriptableObject
    {
        [Header("裁切框尺寸")]
        public SlicerSizePanel SlicerSize;
        [Header("连通性检测参数")]
        public ConnectedObjectPanel ConnectedObject;
        [Serializable]
        public struct SlicerSizePanel
        {
            [CustomLabel("裁切器范围")]
            public Vector3 RANGE_OF_DETECTION;
            [CustomLabel("切片厚度"),Range(0,1)]
            public float SLICE_THICKNESS;
            [CustomLabel("间距补偿"),Range(-1,1)]
            public float SLICE_SPACING_COMPENSATION;
        }
        [Serializable]
        public struct ConnectedObjectPanel
        {
            [CustomLabel("连通物体检测缩放补偿")]
            public Vector3 DETECTION_COMPENSATIONS_SCALE;
        }
    }

}