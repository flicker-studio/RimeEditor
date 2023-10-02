using Frame.Static.Extensions;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    void FixedUpdate()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

       for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] += (normals[i] * Mathf.Sin(Time.time)) / 60000;
        }
        
       mesh.vertices = vertices;
    }
}