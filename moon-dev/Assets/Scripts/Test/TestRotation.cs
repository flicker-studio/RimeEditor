using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    private float distance;

    private void Start()
    {
        distance = (lookAt.position - transform.position).magnitude;
    }

    void Update()
    {
        var fromAToB = Quaternion.AngleAxis(Time.deltaTime * 10, transform.up);
        var rotate = transform.rotation * fromAToB;

        transform.rotation = rotate;
        
        var offset = new Vector3(lookAt.position.x, transform.position.y, lookAt.position.z);
        transform.position = offset - new Vector3(transform.forward.x, 0, transform.forward.z) * distance;
        
    }
}
