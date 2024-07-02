using UnityEngine;

namespace Moon.Kernel.Extension
{
    /// <summary>
    ///     A static extension method of RectTransform.
    /// </summary>
    public static class RectTransformExtension
    {
        /// <summary>
        ///     The offset of the left of the rectangle relative to the left anchor.
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="left offeset"></param>
        /// <returns>target rectTransform</returns>
        public static RectTransform SetLeft(this RectTransform rt, float left)
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
        public static RectTransform SetRight(this RectTransform rt, float right)
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
        public static RectTransform SetTop(this RectTransform rt, float top)
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
        public static RectTransform SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
            return rt;
        }
    }
}