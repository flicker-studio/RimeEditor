using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MeshMethod
{
    private static Vector3[] vertices;
    private static int[] triangles;
    private static Vector2[] vertices2D;
    private static List<TrianglePoints> trianglePointsList = new List<TrianglePoints>();
    
    public static void CreatePolygonCollider(this Mesh mesh,PolygonCollider2D polygonCollider2D)
    {
        vertices = mesh.vertices;
        triangles = mesh.triangles;
        vertices2D = new Vector2[vertices.Length];
        trianglePointsList.Clear();
        //二维化点
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices2D[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        //添加三角形
        for (int i = 0,j = 0; i < triangles.Length; i += 3,j ++)
        {
            TrianglePoints trianglePoints = new TrianglePoints(vertices2D[triangles[i]]
                ,vertices2D[triangles[i + 1]]
                ,vertices2D[triangles[i + 2]]);
            trianglePointsList.Add(trianglePoints);
        }
        //去掉相同三角形
        trianglePointsList = new List<TrianglePoints>(new HashSet<TrianglePoints>(trianglePointsList));
        //去掉非三角形
        trianglePointsList = trianglePointsList.Where(triangle => triangle.Judge() == false).ToList();
        //去掉相交的三角形
        trianglePointsList = trianglePointsList.GetMaxTrianglePointsIndependentSet();
        //设置三角形数组长度
        polygonCollider2D.pathCount = trianglePointsList.Count;
        //设置碰撞
        for (int i = 0; i < trianglePointsList.Count; i++)
        {
            polygonCollider2D.SetPath(i,new Vector2[]{trianglePointsList[i].PointX
                ,trianglePointsList[i].PointY,
                trianglePointsList[i].PointZ
            });   
        }
    }
}
