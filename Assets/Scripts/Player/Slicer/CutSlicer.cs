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

public class CutSlicer
{
    private Material m_material;
    
    private SlicerProperty m_slicerProperty;

    private PrefabFactory m_prefabFactory;

    private Transform m_transform;

    public void Update()
    {
        ObjectPool.Instance.ReturnCacheGameObjects(m_prefabFactory.SLICE_OBJ);
        CutSliceAll(CheckBox());
    }
    
    public CutSlicer(Transform transform,SlicerProperty slicerProperty,PrefabFactory prefabFactory)
    {
        m_slicerProperty = slicerProperty;
        m_prefabFactory = prefabFactory;
        m_transform = transform;
        m_material = Resources.Load<Material>("Materials/Test");
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
            GameObject obj = ObjectPool.Instance.OnTake(m_prefabFactory.SLICE_OBJ);
            obj.GetComponent<MeshFilter>().mesh = slicedHull.upperHull;
            obj.GetComponent<MeshFilter>().mesh.CreatePolygonCollider(obj.GetComponent<PolygonCollider2D>());
            obj.transform.CopyValue(collider.transform);
            AddSliceMaterial(obj, collider.gameObject,m_material);
            targetColliderList.Add(obj.GetComponent<Collider2D>());
            if (ObjectPool.Instance.CompareObj(collider.gameObject, m_prefabFactory.SLICE_OBJ))
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
        return m_transform.position.ToVector2()
            .OverlapRotatedBox(m_slicerProperty.SlicerSize.RANGE_OF_DETECTION
                , m_transform.rotation.eulerAngles.z).ToList();
    }
    
    private (Vector3,Vector3,Quaternion) GetSliceData(SLICEDIR slicedir)
    {
        switch (slicedir)
        {
            case SLICEDIR.Left:
                return m_slicerProperty.GetSliceLeftData(m_transform);
            case SLICEDIR.Up:
                return m_slicerProperty.GetSliceUpData(m_transform);
            case SLICEDIR.Right:
                return m_slicerProperty.GetSliceRightData(m_transform);
            case SLICEDIR.Down:
                return m_slicerProperty.GetSliceDownData(m_transform);
            default:
                return (Vector3.zero, Vector3.zero, Quaternion.identity);
        }
    }

    private void AddSliceMaterial(GameObject obj,GameObject target,Material material)
    {
        Material[] shared = target.GetComponent<MeshRenderer>().sharedMaterials;
        Material[] newShared = new Material[shared.Length + 1];
        Array.Copy(shared, newShared, shared.Length);
        newShared[shared.Length] = material;
        obj.GetComponent<Renderer>().sharedMaterials = newShared;
    }

// #if UNITY_EDITOR
//     void OnDrawGizmos()
//     {
//         Gizmos.color = new Color(0, 1, 0, 0.5f);
//         Matrix4x4 oldGizmosMatrix = Gizmos.matrix;
//         Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation, m_characterProperty.SlicerProperty.RANGE_OF_DETECTION);
//         Gizmos.DrawCube(Vector3.zero, Vector3.one);
//         Gizmos.color = new Color(1f, 0, 0f, 0.5f);
//         (Vector3 pos, Vector3 size, Quaternion rot) = m_characterProperty.GetSliceLeftData(transform);
//         Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
//         Gizmos.DrawCube(Vector3.zero, Vector3.one);
//         
//         (pos, size, rot) = m_characterProperty.GetSliceUpData(transform);
//         Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
//         Gizmos.DrawCube(Vector3.zero, Vector3.one);
//         
//         (pos, size, rot) = m_characterProperty.GetSliceRightData(transform);
//         Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
//         Gizmos.DrawCube(Vector3.zero, Vector3.one);
//         
//         (pos, size, rot) = m_characterProperty.GetSliceDownData(transform);
//         Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
//         Gizmos.DrawCube(Vector3.zero, Vector3.one);
//         
//         Gizmos.matrix = oldGizmosMatrix;
//     }
// #endif

}
