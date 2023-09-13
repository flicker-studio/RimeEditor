using EzySlice;
using UnityEngine;
using UnityEngine.Serialization;

public class CutSlicer : MonoBehaviour
{
    public Vector3 SlicerSize;

    public float CheckBoxThickNess;

    public float LengthCompensation;
    
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
            CutSlice();
        }

        if (InputManager.Instance.GetDebuggerNum2Down)
        {
            CheckBox();
        }
    }

    private void CutSlice()
    {
        Collider2D[] colliders = (transform.position - transform.right 
                * (SlicerSize.x/2 + LengthCompensation)).ToVector2()
            .OverlapRotatedBox(SlicerSize.NewX(SlicerSize.y+LengthCompensation).NewY(CheckBoxThickNess)
                , (Quaternion.AngleAxis(-90, Vector3.forward) * transform.rotation).eulerAngles.z);
        foreach (var collider in colliders)
        {
            SlicedHull slicedHull = collider.Slice((transform.position - transform.right 
                * (SlicerSize.x/2 + LengthCompensation)), Quaternion.AngleAxis(-90, Vector3.forward) 
                                                          * transform.rotation
                                                            * Vector3.up);
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
            .OverlapRotatedBox(SlicerSize, transform.rotation.eulerAngles.z);
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
