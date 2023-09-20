using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EzySlice;
using UnityEngine;

enum SLICEDIR
{
    Left,
    Up,
    Right,
    Down
}

public class CopySlicer : ICommand
{
    private SlicerInformation m_slicerInformation;

    public void Execute()
    {
        foreach (var collider in m_slicerInformation.TargetList)
        {
            ObjectPool.Instance.OnRelease(collider.gameObject);
        }
        m_slicerInformation.TargetList = CutSliceAll(CheckBox());
    }
    
    public CopySlicer(SlicerInformation slicerInformation)
    {
        m_slicerInformation = slicerInformation;
    }

    private List<Collider2D> CutSliceAll(List<Collider2D> targetColliderList)
    {
        foreach (SLICEDIR dir in Enum.GetValues(typeof(SLICEDIR)))
        {
            CutSlice(dir,targetColliderList);
        }

        return targetColliderList;
    }
    
    private void CutSlice(SLICEDIR slicedir,List<Collider2D> targetColliderList)
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
            if (ObjectPool.Instance.CompareObj(collider.gameObject, m_slicerInformation.GetProductPrefab))
            {
                ObjectPool.Instance.OnRelease(collider.gameObject);
                targetColliderList.Remove(collider);
            }
            else
            {
                targetColliderList.Remove(collider);
            }
        }
    }

    private List<Collider2D> CheckBox()
    {
        return m_slicerInformation.GetTransform.position.ToVector2()
            .OverlapRotatedBox(m_slicerInformation.GetDetectionRange
                , m_slicerInformation.GetTransform.rotation.eulerAngles.z).ToList();
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
