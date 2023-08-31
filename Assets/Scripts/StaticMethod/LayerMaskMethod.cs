using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskMethod
{
    //判断LayerMask是否包含指定的layer
    public static bool ContainsLayer(this LayerMask mask, int layer){
        return ((1 << layer) & mask.value) != 0;
    }
}
