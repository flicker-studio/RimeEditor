using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    // Update is called once per frame

    private void Start()
    {
        var euler = Quaternion.Euler(new Vector3(0, 0, 40));
        var rotation = new Quaternion();
        rotation = transform.rotation * euler;
        transform.rotation = rotation;

        float angle1 = 10;
        float angle2 = 50;
        transform.position = new Vector3(transform.position.x * Mathf.Cos((angle1 + angle2) * Mathf.PI / 180),
            transform.position.y * Mathf.Sin((angle1 + angle2) * Mathf.PI / 180), transform.position.z);
    }

    void Update()
    {
        
    }
}
