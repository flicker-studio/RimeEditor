using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class SliceTest : MonoBehaviour
{
    private bool m_isFlag = true;
    private void Update()
    {
        if (InputManager.Instance.GetJumpInput && m_isFlag)
        {
            StartCoroutine("StartSlice");
        }
    }
    
    public Vector3 FindCenter(Vector3[] points)
    {
        Vector3 center = Vector3.zero;

        foreach (Vector3 point in points)
        {
            center += point;
        }

        return center / points.Length;
    }

    IEnumerator StartSlice()
    {
        m_isFlag = false;
        SlicedHull slicedHull;
        yield return slicedHull = gameObject.Slice(transform.position 
                                                   + FindCenter(GetComponent<MeshFilter>().mesh.vertices)
            , transform.right);
        GameObject temp1 = slicedHull.CreateUpperHull();
        GameObject temp2 = slicedHull.CreateLowerHull();
        yield return temp1.AddComponent<CreatePolygonCollider>();
        yield return temp2.AddComponent<CreatePolygonCollider>();
        temp1.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Test");
        temp2.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Test");
        temp1.transform.position = transform.position;
        temp2.transform.position = transform.position;
        temp1.transform.localScale = transform.localScale;
        temp2.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
}
