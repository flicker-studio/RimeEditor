using EzySlice;
using UnityEngine;
using UnityEngine.Serialization;

public class CutSlicer : MonoBehaviour
{
    public Vector3 SlicerSize;

    public float CheckBoxThickNess;

    public float LengthCompensation;
    
    private Material m_material;
    
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
        Collider2D[] colliders = transform.position.ToVector2()
            .OverlapRotatedBox(SlicerSize, transform.rotation.eulerAngles.z);
        foreach (var collider in colliders)
        {
            SlicedHull slicedHull = collider.Slice(transform.position, transform.up);
            GameObject gameObject = slicedHull.CreateUpperHull(collider,m_material);
            gameObject.GetComponent<Renderer>().material = m_material;
            gameObject.transform.CopyValue(collider.transform);
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
        Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation, SlicerSize);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = new Color(1f, 0, 0f, 0.5f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position - transform.right * (SlicerSize.x/2 + LengthCompensation),
            Quaternion.AngleAxis(-90, Vector3.forward) * transform.rotation, 
            SlicerSize.NewX(SlicerSize.y+LengthCompensation).NewY(CheckBoxThickNess));
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.up * (SlicerSize.y/2 + LengthCompensation),
            Quaternion.AngleAxis(-180, Vector3.forward) * transform.rotation, 
            SlicerSize.NewY(CheckBoxThickNess).NewX(SlicerSize.x+LengthCompensation));
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.right * (SlicerSize.x/2 + LengthCompensation),
            Quaternion.AngleAxis(-270, Vector3.forward) * transform.rotation,
            SlicerSize.NewX(SlicerSize.y+LengthCompensation).NewY(CheckBoxThickNess));
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.TRS(transform.position - transform.up * (SlicerSize.y/2 + LengthCompensation),
            Quaternion.AngleAxis(0, Vector3.forward) * transform.rotation, 
            SlicerSize.NewY(CheckBoxThickNess).NewX(SlicerSize.x+LengthCompensation));
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldGizmosMatrix;
    }
#endif

}
