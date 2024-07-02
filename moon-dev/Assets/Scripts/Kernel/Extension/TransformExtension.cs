using System.Collections.Generic;
using UnityEngine;

namespace Moon.Kernel.Extension
{
    public static class TransformExtension
    {
        public static void CopyValue(this Transform transform, Transform targetTransform)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
            transform.localScale = targetTransform.localScale;
        }

        public static List<Transform> GetChilds(this Transform transform)
        {
            var childs = new List<Transform>();
            for (var i = 0; i < transform.childCount; i++) childs.Add(transform.GetChild(i));

            return childs;
        }

        public static Transform FindPath(this Transform transform, string path)
        {
            var pashs = path.TrimEnd().Split("/");
            var childTransform = transform;

            for (var i = 0; i < pashs.Length; i++)
            {
                childTransform = childTransform.Find(pashs[i]);

                if (childTransform == null)
                {
                    return null;
                }
            }

            return childTransform;
        }

        public static (Vector3, Quaternion, Vector3) GetTransformValue(this Transform transform)
        {
            return (transform.position, transform.rotation, transform.localScale);
        }

        public static void SetTransformValue(this Transform transform, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }
    }
}