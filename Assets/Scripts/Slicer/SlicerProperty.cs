using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CustomProperty/SlicerProperty",order = 2,fileName = "SlicerProperty")]
public class SlicerProperty : ScriptableObject
{
    public SlicerSizePanel SlicerSize;
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
}
