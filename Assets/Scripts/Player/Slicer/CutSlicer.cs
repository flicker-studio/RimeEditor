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

public class CutSlicer : MonoBehaviour
{
    private Material m_material;
    
    private CharacterProperty m_characterProperty 
        => Resources.Load<CharacterProperty>("GlobalSettings/CharacterProperty");

    private PrefabFactory m_prefabFactory
        => Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
    
    private void Start()
    {
        m_material = Resources.Load<Material>("Materials/Test");
    }

    private void FixedUpdate()
    {
        
        if (InputManager.Instance.GetDebuggerNum1Down)
        {
            ObjectPool.Instance.ReturnCacheGameObjects(m_prefabFactory.SLICE_OBJ);
            CutSliceAll(CheckBox());
        }

        if (InputManager.Instance.GetDebuggerNum2Down)
        {
            CheckBox();
        }
    }

    private void CutSliceAll(List<Collider2D> targetColliderList)
    {
        foreach (SLICEDIR dir in Enum.GetValues(typeof(SLICEDIR)))
        {
            CutSlice(dir,targetColliderList);
        }
    }

    private (Vector3,Vector3,Quaternion) GetSliceData(SLICEDIR slicedir)
    {
        switch (slicedir)
        {
            case SLICEDIR.Left:
                return m_characterProperty.GetSliceLeftData(transform);
            case SLICEDIR.Up:
                return m_characterProperty.GetSliceUpData(transform);
            case SLICEDIR.Right:
                return m_characterProperty.GetSliceRightData(transform);
            case SLICEDIR.Down:
                return m_characterProperty.GetSliceDownData(transform);
            default:
                return (Vector3.zero, Vector3.zero, Quaternion.identity);
        }
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
            GameObject obj = ObjectPool.Instance.RequestCacheGameObject(m_prefabFactory.SLICE_OBJ);
            obj.GetComponent<MeshFilter>().mesh = slicedHull.upperHull;
            obj.GetComponent<MeshFilter>().mesh.CreatePolygonCollider(obj.GetComponent<PolygonCollider2D>());
            obj.transform.CopyValue(collider.transform);
            Material[] shared = collider.GetComponent<MeshRenderer>().sharedMaterials;
            Material[] newShared = new Material[shared.Length + 1];
            System.Array.Copy(shared, newShared, shared.Length);
            newShared[shared.Length] = m_material;
            obj.GetComponent<Renderer>().sharedMaterials = newShared;
            targetColliderList.Add(obj.GetComponent<Collider2D>());
            if (ObjectPool.Instance.CompareObj(collider.gameObject, m_prefabFactory.SLICE_OBJ))
            {
                ObjectPool.Instance.ReturnCacheGameObject(collider.gameObject);
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
        return transform.position.ToVector2()
            .OverlapRotatedBox(m_characterProperty.SlicerProperty.RANGE_OF_DETECTION
                , transform.rotation.eulerAngles.z).ToList();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation, m_characterProperty.SlicerProperty.RANGE_OF_DETECTION);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = new Color(1f, 0, 0f, 0.5f);
        (Vector3 pos, Vector3 size, Quaternion rot) = m_characterProperty.GetSliceLeftData(transform);
        Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        
        (pos, size, rot) = m_characterProperty.GetSliceUpData(transform);
        Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        
        (pos, size, rot) = m_characterProperty.GetSliceRightData(transform);
        Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        
        (pos, size, rot) = m_characterProperty.GetSliceDownData(transform);
        Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        
        Gizmos.matrix = oldGizmosMatrix;
    }
#endif

}
