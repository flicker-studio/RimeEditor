using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePolygonCollider : MonoBehaviour
{
    private Mesh m_mesh;
    private Vector3[] m_vertices;
    public List<Vector3> m_ansVertices = new List<Vector3>();

    private void Start()
    {
        m_mesh = GetComponent<MeshFilter>().sharedMesh;
        m_vertices = m_mesh.vertices;
    }

    private void Update()
    {
        m_ansVertices.Clear();
        
        for (int i = 0; i < m_vertices.Length - 1; i++)
        {
            float maxY = float.MinValue, minY = float.MaxValue;
            int maxIndex = 0, minIndex = 0;
            for (int j = 0; j < m_vertices.Length - 1; j++)
            {
                if (m_vertices[i].x == m_vertices[j].x)
                {
                    if (m_vertices[j].y < minY)
                    {
                        minY = m_vertices[j].y;
                        minIndex = j;
                    }

                    if (m_vertices[j].y > maxY)
                    {
                        maxY = m_vertices[j].y;
                        maxIndex = j;
                    }
                }
            }

            if (minIndex != maxIndex)
            {
                if(!m_ansVertices.Contains(m_vertices[minIndex])) m_ansVertices.Add(m_vertices[minIndex]);
                if(!m_ansVertices.Contains(m_vertices[maxIndex])) m_ansVertices.Add(m_vertices[maxIndex]);
            }
            else
            {
                if(!m_ansVertices.Contains(m_vertices[minIndex])) m_ansVertices.Add(m_vertices[minIndex]);
            }
        }
        m_ansVertices = SortClockwise(m_ansVertices);
        for (int i = 1; i < m_ansVertices.Count - 1; i++)
        {
            Debug.DrawLine(m_ansVertices[i-1],m_ansVertices[i],Color.red);
        }
        Debug.DrawLine(m_ansVertices[m_ansVertices.Count - 1],m_ansVertices[0],Color.red);
    }
    
    public List<Vector3> SortClockwise(List<Vector3> points)
    {
        // 找到所有点的重心（中心点）
        Vector2 center = Vector2.zero;
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Count;

        // 计算每个点相对于中心点的极角
        List<PointWithAngle> pointsWithAngles = new List<PointWithAngle>();
        foreach (Vector2 point in points)
        {
            float angle = Mathf.Atan2(point.y - center.y, point.x - center.x);
            PointWithAngle pointWithAngle = new PointWithAngle(point, angle);
            pointsWithAngles.Add(pointWithAngle);
        }

        // 使用极角对点进行排序
        pointsWithAngles.Sort();

        // 返回排序后的点列表
        List<Vector3> sortedPoints = new List<Vector3>();
        foreach (PointWithAngle pointWithAngle in pointsWithAngles)
        {
            sortedPoints.Add(pointWithAngle.point);
        }

        return sortedPoints;
    }

    // 用于存储点及其相对于中心点的极角的结构体
    private struct PointWithAngle : System.IComparable<PointWithAngle>
    {
        public Vector2 point;
        public float angle;

        public PointWithAngle(Vector2 point, float angle)
        {
            this.point = point;
            this.angle = angle;
        }

        public int CompareTo(PointWithAngle other)
        {
            if (angle < other.angle)
            {
                return -1;
            }
            else if (angle > other.angle)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
