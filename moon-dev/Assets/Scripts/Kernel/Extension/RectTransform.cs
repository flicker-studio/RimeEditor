using UnityEngine;

namespace Moon.Kernel.Extension
{
    /// <summary>
    ///     A static extension method of RectTransform.
    /// </summary>
    public static class RectTransform
    {
        /// <summary>
        ///     The offset of the left of the rectangle relative to the left anchor.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="left offeset"></param>
        /// <returns>target rectTransform</returns>
        public static UnityEngine.RectTransform SetLeft(this UnityEngine.RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
            return rt;
        }

        /// <summary>
        ///     The offset of the right of the rectangle relative to the right anchor.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="right offeset"></param>
        /// <returns>target rectTransform</returns>
        public static UnityEngine.RectTransform SetRight(this UnityEngine.RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
            return rt;
        }

        /// <summary>
        ///     The offset of the top of the rectangle relative to the top anchor.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="top offeset"></param>
        /// <returns>target rectTransform</returns>
        public static UnityEngine.RectTransform SetTop(this UnityEngine.RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
            return rt;
        }

        /// <summary>
        ///     The offset of the bottom of the rectangle relative to the bottom anchor.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="bottom offeset"></param>
        /// <returns>target rectTransform</returns>
        public static UnityEngine.RectTransform SetBottom(this UnityEngine.RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
            return rt;
        }
    }
}