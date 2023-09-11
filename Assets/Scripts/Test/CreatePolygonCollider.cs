using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CreatePolygonCollider : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshFilter>().mesh.CreatePolygonCollider(GetComponent<PolygonCollider2D>());
    }
}
