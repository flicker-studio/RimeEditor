namespace Moon.Kernel.Extension
{
    public static class LayerMask
    {
        /// <summary>
        /// 判断 <paramref name="mask"/> 是否包含指定的 <paramref name="layer"/>
        /// </summary>
        public static bool ContainsLayer(this UnityEngine.LayerMask mask, int layer)
        {
            return ((1 << layer) & mask.value) != 0;
        }
    }
}