using System.Collections.Generic;
using System.Linq;
using Data.ScriptableObject;
using UnityEngine;
using Frame.Static.Global;
using Frame.Tool.Pool;

namespace Frame.Static.Extensions
{
    public static class ColliderListStaticMethod
{
    private static PrefabFactory m_prefabFactory;

    private static GameObject GetCombinationRigidbodyParentPrefab =>
        m_prefabFactory.COMBINATION_COLLIDES_RIGIDBODY_PARENT;

    private static GameObject GetCombinationNotRigidbodyParentPrefab =>
        m_prefabFactory.COMBINATION_COLLIDES_NOT_RIGIDBODY_PARENT;

    private static GameObject GetRigidbodyParentPrefab => 
        m_prefabFactory.RIGIDBODY_PARENT;

    private static GameObject GetSlicerObj => m_prefabFactory.SLICE_OBJ;
    public static List<List<Collider2D>> CheckColliderConnectivity(this List<Collider2D> targetCollider,Vector3 scale,LayerMask layerMask)
    {
        List<List<Collider2D>> colliderListGroup = new List<List<Collider2D>>();
        foreach (var collider in targetCollider)
        {
            colliderListGroup.Add(collider.CheckColliderConnectivity(scale,layerMask));
        }
        colliderListGroup = colliderListGroup.Distinct(new Collider2DListEqualityComparer()).ToList();
        return colliderListGroup;
    }

    public static void GetCombinationConnectivity(this List<List<Collider2D>> colliderListGroup,PrefabFactory prefabFactory)
    {
        m_prefabFactory = prefabFactory;
        foreach (var colliderList in colliderListGroup)
        {
            bool addParentFlag = true;
            foreach (var collider in colliderList)
            {
                if (!CheckRigidbody(collider))
                {
                    addParentFlag = false;
                }
            }

            if (addParentFlag)
            {
                GameObject parentObj;
                if (colliderList.Count == 1 &&
                    colliderList[0].gameObject.name.Contains(GlobalSetting.ObjNameTag.rigidbodyTag))
                {
                    parentObj = ObjectPool.Instance.OnTake(GetRigidbodyParentPrefab);
                }
                else
                {
                    parentObj = ObjectPool.Instance.OnTake(GetCombinationRigidbodyParentPrefab);
                }
                foreach (var collider in colliderList)
                {
                    Transform colliderParent = collider.transform.parent;
                    collider.transform.parent = parentObj.transform;
                    if (colliderParent != null && colliderParent.childCount == 0)
                    {
                        if (ObjectPool.Instance.CompareObj(colliderParent.gameObject, GetRigidbodyParentPrefab))
                        {
                            ObjectPool.Instance.OnRelease(colliderParent.gameObject,GetRigidbodyParentPrefab);
                        }

                        if (ObjectPool.Instance.CompareObj(colliderParent.gameObject, GetCombinationRigidbodyParentPrefab))
                        {
                            ObjectPool.Instance.OnRelease(colliderParent.gameObject,GetCombinationRigidbodyParentPrefab);
                        }
                    }
                }
            }
            else
            {
                GameObject parentObj = ObjectPool.Instance.OnTake(GetCombinationNotRigidbodyParentPrefab);
                foreach (var collider in colliderList)
                {
                    Transform colliderParent = collider.transform.parent;
                    collider.transform.parent = parentObj.transform;
                    if (colliderParent != null 
                        && ObjectPool.Instance.CompareObj(colliderParent.gameObject,GetRigidbodyParentPrefab))
                    {
                        ObjectPool.Instance.OnRelease(colliderParent.gameObject,GetRigidbodyParentPrefab);
                    }
                }
            }
        }
    }
    
    private class Collider2DListEqualityComparer : IEqualityComparer<List<Collider2D>>
    {
        public bool Equals(List<Collider2D> x, List<Collider2D> y)
        {
            return x.OrderBy(c => c.GetHashCode()).SequenceEqual(y.OrderBy(c => c.GetHashCode()));
        }

        public int GetHashCode(List<Collider2D> obj)
        {
            int hash = 0;
            foreach (var o in obj.OrderBy(c => c.GetHashCode()))
            {
                hash ^= o.GetHashCode();
            }
            return hash;
        }
    }
    
    private static bool CheckRigidbody(this Collider2D collider)
    {
        return ObjectPool.Instance.CompareObj(collider.gameObject,GetSlicerObj) ||
               collider.name.Contains(GlobalSetting.ObjNameTag.rigidbodyTag);
    }
}
}