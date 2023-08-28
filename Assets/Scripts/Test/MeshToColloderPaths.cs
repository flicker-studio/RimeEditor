using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(PolygonCollider2D))]
[ExecuteInEditMode]
public class MeshToColloderPaths
{
    //
    class Edge2D
    {
        public Vector2 A { get; private set; }
        public Vector2 B { get; private set; }

        public Edge2D (Vector2 pointA, Vector2 pointB)
        {
            A = pointA;
            B = pointB;
        }

        //重写Equals
        public override bool Equals(object obj)
        {
            if (obj is Edge2D)
            {
                var edge = (Edge2D)obj;
                return (edge.A == A && edge.B == B) || (edge.B == A && edge.A == B);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return A.GetHashCode() ^ B.GetHashCode();
        }
    }

    //
    public static List<Vector2[]> CreatePolygon2DColliderPoints(Mesh mesh)
    {
        Dictionary<Edge2D, int> edges = BuildEdgesFromMesh(mesh);
        List<Vector2[]> paths = BuildColliderPaths(edges);
        return paths;
    }

    // ------

    //获取mesh的三角形边
    static Dictionary<Edge2D, int> BuildEdgesFromMesh(Mesh mesh)
    {
        if (mesh == null)
            return null;
        //mesh的顶点
        Vector3[] tmpVerts = mesh.vertices;
        //mesh内包含的所有三角形
        int[] tmpTris = mesh.triangles;
        //用以保存mesh内所有的三角形边及数量
        Dictionary<Edge2D, int> edgeDic = new Dictionary<Edge2D, int>();
        //
        Vector3 tmpPoint0;
        Vector3 tmpPoint1;
        Vector3 tmpPoint2;
        for (int i = 0; i < tmpTris.Length - 2; i += 3)
        {
            //三角形顶点
            tmpPoint0 = tmpVerts[tmpTris[i]];
            tmpPoint1 = tmpVerts[tmpTris[i + 1]];
            tmpPoint2 = tmpVerts[tmpTris[i + 2]];
            //三角形边
            Edge2D[] tmpEdges = new Edge2D[] {
                new Edge2D(tmpPoint0, tmpPoint1),
                new Edge2D(tmpPoint1, tmpPoint2),
                new Edge2D(tmpPoint2, tmpPoint0)
            };
            //记录每条边及出现的次数
            for (int j = 0; j < tmpEdges.Length; j++)
            {
                //判断重复, 加入字典, 需要重写Edge2D的Equals函数
                if (edgeDic.ContainsKey(tmpEdges[j]))
                    edgeDic[tmpEdges[j]]++;
                else
                    edgeDic.Add(tmpEdges[j], 1);
            }
        }
        return edgeDic;
    }


