using Frame.Static.Extensions;
using Moon.Kernel.Extension;
using UnityEngine;

namespace Slicer
{
    public static class SlicerPropertyMethod
    {
        #region SliceMethod
    
        private static SlicerProperty.SlicerSizePanel _slicerSize;
        
        private static Vector3 rangeOfDetection => _slicerSize.RANGE_OF_DETECTION;
        
        private static float sliceSpacingCompensation => _slicerSize.SLICE_SPACING_COMPENSATION;
    
        private static float sliceThickNess => _slicerSize.SLICE_THICKNESS;
        /// <summary>
        /// 获取左裁切框数据
        /// </summary>
        /// <param name="角色属性"></param>
        /// <param name="参考Transform "></param>
        /// <returns>位置、尺寸、旋转</returns>
        public static (Vector3,Vector3,Quaternion) GetSliceLeftData(this SlicerProperty slicerProperty,Transform transform)
        {
            SlicerPropertyMethod._slicerSize = slicerProperty.SlicerSize;
            Vector3 pos = transform.position - transform.right * (rangeOfDetection.x / 2 + sliceSpacingCompensation);
            Vector3 size = rangeOfDetection.NewX(rangeOfDetection.y + sliceSpacingCompensation).NewY(sliceThickNess);
            Quaternion rot = Quaternion.AngleAxis(-90, Vector3.forward) * transform.rotation;
            return (pos,size,rot);
        }
        /// <summary>
        /// 获取上裁切框数据
        /// </summary>
        /// <param name="角色属性"></param>
        /// <param name="参考Transform "></param>
        /// <returns>位置、尺寸、旋转</returns>
        public static (Vector3,Vector3,Quaternion) GetSliceUpData(this SlicerProperty slicerProperty,Transform transform)
        {
            SlicerPropertyMethod._slicerSize = slicerProperty.SlicerSize;
            Vector3 pos = transform.position + transform.up * (rangeOfDetection.y / 2
                                                               + sliceSpacingCompensation);
            Vector3 size = rangeOfDetection.NewY(sliceThickNess).NewX(rangeOfDetection.x+sliceSpacingCompensation);
            Quaternion rot = Quaternion.AngleAxis(-180, Vector3.forward) * transform.rotation;
            return (pos,size,rot);
        }
        /// <summary>
        /// 获取右裁切框数据
        /// </summary>
        /// <param name="角色属性"></param>
        /// <param name="参考Transform "></param>
        /// <returns>位置、尺寸、旋转</returns>
        public static (Vector3,Vector3,Quaternion) GetSliceRightData(this SlicerProperty slicerProperty,Transform transform)
        {
            SlicerPropertyMethod._slicerSize = slicerProperty.SlicerSize;
            Vector3 pos = transform.position + transform.right * (rangeOfDetection.x / 2
                                                                  + sliceSpacingCompensation);
            Vector3 size = rangeOfDetection.NewX(rangeOfDetection.y + sliceSpacingCompensation).NewY(sliceThickNess);
            Quaternion rot = Quaternion.AngleAxis(-270, Vector3.forward) * transform.rotation;
            return (pos,size,rot);
        }
        /// <summary>
        /// 获取左下裁切框数据
        /// </summary>
        /// <param name="角色属性"></param>
        /// <param name="参考Transform "></param>
        /// <returns>位置、尺寸、旋转</returns>
        public static (Vector3,Vector3,Quaternion) GetSliceDownData(this SlicerProperty slicerProperty,Transform transform)
        {
            SlicerPropertyMethod._slicerSize = slicerProperty.SlicerSize;
            Vector3 pos = transform.position - transform.up * (rangeOfDetection.y / 2
                                                               + sliceSpacingCompensation);
            Vector3 size = rangeOfDetection.NewY(sliceThickNess).NewX(rangeOfDetection.x+sliceSpacingCompensation);
            Quaternion rot = Quaternion.AngleAxis(0, Vector3.forward) * transform.rotation;
            return (pos,size,rot);
        }
    
        #endregion
    }
}

