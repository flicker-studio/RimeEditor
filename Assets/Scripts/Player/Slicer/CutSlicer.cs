using System.Collections.Generic;
using EzySlice;
using UnityEngine;
using UnityEngine.Serialization;

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

    private Queue<GameObject> m_objQueue = new Queue<GameObject>();
    
    private void Start()
    {
        m_material = Resources.Load<Material>("Materials/Test");
    }

    private void Update()
    {
        
        if (InputManager.Instance.GetDebuggerNum1Down)
        {
            ObjectPool.Instance.ReturnCacheGameObjects(m_prefabFactory.SLICE_OBJ);
            CutSlice(SLICEDIR.Left);
            CutSlice(SLICEDIR.Up);
            CutSlice(SLICEDIR.Right);
            CutSlice(SLICEDIR.Down);
        }

        if (InputManager.Instance.GetDebuggerNum2Down)
        {
            CheckBox();
        }

        if (InputManager.Instance.GetDebuggerNum3Down)
        {
            GameObject obj = ObjectPool.Instance.RequestCacheGameObject(m_prefabFactory.SLICE_OBJ);
            m_objQueue.Enqueue(obj);
        }
        
        if (InputManager.Instance.GetDebuggerNum4Down)
        {
            if(m_objQueue.Count <= 0) return;
            GameObject obj = m_objQueue.Dequeue();
            ObjectPool.Instance.ReturnCacheGameObject(obj);
        }
    }

    private void CutSlice(SLICEDIR slicedir)
    {
        Vector3 pos, size;
        Quaternion rot;
        switch (slicedir)
        {
            case SLICEDIR.Left:
                (pos,  size, rot) = m_characterProperty.GetSliceLeftData(transform);
                break;
            case SLICEDIR.Up:
                (pos,  size, rot) = m_characterProperty.GetSliceUpData(transform);
                break;
            case SLICEDIR.Right:
                (pos,  size, rot) = m_characterProperty.GetSliceRightData(transform);
                break;
            case SLICEDIR.Down:
                (pos,  size, rot) = m_characterProperty.GetSliceDownData(transform);
                break;
            default:
                (pos, size, rot) = (Vector3.zero, Vector3.zero, Quaternion.identity);
                break;
        }
        Collider2D[] colliders = (pos).ToVector2().OverlapRotatedBox(size, rot.eulerAngles.z);
        foreach (var collider in colliders)
        {
            SlicedHull slicedHull = collider.Slice(pos, rot * Vector3.up);
            if(slicedHull == null) continue;
            GameObject obj = ObjectPool.Instance.RequestCacheGameObject(m_prefabFactory.SLICE_OBJ);
            obj.GetComponent<MeshFilter>().mesh = slicedHull.upperHull;
            obj.transform.CopyValue(collider.transform);
            Material[] shared = collider.GetComponent<MeshRenderer>().sharedMaterials;
            Material[] newShared = new Material[shared.Length + 1];
            System.Array.Copy(shared, newShared, shared.Length);
            newShared[shared.Length] = m_material;
            obj.GetComponent<Renderer>().sharedMaterials = newShared;
            if (ObjectPool.Instance.CompareObj(collider.gameObject, m_prefabFactory.SLICE_OBJ))
            {
                ObjectPool.Instance.ReturnCacheGameObject(collider.gameObject);
            }
        }
    }

    private void CheckBox()
    {
        Collider2D[] colliders = transform.position.ToVector2()
            .OverlapRotatedBox(m_characterProperty.SlicerProperty.RANGE_OF_DETECTION, transform.rotation.eulerAngles.z);
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
