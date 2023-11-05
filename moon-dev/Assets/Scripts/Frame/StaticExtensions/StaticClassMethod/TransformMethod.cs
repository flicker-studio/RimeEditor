using System.Collections.Generic;
using UnityEngine;

namespace Frame.Static.Extensions
{
    public static class TransformMethod
    {
        public static void CopyValue(this Transform transform,Transform targetTransform)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
            transform.localScale = targetTransform.localScale;
        }

        public static List<Transform> GetChilds(this Transform transform)
        {
            List<Transform> childs = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                childs.Add(transform.GetChild(i));
            }

            return childs;
        }

        public static Transform FindPath(this Transform transform,string path)
        {
            string[] pashs = path.TrimEnd().Split("/");
            Transform childTransform = transform;
            for (var i = 0; i < pashs.Length; i++)
            {
                childTransform = childTransform.Find(pashs[i]);
                if (childTransform == null) return null;
            }

            return childTransform;
        }
    }
}

