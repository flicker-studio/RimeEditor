using System.Collections.Generic;
using UnityEngine;

namespace Moon.Kernel.Extension
{
    public static class Collider
    {
        private static ContactFilter2D m_contactFilter2D = new ContactFilter2D();

        private static GameObject m_sliceObj;

        private static GameObject GetSliceObj
        {
            get
            {
                if (m_sliceObj == null)
                {
                    m_sliceObj = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory").SLICE_OBJ;
                }

                return m_sliceObj;
            }
        }
    
        public static List<Collider2D> CheckColliderConnectivity(this Collider2D targetCollider,Vector3 scale,UnityEngine.LayerMask layerMask)
        {
            m_contactFilter2D.SetLayerMask(~layerMask);
            List<Collider2D> m_connectCollider = new List<Collider2D>();
            HashSet<Collider2D> m_visited = new HashSet<Collider2D>();
            Stack<Collider2D> stack = new Stack<Collider2D>();
            stack.Push(targetCollider);
            while (stack.Count > 0)
            {
                Collider2D current = stack.Pop();
            
                Vector3 oldLocalScale = current.transform.localScale;
                current.transform.localScale = 
                    new Vector3(current.transform.localScale.x * scale.x,
                        current.transform.localScale.y * scale.y,
                        current.transform.localScale.z * scale.z);
                Physics2D.SyncTransforms();
            
                if (m_visited.Contains(current))
                {
                    current.transform.localScale = oldLocalScale;
                    continue;
                }

                m_visited.Add(current);
                m_connectCollider.Add(current);

                List<Collider2D> collider2D = new List<Collider2D>();
                current.OverlapCollider(m_contactFilter2D, collider2D);

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
            return m_connectCollider;
        }
    }

}