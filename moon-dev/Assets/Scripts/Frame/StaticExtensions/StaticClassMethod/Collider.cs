using System.Collections.Generic;
using Data.ScriptableObject;
using Frame.Tool.Pool;
using Moon.Kernel;
using UnityEngine;

namespace Frame.StaticExtensions.StaticClassMethod
{
    public static class Collider
    {
        private static ContactFilter2D _contactFilter2D;

        private static GameObject _sliceObj;

        private static GameObject GetSliceObj
        {
            get
            {
                if (_sliceObj == null)
                {
                    _sliceObj = Explorer.TryGetSetting<PrefabFactory>().SLICE_OBJ;
                }

                return _sliceObj;
            }
        }

        public static List<Collider2D> CheckColliderConnectivity(this Collider2D targetCollider, Vector3 scale, LayerMask layerMask)
        {
            _contactFilter2D.SetLayerMask(~layerMask);
            var connectCollider = new List<Collider2D>();
            var visited = new HashSet<Collider2D>();
            Stack<Collider2D> stack = new Stack<Collider2D>();
            stack.Push(targetCollider);

            while (stack.Count > 0)
            {
                Collider2D current = stack.Pop();

                var transform = current.transform;
                var localScale = transform.localScale;
                var oldLocalScale = localScale;

                localScale =
                    new Vector3(localScale.x * scale.x,
                        localScale.y * scale.y,
                        localScale.z * scale.z);

                transform.localScale = localScale;
                Physics2D.SyncTransforms();

                if (visited.Contains(current))
                {
                    current.transform.localScale = oldLocalScale;
                    continue;
                }

                visited.Add(current);
                connectCollider.Add(current);

                List<Collider2D> collider2D = new List<Collider2D>();
                current.OverlapCollider(_contactFilter2D, collider2D);

                if (ObjectPool.Instance.CompareObj(current.gameObject, GetSliceObj))
                {
                    foreach (Collider2D c in collider2D)
                    {
                        stack.Push(c);
                    }
                }
                else
                {
                    foreach (Collider2D c in collider2D)
                    {
                        if (ObjectPool.Instance.CompareObj(c.gameObject, GetSliceObj))
                        {
                            stack.Push(c);
                        }
                    }
                }

                current.transform.localScale = oldLocalScale;
            }

            return connectCollider;
        }
    }
}