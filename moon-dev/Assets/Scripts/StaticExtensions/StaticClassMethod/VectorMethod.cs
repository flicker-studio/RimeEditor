using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public static class VectorMethod
{
    public static Vector2 NewX(this Vector2 vector2,float x)
    {
        return new Vector2(x, vector2.y);
    }
    
    public static Vector2 NewY(this Vector2 vector2,float y)
    {
        return new Vector2(vector2.x,y);
    }
    
    public static Vector3 NewX(this Vector3 vector3,float x)
    {
        return new Vector3(x, vector3.y,vector3.z);
    }
    
    public static Vector3 NewY(this Vector3 vector3,float y)
    {
        return new Vector3(vector3.x,y,vector3.z);
    }
    
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x,vector3.y);
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

        for (int i = 0; i < points.Count; i++)
        {
            var xDiff = points[i].x  - xMean;
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
        int maxColliders = 50;
        Collider2D[] colliderBuffer = new Collider2D[maxColliders];
        int numColliders = Physics2D.OverlapBoxNonAlloc(center, size,angle, colliderBuffer);
        Collider2D[] colliders = new Collider2D[numColliders];
        for (int i = 0; i < numColliders; i++)
        {
            colliders[i] = colliderBuffer[i];
        }
        return colliders;
    }

}