    //计算轮廓点坐标
    static List<Vector2[]> BuildColliderPaths(Dictionary<Edge2D, int> allEdges)
    {
        if (allEdges == null)
            return null;
        //获取mesh外轮廓(三角形边没有重复使用, 不是公共边, 即视为外轮廓边)
        List<Edge2D> outEdges = new List<Edge2D>();
        foreach (var edge in allEdges.Keys)
        {
            if (allEdges[edge] == 1)
                outEdges.Add(edge);
        }
        //path列表, 对应Collider的paths, 包含多个path
        List<List<Edge2D>> pathList = new List<List<Edge2D>>();
        //单个path, 是一条(闭合的)折线
        List<Edge2D> path = null;
        //循环所有外轮廓边, 每个轮廓边都是一条线段, 若两条线段端点重合, 则可以连接这两条线段为一条折线
        while (outEdges.Count > 0)
        {
            //新建一个path
            if (path == null)
            {
                path = new List<Edge2D>();
                //加入首个线段到path, 并从外轮廓(线段)列表中移除
                path.Add(outEdges[0]);
                outEdges.RemoveAt(0);
                //将path添加到pathList
                pathList.Add(path);
            }
            //至少找到一条可连接线段
            bool foundAtLeastOneEdge = false;
            int i = 0;
            Edge2D tmeEdge;
            //循环判断所有线段
            while (outEdges.Count > i)
            {
                //依次取一条线段
                tmeEdge = outEdges[i];
                //如果该线段的终点与path折线的起点重合
                if (tmeEdge.B == path[0].A)
                {
                    //插入到path首位, 并从外轮廓(线段)列表中移除
                    path.Insert(0, tmeEdge);
                    outEdges.RemoveAt(i);
                    foundAtLeastOneEdge = true;
                }
                //如果该线段的起点与path折线的终点重合
                else if (tmeEdge.A == path[path.Count - 1].B)
                {
                    //添加到path的末位, 并从外轮廓(线段)列表中移除
                    path.Add(tmeEdge);
                    outEdges.RemoveAt(i);
                    foundAtLeastOneEdge = true;
                }
                //该线段不能连接到path, 跳过
                else
                {
                    i++;
                }
            }
            //如果没有找到能与首个线段连接的其他线段, 说明该线段是孤立的(不可用的线段), 将path置空
            if (!foundAtLeastOneEdge)
                path = null;
        }
        //整理pathList
        List<Vector2[]> cleanedPaths = new List<Vector2[]>();
        //循环
        for (int i = 0; i < pathList.Count; i++)
        {
            //判断空值
            if (pathList[i] == null)
                continue;
            //获取折线的折点
            List<Vector2> coords = new List<Vector2>();
            for (int j = 0; j < pathList[i].Count; j++)
            {
                coords.Add(pathList[i][j].A);
            }
            //去除多余点
            cleanedPaths.Add(RemoveUnusedCoords(coords));
        }
        //返回paths
        return cleanedPaths;
    }
    //去除多余坐标点，如果三点共线，去除中间点
    static Vector2[] RemoveUnusedCoords(List<Vector2> oldCoordsList)
    {
        //保存筛选之后的点
        List<Vector2> newCoordsList = new List<Vector2>();
        //添加第一个点
        newCoordsList.Add(oldCoordsList[0]);
        //标记下表
        int lastAddedIndex = 0;
        //循环判断
        for (int i = 1; i < oldCoordsList.Count; i++)
        {
            //取点
            Vector2 curCoords = oldCoordsList[i];
            Vector2 lastAddedCoords = oldCoordsList[lastAddedIndex];
            Vector2 nextCoords = (i + 1 >= oldCoordsList.Count) ? oldCoordsList[0] : oldCoordsList[i + 1];
            //判断共线
            if (!PointPosionOfLine(lastAddedCoords, nextCoords, curCoords))
            {
                newCoordsList.Add(curCoords);
                lastAddedIndex = i;
            }
        }
        return newCoordsList.ToArray();
    }

    //判断三点共线, 即点M在线段AB上
    static bool PointPosionOfLine(Vector2 A, Vector2 B, Vector2 M)
    {
        //只是判断位置, 这种算法比较简单, 还可以计算三角形面积(相对繁琐一些), 面积为0, 则三点共线
        return Mathf.Approximately((B.y - M.y) * (A.x - M.x) - (A.y - M.y) * (B.x - M.x), 0);

        //公式计算过程
        //直线公式：a * X + b * Y + c = 0
        //将线段端点代入公式
        // a * A.x + b * A.y + c = 0
        // a * B.x + b * B.y + c = 0
        //两式分别相加、相减
        // (A.x + B.x) * a + (A.y + B.y) * b + 2c = 0
        // (A.x - B.x) * a + (A.y - B.y) * b = 0
        //化简得
        // b = (B.x - A.x) / (A.y - B.y) * a
        // c = -a * (A.y * B.x - A.x * B.y) / (A.y - B.y)
        //原直线公式用a表示为
        // a * X + (B.x - A.x) / (A.y - B.y) * a * Y - a * (A.y * B.x - A.x * B.y) / (A.y - B.y) = 0
        //公式两边同时除a，直线公式用点A/B表示为
        // X + (B.x - A.x) / (A.y - B.y) * Y - (A.y * B.x - A.x * B.y) / (A.y - B.y) = 0
        //再次化简
        // (A.y - B.y) * X + (B.x - A.x) * Y - (A.y * B.x - A.x * B.y) = 0
        //公式左侧==0,点在直线上，公式左侧>0,点在直线右侧，工作左侧<0,点在直线左侧
        //将目标点M代入公式
        // (A.y - B.y) * M.x + (B.x - A.x) * M.y - (A.y * B.x - A.x * B.y)
        // A.y * M.x - B.y * M.x + B.x * M.y - A.x * M.y - A.y * B.x + A.x * B.y
        //整理为, 值 > 0 在右侧, = 0 在线上, < 0 在左侧
        // (B.y - M.y) * (A.x - M.x) - (A.y - M.y) * (B.x - M.x)
    }

}