using System.Collections.Generic;
using UnityEngine;

public static class TrianglePointsMethod
{
    /// <summary>
    /// 获取最大相交的三角形
    /// </summary>
    /// <param name="三角形列表"></param>
    /// <returns></returns>
    public static List<TrianglePoints> GetMaxTrianglePointsIndependentSet(this List<TrianglePoints> trianglePoints)
    {
        List<TrianglePoints> nonIntersectingTriangles = new List<TrianglePoints>();

        foreach (TrianglePoints newTriangle in trianglePoints)
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
    /// <summary>
    /// 判定相交
    /// </summary>
    /// <param name="三角形A"></param>
    /// <param name="三角形B"></param>
    /// <returns></returns>
    private static bool Intersects(TrianglePoints t1, TrianglePoints t2)
    {
        // 对于t1的每条边与t2的每条边，检查它们是否相交
        if (LineSegmentsIntersect(t1.PointX, t1.PointY, t2.PointX, t2.PointY) ||
            LineSegmentsIntersect(t1.PointX, t1.PointY, t2.PointX, t2.PointZ) ||
            LineSegmentsIntersect(t1.PointX, t1.PointY, t2.PointY, t2.PointZ) ||
            LineSegmentsIntersect(t1.PointX, t1.PointZ, t2.PointX, t2.PointY) ||
            LineSegmentsIntersect(t1.PointX, t1.PointZ, t2.PointX, t2.PointZ) ||
            LineSegmentsIntersect(t1.PointX, t1.PointZ, t2.PointY, t2.PointZ) ||
            LineSegmentsIntersect(t1.PointY, t1.PointZ, t2.PointX, t2.PointY) ||
            LineSegmentsIntersect(t1.PointY, t1.PointZ, t2.PointX, t2.PointZ) ||
            LineSegmentsIntersect(t1.PointY, t1.PointZ, t2.PointY, t2.PointZ))
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// 判断线段关系
    /// </summary>
    /// <param name="三角形A的Vector2.x"></param>
    /// <param name="三角形A的Vector2.y"></param>
    /// <param name="三角形B的Vector2.x"></param>
    /// <param name="三角形B的Vector2.y"></param>
    /// <returns></returns>
    private static bool LineSegmentsIntersect(Vector2 p, Vector2 p2, Vector2 q, Vector2 q2)
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
    /// <summary>
    /// 叉乘
    /// </summary>
    /// <param name="向量A"></param>
    /// <param name="向量B"></param>
    /// <returns></returns>
    private static float CrossProduct(Vector2 v1, Vector2 v2)
    {
        return v1.x * v2.y - v1.y * v2.x;
    }
}
