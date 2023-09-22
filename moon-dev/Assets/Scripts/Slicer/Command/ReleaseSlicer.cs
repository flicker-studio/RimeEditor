using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReleaseSlicer : ICommand
{
    private SlicerInformation m_slicerInformation;

    private List<List<Collider2D>> m_colliderListGroup = new List<List<Collider2D>>();

    private GameObject GetCombinationRigidbodyParentPrefab => m_slicerInformation.GetCombinationRigidbodyParentPrefab;

    private GameObject GetCombinationNotRigidbodyParentPrefab =>
        m_slicerInformation.GetCombinationNotRigidbodyParentPrefab;

    private GameObject GetRigidbodyParentPrefab => m_slicerInformation.GetRigidbodyParentPrefab;
    public ReleaseSlicer(SlicerInformation slicerInformation)
    {
        m_slicerInformation = slicerInformation;
    }

    public void Execute()
    {
        List<Collider2D> targetColliderList = m_slicerInformation.TargetList;
        foreach (var collider in targetColliderList)
        {
            collider.enabled = true;
        }
        foreach (var collider in targetColliderList)
        {
            m_colliderListGroup.Add(collider.CheckColliderConnectivity(
                m_slicerInformation.GetDetectionCompensationScale,GlobalSetting.LayerMasks.GROUND));
        }
        m_colliderListGroup = m_colliderListGroup.Distinct(new Collider2DListEqualityComparer()).ToList();
        foreach (var colliderList in m_colliderListGroup)
        {
            bool addParentFlag = true;
            foreach (var collider in colliderList)
            {
                if (!targetColliderList.Contains(collider) && !CheckRigidbody(collider))
                {
                    addParentFlag = false;
                }
            }

            if (addParentFlag)
            {
                GameObject parentObj = ObjectPool.Instance.OnTake(GetCombinationRigidbodyParentPrefab);
                m_slicerInformation.ParentList.Add(parentObj);
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
            else
            {
                GameObject parentObj = ObjectPool.Instance.OnTake(GetCombinationNotRigidbodyParentPrefab);
                m_slicerInformation.ParentList.Add(parentObj);
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

    private bool CheckRigidbody(Collider2D collider)
    {
        return collider.transform.parent != null
               && ObjectPool.Instance
                   .CompareObj(collider.transform.parent.gameObject, GetRigidbodyParentPrefab);
    }
    
    public class Collider2DListEqualityComparer : IEqualityComparer<List<Collider2D>>
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
}


