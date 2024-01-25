using UnityEngine;

namespace Moon.Kernel.Extension
{
    public static class LayerMask
    {
        //判断LayerMask是否包含指定的layer
        public static bool ContainsLayer(this UnityEngine.LayerMask mask, int layer){
            return ((1 << layer) & mask.value) != 0;
        }
    }
}
