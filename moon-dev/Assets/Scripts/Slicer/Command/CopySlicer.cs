using System;
using System.Collections.Generic;
using System.Linq;
using EzySlice;
using Frame.Static.Extensions;
using Frame.Static.Global;
using Frame.Tool;
using Frame.Tool.Pool;
using Slicer.Information;
using UnityEngine;

namespace Slicer.Command
{
    enum SLICEDIR
{
    Left,
    Up,
    Right,
    Down
}

public class CopySlicer : SliceCommand
{
    private SlicerInformation m_slicerInformation;
    
    private List<List<Collider2D>> m_colliderListGroup = new List<List<Collider2D>>();

    private GameObject GetCombinationRigidbodyParentPrefab =>
        m_slicerInformation.GetCombinationRigidbodyParentPrefab;

    private GameObject GetCombinationNotRigidbodyParentPrefab =>
        m_slicerInformation.GetCombinationNotRigidbodyParentPrefab;
    
    public override void Execute()
    {
        m_colliderListGroup = m_slicerInformation.TargetList.CheckColliderConnectivity(
            m_slicerInformation.GetDetectionCompensationScale
            , GlobalSetting.LayerMasks.GROUND);
        
        foreach (var collider in m_slicerInformation.TargetList)
        {
            ObjectPool.Instance.OnRelease(collider.gameObject);
        }

        ObjectPool.Instance.OnReleaseAll(GetCombinationRigidbodyParentPrefab);
        
        ObjectPool.Instance.OnReleaseAll(GetCombinationNotRigidbodyParentPrefab);
        
        List<Collider2D> tempList = new List<Collider2D>();
        
        foreach (var colliderList in m_colliderListGroup)
        {
            tempList.AddRange(colliderList);
        }

        tempList = tempList.Distinct().ToList();

        foreach (var collider in m_slicerInformation.TargetList)
        {
            tempList.Remove(collider);
        }

        m_colliderListGroup = tempList.CheckColliderConnectivity(
            m_slicerInformation.GetDetectionCompensationScale
            , GlobalSetting.LayerMasks.GROUND);

        m_colliderListGroup.GetCombinationConnectivity(m_slicerInformation.GetPrefabFactory);
        
        m_slicerInformation.TargetList = CutSliceAll(CheckBox());
    }
    
    public CopySlicer(SlicerInformation slicerInformation)
    {
        m_slicerInformation = slicerInformation;
    }

    private List<Collider2D> CutSliceAll(List<Collider2D> targetColliderList)
    {
        List<Collider2D> oriColliderList = new List<Collider2D>();
        oriColliderList.AddRange(targetColliderList);
        
        foreach (SLICEDIR dir in Enum.GetValues(typeof(SLICEDIR)))
        {
            CutSlice(dir,targetColliderList,oriColliderList);
        }
        
        foreach (var collider in targetColliderList)
        {
            collider.enabled = false;
            collider.transform.parent = m_slicerInformation.GetTransform;
        }
        return targetColliderList;
    }
    
    private void CutSlice(SLICEDIR slicedir,List<Collider2D> targetColliderList,List<Collider2D> oriColliderList)
    {
        Vector3 pos, size;
        Quaternion rot;
        (pos,size,rot) = GetSliceData(slicedir);
        List<Collider2D> tempList = new List<Collider2D>();
        tempList.AddRange(targetColliderList);
        foreach (var collider in tempList)
        {
            if(!targetColliderList.Contains(collider)) continue;
            SlicedHull slicedHull = collider.Slice(pos, rot * Vector3.up);
            if(slicedHull == null) continue;
            GameObject obj = ObjectPool.Instance.OnTake(m_slicerInformation.GetProductPrefab);
            obj.GetComponent<MeshFilter>().mesh = slicedHull.upperHull;
            obj.GetComponent<MeshFilter>().mesh.CreatePolygonCollider(obj.GetComponent<PolygonCollider2D>());
            obj.transform.CopyValue(collider.transform);
            AddSliceMaterial(obj, collider.gameObject,m_slicerInformation.GetCutSurfaceMaterial);
            targetColliderList.Add(obj.GetComponent<Collider2D>());
            if (ObjectPool.Instance.CompareObj(collider.gameObject, m_slicerInformation.GetProductPrefab)
                && !oriColliderList.Contains(collider))
            {
                ObjectPool.Instance.OnRelease(collider.gameObject);
                targetColliderList.Remove(collider);
            }
            else
            {
                targetColliderList.Remove(collider);
            }
        }
        foreach (var collider in tempList)
        {
            if(!targetColliderList.Contains(collider)) continue;
            if(ObjectPool.Instance.CompareObj(collider.gameObject, m_slicerInformation.GetProductPrefab)) continue;
            if(!oriColliderList.Contains(collider))continue;
            GameObject obj = ObjectPool.Instance.OnTake(m_slicerInformation.GetProductPrefab);
            obj.GetComponent<MeshFilter>().mesh = collider.GetComponent<MeshFilter>().mesh;
            PolygonCollider2D targetPolygonCollider2D = collider.GetComponent<PolygonCollider2D>();
            if (targetPolygonCollider2D != null)
            {
                obj.GetComponent<PolygonCollider2D>().CopyComponent(targetPolygonCollider2D);
            }
            else
            {
                obj.GetComponent<MeshFilter>().mesh.CreatePolygonCollider(obj.GetComponent<PolygonCollider2D>());
            }
            obj.transform.CopyValue(collider.transform);
            obj.GetComponent<Renderer>().sharedMaterials = collider.GetComponent<MeshRenderer>().sharedMaterials;
            targetColliderList.Add(obj.GetComponent<Collider2D>());
            targetColliderList.Remove(collider);
        }
    }

    private List<Collider2D> CheckBox()
    {
        List<Collider2D> overlapColliderList = m_slicerInformation.GetTransform.position.ToVector2()
            .OverlapRotatedBox(m_slicerInformation.GetDetectionRange
                , m_slicerInformation.GetTransform.rotation.eulerAngles.z).ToList();
        List<Collider2D> tempList = new List<Collider2D>();
        tempList.AddRange(overlapColliderList);
        foreach (var collider in overlapColliderList)
        {
            if (ObjectPool.Instance.CompareObj(collider.gameObject, m_slicerInformation.GetProductPrefab))
            {
                tempList.Remove(collider);
            }
        }

        return tempList;
    }
    
    private (Vector3,Vector3,Quaternion) GetSliceData(SLICEDIR slicedir)
    {
        switch (slicedir)
        {
            case SLICEDIR.Left:
                return m_slicerInformation.GetSliceLeftData();
            case SLICEDIR.Up:
                return m_slicerInformation.GetSliceUpData();
            case SLICEDIR.Right:
                return m_slicerInformation.GetSliceRightData();
            case SLICEDIR.Down:
                return m_slicerInformation.GetSliceDownData();
            default:
                return (Vector3.zero, Vector3.zero, Quaternion.identity);
        }
    }

    private void AddSliceMaterial(GameObject obj,GameObject target,Material material)
    {
        Material[] shared = target.GetComponent<MeshRenderer>().sharedMaterials;
        Material[] newShared = new Material[shared.Length + 1];
        Array.Copy(shared, newShared, shared.Length);
        newShared[shared.Length] = m_slicerInformation.GetCutSurfaceMaterial;
        obj.GetComponent<Renderer>().sharedMaterials = newShared;
    }
}

}