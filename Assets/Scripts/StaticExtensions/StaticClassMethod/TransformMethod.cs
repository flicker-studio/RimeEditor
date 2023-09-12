using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformMethod
{
    public static void CopyValue(this Transform transform,Transform targetTransform)
    {
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
        transform.localScale = targetTransform.localScale;
    }
}
