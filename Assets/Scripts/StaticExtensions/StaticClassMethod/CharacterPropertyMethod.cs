using UnityEngine;

public static class CharacterPropertyMethod
{
    #region SliceMethod

    private static CharacterProperty.SlicerPropertyPanel slicerProperty;
    
    private static Vector3 rangeOfDetection => slicerProperty.RANGE_OF_DETECTION;
    
    private static float sliceSpacingCompensation => slicerProperty.SLICE_SPACING_COMPENSATION;

    private static float sliceThickNess => slicerProperty.SLICE_THICKNESS;
    /// <summary>
    /// 获取左裁切框数据
    /// </summary>
    /// <param name="角色属性"></param>
    /// <param name="参考Transform "></param>
    /// <returns>位置、尺寸、旋转</returns>
    public static (Vector3,Vector3,Quaternion) GetSliceLeftData(this CharacterProperty characterProperty,Transform transform)
    {
        slicerProperty = characterProperty.SlicerProperty;
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
    public static (Vector3,Vector3,Quaternion) GetSliceUpData(this CharacterProperty characterProperty,Transform transform)
    {
        slicerProperty = characterProperty.SlicerProperty;
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
    public static (Vector3,Vector3,Quaternion) GetSliceRightData(this CharacterProperty characterProperty,Transform transform)
    {
        slicerProperty = characterProperty.SlicerProperty;
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
    public static (Vector3,Vector3,Quaternion) GetSliceDownData(this CharacterProperty characterProperty,Transform transform)
    {
        slicerProperty = characterProperty.SlicerProperty;
        Vector3 pos = transform.position - transform.up * (rangeOfDetection.y / 2
                                                           + sliceSpacingCompensation);
        Vector3 size = rangeOfDetection.NewY(sliceThickNess).NewX(rangeOfDetection.x+sliceSpacingCompensation);
        Quaternion rot = Quaternion.AngleAxis(0, Vector3.forward) * transform.rotation;
        return (pos,size,rot);
    }

    #endregion
}
