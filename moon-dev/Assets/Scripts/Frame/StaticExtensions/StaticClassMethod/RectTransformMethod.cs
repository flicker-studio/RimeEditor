using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.Static.Extensions
{
    public static class RectTransformMethod
    {
        public static RectTransform SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
            return rt;
        }

        public static RectTransform SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
            return rt;
        }

        public static RectTransform SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
            return rt;
        }

        public static RectTransform SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
            return rt;
        }
    }
}
