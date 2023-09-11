using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EzySlice;
using UnityEngine;

public struct TrianglePoints
{
    public Vector2 P1;
    public Vector2 P2;
    public Vector2 P3;

    public TrianglePoints(Vector2 p1,Vector2 p2,Vector2 p3)
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }
    public override bool Equals(object obj)
    {
        if (obj is TrianglePoints)
        {
            TrianglePoints other = (TrianglePoints)obj;
            return P1 == other.P1 && P2 == other.P2 && P3 == other.P3 ||
                   P1 == other.P2 && P2 == other.P1 && P3 == other.P3 ||
                   P1 == other.P3 && P2 == other.P2 && P3 == other.P1 ||
                   P1 == other.P1 && P2 == other.P3 && P3 == other.P2 ||
                   P1 == other.P2 && P2 == other.P3 && P3 == other.P1 ||
                   P1 == other.P3 && P2 == other.P1 && P3 == other.P2;
        }
        return false;
    }

    public bool Judge()
    {
        return P1 == P2 || P1 == P3 || P2 == P3;
    }

    public override int GetHashCode()
    {
        return P1.GetHashCode() ^ P2.GetHashCode() ^ P3.GetHashCode();
    }
}

[RequireComponent(typeof(Rigidbody2D),typeof(PolygonCollider2D))]
public class CreatePolygonCollider : MonoBehaviour
{
    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Rigidbody2D m_rigidbody;
    [SerializeField] private float m_radius = 0.1f;
    
    private void Start()
    {
        m_mesh = GetComponent<MeshFilter>().sharedMesh;
        m_vertices = m_mesh.vertices;
        int[] triangles = m_mesh.triangles;
        Vector2[] vertices2D = new Vector2[m_vertices.Length];
        for (int i = 0; i < m_vertices.Length; i++)
        {
            vertices2D[i] = new Vector2(m_vertices[i].x, m_vertices[i].y);
        }
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        List<TrianglePoints> trianglePointsList = new List<TrianglePoints>();
        for (int i = 0,j = 0; i < triangles.Length; i += 3,j ++)
        {
            TrianglePoints trianglePoints = new TrianglePoints(vertices2D[triangles[i]]
                ,vertices2D[triangles[i + 1]]
                ,vertices2D[triangles[i + 2]]);
            trianglePointsList.Add(trianglePoints);
        }
        HashSet<TrianglePoints> set = new HashSet<TrianglePoints>(trianglePointsList);
        trianglePointsList = new List<TrianglePoints>(set);
        trianglePointsList = trianglePointsList.Where(triangle => triangle.Judge() == false).ToList();
        trianglePointsList = GetMaxIndependentSet(trianglePointsList);
        collider.pathCount = trianglePointsList.Count;
        for (int i = 0; i < trianglePointsList.Count; i++)
        {
            collider.SetPath(i,new Vector2[]{trianglePointsList[i].P1
                ,trianglePointsList[i].P2,
                trianglePointsList[i].P3
            });   
        }
        m_rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        gameObject.layer = GlobalSetting.LayerMasks.Ground;
    }

    private void OnDrawGizmos()
    {
        Vector3[] points = GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3 center = FindCenter(points);
        Gizmos.color = Color.red;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(point+transform.position,m_radius);
            Gizmos.DrawLine(center+transform.position,point+transform.position);
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

    private List<TrianglePoints> GetMaxIndependentSet(List<TrianglePoints> targetList)
    {
        List<TrianglePoints> nonIntersectingTriangles = new List<TrianglePoints>();

        foreach (TrianglePoints newTriangle in targetList)
        {
            bool intersects = false;

            foreach (TrianglePoints existingTriangle in nonIntersectingTriangles)
            {
                if (Intersects(newTriangle, existingTriangle))
                {
                    intersects = true;
                    break;
                }
            }

            if (!intersects)
            {
                nonIntersectingTriangles.Add(newTriangle);
            }
        }

        return nonIntersectingTriangles;
    }
    
    private bool Intersects(TrianglePoints t1, TrianglePoints t2)
    {
        // 对于t1的每条边与t2的每条边，检查它们是否相交
        if (LineSegmentsIntersect(t1.P1, t1.P2, t2.P1, t2.P2) ||
            LineSegmentsIntersect(t1.P1, t1.P2, t2.P1, t2.P3) ||
            LineSegmentsIntersect(t1.P1, t1.P2, t2.P2, t2.P3) ||
            LineSegmentsIntersect(t1.P1, t1.P3, t2.P1, t2.P2) ||
            LineSegmentsIntersect(t1.P1, t1.P3, t2.P1, t2.P3) ||
            LineSegmentsIntersect(t1.P1, t1.P3, t2.P2, t2.P3) ||
            LineSegmentsIntersect(t1.P2, t1.P3, t2.P1, t2.P2) ||
            LineSegmentsIntersect(t1.P2, t1.P3, t2.P1, t2.P3) ||
            LineSegmentsIntersect(t1.P2, t1.P3, t2.P2, t2.P3))
        {
            return true;
        }

        return false;
    }
    
    private bool LineSegmentsIntersect(Vector2 p, Vector2 p2, Vector2 q, Vector2 q2)
    {
        Vector2 r = p2 - p;
        Vector2 s = q2 - q;

        float rxs = CrossProduct(r, s);
        Vector2 qp = q - p;

        if (Mathf.Abs(rxs) < float.Epsilon && CrossProduct(qp, r) == 0)
        {
            return false;
        }

        if (Mathf.Abs(rxs) < float.Epsilon && CrossProduct(qp, r) != 0)
        {
            return false;
        }

        float t = CrossProduct(qp, s) / rxs;
        float u = CrossProduct(qp, r) / rxs;

        return (0 < t && t < 1 && 0 < u && u < 1);
    }

    private float CrossProduct(Vector2 v1, Vector2 v2)
    {
        return v1.x * v2.y - v1.y * v2.x;
    }

}
