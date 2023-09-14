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
    
    private void Start()
    {
        m_material = Resources.Load<Material>("Materials/Test");
    }

    private void Update()
    {
        
        if (InputManager.Instance.GetDebuggerNum1Down)
        {
            CutSlice(SLICEDIR.Left);
            CutSlice(SLICEDIR.Up);
            CutSlice(SLICEDIR.Right);
            CutSlice(SLICEDIR.Down);
        }

        if (InputManager.Instance.GetDebuggerNum2Down)
        {
            CheckBox();
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
            GameObject gameObject = slicedHull.CreateUpperHull(collider,m_material);
            gameObject.GetComponent<Renderer>().material = m_material;
            gameObject.transform.CopyValue(collider.transform);
            PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
            gameObject.GetComponent<MeshFilter>().mesh.CreatePolygonCollider(polygonCollider2D);
            Destroy(collider.gameObject);
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
