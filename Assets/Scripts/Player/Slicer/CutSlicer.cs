using System;
using System.Collections.Generic;
using System.Linq;
using EzySlice;
using UnityEngine;

public class CutSlicer : MonoBehaviour
{
    public Vector3 slicerSize;

    private bool m_canSlice = true;

    private Material m_material;

    private void Start()
    {
        m_material = Resources.Load<Material>("Materials/Test");
    }

    private void Update()
    {
        if (!InputManager.Instance.GetSliceInput)
        {
            m_canSlice = true;
        }

        if (InputManager.Instance.GetSliceInput && m_canSlice)
        {
            m_canSlice = false;
            Collider2D[] colliders = transform.position.ToVector2()
                .OverlapRotatedBox(slicerSize, transform.rotation.eulerAngles.z);
            foreach (var collider in colliders)
            {
                SlicedHull slicedHull = collider.Slice(transform.position, transform.up);
                GameObject gameObject = slicedHull.CreateUpperHull(collider,m_material);
                gameObject.GetComponent<Renderer>().material = m_material;
                gameObject.transform.CopyValue(collider.transform);
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, slicerSize);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldGizmosMatrix;
    }
#endif

}
