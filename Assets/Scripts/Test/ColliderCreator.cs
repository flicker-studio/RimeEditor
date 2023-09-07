using System.Collections.Generic;
using UnityEngine;

public class ColliderCreator : MonoBehaviour
{
    private void Start()
    {
        // Stop if no mesh filter exists or there's already a collider
        if (GetComponent<PolygonCollider2D>() || GetComponent<MeshFilter>() == null)
        {
            return;
        }
        // Get mesh vertices
        Vector3[] vertices3D = GetComponent<MeshFilter>().mesh.vertices;

        // Convert vertices to Vector2
        Vector2[] vertices2D = new Vector2[vertices3D.Length];
        for (int i = 0; i < vertices3D.Length; i++)
        {
            vertices2D[i] = new Vector2(vertices3D[i].x, vertices3D[i].y);
        }

        // Calculate the convex hull
        List<Vector2> convexHull = CalculateConvexHull(vertices2D);

        // Add PolygonCollider2D component and set its path
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.SetPath(0, convexHull.ToArray());
    }

    private List<Vector2> CalculateConvexHull(Vector2[] points)
    {
        List<Vector2> hull = new List<Vector2>();

        // Find the leftmost point
        int leftMostIndex = 0;
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i].x < points[leftMostIndex].x)
            {
                leftMostIndex = i;
            }
        }
        hull.Add(points[leftMostIndex]);

        // Calculate the convex hull using the Graham scan algorithm
        int currentIndex = leftMostIndex;
        int nextIndex;
        do
        {
            nextIndex = (currentIndex + 1) % points.Length;
            for (int i = 0; i < points.Length; i++)
            {
                if (Orientation(points[currentIndex], points[i], points[nextIndex]) < 0)
                {
                    nextIndex = i;
                }
            }

            currentIndex = nextIndex;
            hull.Add(points[currentIndex]);
        } while (currentIndex != leftMostIndex);

        return hull;
    }

    private float Orientation(Vector2 p, Vector2 q, Vector2 r)
    {
        return (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);
    }
}
