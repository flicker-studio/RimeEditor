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
            m_isFlag = false;
            SlicedHull slicedHull = gameObject.Slice(transform.position, transform.right);
            GameObject temp1 = slicedHull.CreateUpperHull();
            GameObject temp2 = slicedHull.CreateLowerHull();
            temp1.AddComponent<AdjustColliderOrMesh>();
            temp2.AddComponent<AdjustColliderOrMesh>();
            temp1.transform.position = transform.position;
            temp2.transform.position = transform.position;
        }
    }
}
