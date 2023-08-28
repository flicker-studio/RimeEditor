using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AdjustColliderOrMesh : MonoBehaviour
{
    [SerializeField] bool useMass;
    [SerializeField] float massCoefficient = 1;
    [HideInInspector] public Vector3 centerPoint;

    private void Start()
    {
        ApplyMeshToCollider();
    }

    //按照PolygonCollider2D碰撞体的大小形状，创建MeshFilter网格
    public void ApplyColliderToMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        PolygonCollider2D polygon = GetComponent<PolygonCollider2D>();
        if (polygon == null || meshFilter == null)
            return;
        ApplyColliderToMesh(polygon, meshFilter);
        RecaculateMass();
        SaveInEditor();
    }

    //按照Mesh网格的大小形状，创建PolygonCollider2D碰撞体
    //只能是由一条闭合折线围成的网格，即不可以有内部孔或嵌套多个多边形
    public void ApplyMeshToCollider()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        PolygonCollider2D polygon = GetComponent<PolygonCollider2D>();
        if (polygon == null || meshFilter == null)
            return;
        ApplyMeshToCollider(meshFilter, polygon);
        RecaculateMass();
        SaveInEditor();
    }

    //按照Mesh网格的大小形状，创建PolygonCollider2D碰撞体
    //可以是由多条独立的闭合折线围成的网格，即可以有内部孔或嵌套多个多边形
    public void ApplyPathToCollider()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        PolygonCollider2D polygon = GetComponent<PolygonCollider2D>();
        if (polygon == null || meshFilter == null)
            return;
        ApplyPathToCollider(meshFilter, polygon);
        RecaculateMass();
        SaveInEditor();
    }

    //根据网格形状重新计算质量
    public void RecaculateMass()
    {
        if (useMass)
        {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (rigidBody == null || meshFilter == null)
                return;
            RecaculateMass(rigidBody, meshFilter);
        }
    }

    // ----------

    void SaveInEditor()
    {
#if UNITY_EDITOR
        //调用SetDirty方法保存修改部分
        EditorUtility.SetDirty(gameObject);
#endif
    }

    //按照面积重新计算质量
    void RecaculateMass(Rigidbody2D rigidBody, MeshFilter meshFilter)
    {
        float area = CalculateArea(meshFilter);
        rigidBody.mass = area * massCoefficient;
        SaveInEditor();
    }

    //计算多边形面积
    float CalculateArea(MeshFilter meshFilter)
    {
        float area = 0;
        //三角形顶点
        Vector3[] vertices = meshFilter.mesh.vertices;
        //三角形顶点索引
        int[] indices = meshFilter.mesh.GetIndices(0);
        //依次计算三角形面积, 累加 //每三个顶点确定一个三角形
        for (int i = 0; i < indices.Length; i += 3)
        {
            //获取三个顶点, 计算三角形面积, 累加
            area += CalculateTriangleArea(vertices[indices[i]], vertices[indices[i + 1]], vertices[indices[i + 2]]);
        }
        return area;
    }

    //计算三角形面积
    float CalculateTriangleArea(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float tmpArea = 0;
        tmpArea += p0.x * p1.y - p1.x * p0.y;
        tmpArea += p1.x * p2.y - p2.x * p1.y;
        tmpArea += p2.x * p0.y - p0.x * p2.y;
        tmpArea /= 2;
        tmpArea = Mathf.Abs(tmpArea);
        return tmpArea;
    }

    //计算三角形面积
    float CalculateTriangleArea02(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return p0.x * (p1.y - p2.y) + p1.x * (p2.y - p0.y) + p2.x * (p0.y - p1.y);
    }

    //计算三角形面积, 该方法在部分情况会有微小误差
    float CalculateTriangleArea03(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float a = (p1 - p0).magnitude;
        float b = (p2 - p1).magnitude;
        float c = (p0 - p2).magnitude;
        float p = (a + b + c) * 0.5f;
        return Mathf.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    // -----

    //按照PolygonCollider2D碰撞体的大小形状修改MeshFilter的显示网格
    void ApplyColliderToMesh(PolygonCollider2D polygon, MeshFilter meshFilter)
    {
        //坐标点转换
        Vector3[] newVertices = ConvertVertex(polygon.points);
        //创建网格
        Mesh newMesh = new Mesh();
        //重命名
        newMesh.name = transform.name;
        //设置顶点
        newMesh.vertices = newVertices;
        //获取三角形顶点索引数组
        Triangulator tr = new Triangulator(newVertices);
        //赋值
        newMesh.triangles = tr.Triangulate();
        //修改物体Mesh
        //mesh是值传递, 只会修改本物体的mesh
        meshFilter.mesh = newMesh;
        //sharedMesh是因用户传递, 会修改所有引用该mesh的物体
        //meshFilter.sharedMesh = mesh;

#if UNITY_EDITOR
        EditorUtility.SetDirty(meshFilter);
        EditorUtility.SetDirty(meshFilter.sharedMesh);
        EditorUtility.SetDirty(gameObject);
#endif
    }

    //Vector2转Vector3, 生成顶点, 中心点
    Vector3 tmpPoint;
    Vector3[] ConvertVertex(Vector2[] vertices)
    {
        tmpPoint = Vector3.zero;
        //Vector2转Vector3
        Vector3[] newVertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            newVertices[i] = vertices[i];
            centerPoint += newVertices[i];
        }
        //中心点, 取所有点的平均值
        centerPoint = tmpPoint / vertices.Length + transform.position;
        //返回
        return newVertices;
    }

    //-----

    //可以是凹多边形, 但不可以有内部孔或嵌套多个多边形, 用一段闭合折线记录
    void ApplyMeshToCollider(MeshFilter meshFilter, PolygonCollider2D polygon)
    {
        Vector3[] vertices = meshFilter.sharedMesh.vertices;
        Vector2[] points = new Vector2[vertices.Length];
        //读取网格数据
        for (int i = 0; i < vertices.Length; i++)
        {
            points[i] = vertices[i];
        }
        //赋值到碰撞体points
        polygon.points = points;
    }

    //-----

    //可以是凹多边形, 可以有内部孔或嵌套多个多边形, 用多段独立的闭合折线记录
    void ApplyPathToCollider(MeshFilter meshFilter, PolygonCollider2D polygon)
    {
        //解析网格数据
        List<Vector2[]> paths = MeshToColloderPaths.CreatePolygon2DColliderPoints(meshFilter.sharedMesh);
        if (paths == null)
            return;
        //赋值到碰撞体path
        polygon.pathCount = paths.Count;
        for (int i = 0; i < paths.Count; i++)
        {
            polygon.SetPath(i, paths[i]);
        }
    }

    // ------

    public static bool IsZero(float n)
    {
        return Mathf.Approximately(n, 0);
    }

}