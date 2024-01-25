using System.Collections.Generic;
using UnityEngine;

namespace Moon.Kernel.Extension
{
    public static class Vector
    {
        public static Vector2 NewX(this Vector2 vector2, float x)
        {
            return new Vector2(x, vector2.y);
        }

        public static Vector2 NewY(this Vector2 vector2, float y)
        {
            return new Vector2(vector2.x, y);
        }

        public static Vector3 NewX(this Vector3 vector3, float x)
        {
            return new Vector3(x, vector3.y, vector3.z);
        }

        public static Vector3 NewY(this Vector3 vector3, float y)
        {
            return new Vector3(vector3.x, y, vector3.z);
        }

        public static Vector3 NewZ(this Vector3 vector3, float z)
        {
            return new Vector3(vector3.x, vector3.y, z);
        }

        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        public static Vector2 GetOrthogonalVector(this Vector2 vector2)
        {
            Vector2 orthogonalVector;

            if (vector2.y != 0)
            {
                orthogonalVector = new Vector2(-vector2.y, vector2.x);
            }
            else // 如果nearestDirection的y坐标为0，那么正交向量就是(0, 1)或者(0, -1)，我们选择y大于0的
            {
                orthogonalVector = new Vector2(0, 1);
            }

            // 确保向量的y轴大于0
            if (orthogonalVector.y < 0)
            {
                orthogonalVector = -orthogonalVector;
            }

            return orthogonalVector.normalized;
        }

        public static Vector2 CalculateBestFitLine(this List<Vector2> points)
        {
            float xSum = 0, ySum = 0;

            foreach (var point in points)
            {
                xSum += point.x;
                ySum += point.y;
            }

            var xMean = xSum / points.Count;
            var yMean = ySum / points.Count;

            float numerator = 0, denominator = 0;

            for (var i = 0; i < points.Count; i++)
            {
                var xDiff = points[i].x - xMean;
                var yDiff = points[i].y - yMean;

                numerator += xDiff * yDiff;
                denominator += xDiff * xDiff;
            }

            var slope = numerator / denominator;
            var yIntercept = yMean - slope * xMean;

            var direction = new Vector2(1, slope).normalized;
            return direction;
        }

        public static Collider2D[] OverlapRotatedBox(this Vector2 center, Vector2 size, float angle)
        {
            var maxColliders = 50;
            var colliderBuffer = new Collider2D[maxColliders];
            var numColliders = Physics2D.OverlapBoxNonAlloc(center, size, angle, colliderBuffer);
            var colliders = new Collider2D[numColliders];
            for (var i = 0; i < numColliders; i++) colliders[i] = colliderBuffer[i];
            return colliders;
        }

        public static Vector3 GetCenterPoint(this List<Vector3> points)
        {
            var sum = Vector3.zero;
            foreach (var point in points) sum += point;
            return sum / points.Count;
        }

        public static Vector3 HadamardProduct(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 DivideVector(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector3 Pow(this Vector3 a, float pow)
        {
            return new Vector3(Mathf.Pow(a.x, pow), Mathf.Pow(a.y, pow), Mathf.Pow(a.z, pow));
        }

        public static Vector2 HadamardProduct(this Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 DivideVector(this Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static Vector3 Abs(this Vector3 value)
        {
            return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
        }
    }
}