using System.Collections.Generic;
using System.Linq;
using Struct;
using UnityEngine;
using Plane = UnityEngine.Plane;

namespace Slicer
{
    public static class MeshMethod
    {
        private static Vector3[] vertices;

        private static int[] triangles;

        private static Vector2[] vertices2D;

        public static void CreatePolygonCollider(this Mesh mesh, PolygonCollider2D polygonCollider2D)
        {
            List<TrianglePoints> trianglePointsList = new List<TrianglePoints>();
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
            for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
            {
                TrianglePoints trianglePoints = new TrianglePoints(vertices2D[triangles[i]]
                    , vertices2D[triangles[i + 1]]
                    , vertices2D[triangles[i + 2]]);

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
                polygonCollider2D.SetPath(i, new Vector2[]
                {
                    trianglePointsList[i].PointX, trianglePointsList[i].PointY,
                    trianglePointsList[i].PointZ
                });
            }
        }

        public static void CreatePolygonCollider(this PolygonCollider2D polygonCollider2D
            , PolygonCollider2D targetCollider2D, Plane slicerPlane)
        {
            List<Vector2[]> newPaths = new List<Vector2[]>();

            bool[] needReserves = new bool[targetCollider2D.pathCount];

            for (int i = 0; i < targetCollider2D.pathCount; i++)
            {
                Vector2[] path = (Vector2[])targetCollider2D.GetPath(i).Clone();

                List<Vector2> positiveSidePath = new List<Vector2>();
                List<Vector2> negativeSidePath = new List<Vector2>();

                for (var j = 0; j < path.Length; j++)
                {
                    path[j] = targetCollider2D.transform.TransformPoint(path[j]);
                    (slicerPlane.GetSide(path[j]) ? positiveSidePath : negativeSidePath).Add(path[j]);
                }

                Vector2 linePoint;
                Vector2 lineOne;
                Vector2 lineTwo;
                Vector2 intersectPointOne;
                Vector2 intersectPointTwo;
                float denomOne;
                float denomTwo;
                float tOne;
                float tTwo;

                switch (positiveSidePath.Count)
                {
                    case 0:
                        break;
                    case 1:
                        linePoint = positiveSidePath[0];
                        lineOne = negativeSidePath[0] - linePoint;
                        denomOne = Vector3.Dot(slicerPlane.normal, lineOne);
                        tOne = (-slicerPlane.GetDistanceToPoint(linePoint)) / denomOne;
                        intersectPointOne = linePoint + lineOne * tOne;
                        lineTwo = negativeSidePath[1] - linePoint;
                        denomTwo = Vector3.Dot(slicerPlane.normal, lineTwo);
                        tTwo = (-slicerPlane.GetDistanceToPoint(linePoint)) / denomTwo;
                        intersectPointTwo = linePoint + lineTwo * tTwo;
                        Vector2[] newPath = new[] { linePoint, intersectPointOne, intersectPointTwo };

                        for (var index = 0; index < newPath.Length; index++)
                        {
                            newPath[index] = targetCollider2D.transform.InverseTransformPoint(newPath[index]);
                        }

                        newPaths.Add(newPath);
                        break;
                    case 2:
                        linePoint = negativeSidePath[0];
                        lineOne = positiveSidePath[0] - linePoint;
                        denomOne = Vector3.Dot(slicerPlane.normal, lineOne);
                        tOne = (-slicerPlane.GetDistanceToPoint(linePoint)) / denomOne;
                        intersectPointOne = linePoint + lineOne * tOne;
                        lineTwo = positiveSidePath[1] - linePoint;
                        denomTwo = Vector3.Dot(slicerPlane.normal, lineTwo);
                        tTwo = (-slicerPlane.GetDistanceToPoint(linePoint)) / denomTwo;
                        intersectPointTwo = linePoint + lineTwo * tTwo;

                        (Vector2[] newPathOne, Vector2[] newPathTwo) =
                            CreateTriangles(intersectPointOne, intersectPointTwo, positiveSidePath[0], positiveSidePath[1]);

                        for (var index = 0; index < newPathOne.Length; index++)
                        {
                            newPathOne[index] = targetCollider2D.transform.InverseTransformPoint(newPathOne[index]);
                        }

                        for (var index = 0; index < newPathTwo.Length; index++)
                        {
                            newPathTwo[index] = targetCollider2D.transform.InverseTransformPoint(newPathTwo[index]);
                        }

                        newPaths.Add(newPathOne);
                        newPaths.Add(newPathTwo);
                        break;
                    case 3:
                        needReserves[i] = true;
                        break;
                }
            }

            for (int i = 0; i < targetCollider2D.pathCount; i++)
            {
                if (needReserves[i]) newPaths.Add(targetCollider2D.GetPath(i));
            }

            polygonCollider2D.pathCount = newPaths.Count;

            for (var i = 0; i < newPaths.Count; i++)
            {
                polygonCollider2D.SetPath(i, newPaths[i]);
            }
        }

        static (Vector2[], Vector2[]) CreateTriangles(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            List<Vector2> points = new List<Vector2>() { p1, p2, p3, p4 };

            Vector2 topRightPoint = new Vector2(-float.MaxValue, -float.MaxValue);
            Vector2 bottomLeftPoint = new Vector2(float.MaxValue, float.MaxValue);

            foreach (var point in points)
            {
                if (point.x >= topRightPoint.x && point.y >= topRightPoint.y)
                {
                    topRightPoint = point;
                }

                if (point.x <= bottomLeftPoint.x && point.y <= bottomLeftPoint.y)
                {
                    bottomLeftPoint = point;
                }
            }

            int count = 2;

            for (int i = points.Count - 1; i >= 0; i--)
            {
                if (points[i] == topRightPoint || points[i] == bottomLeftPoint)
                {
                    if (count <= 0) continue;

                    points.RemoveAt(i);
                    count--;
                }
            }

            return (new[] { topRightPoint, points[0], points[1] },
                new[] { bottomLeftPoint, points[0], points[1] });
        }
    }
}