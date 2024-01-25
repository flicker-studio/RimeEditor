using UnityEngine;

namespace Moon.Kernel.Extension
{
    public static class RectTransform
    {
        public static UnityEngine.RectTransform SetLeft(this UnityEngine.RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
            return rt;
        }

        public static UnityEngine.RectTransform SetRight(this UnityEngine.RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
            return rt;
        }

        public static UnityEngine.RectTransform SetTop(this UnityEngine.RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
            return rt;
        }

        public static UnityEngine.RectTransform SetBottom(this UnityEngine.RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
            return rt;
        }
    }
}