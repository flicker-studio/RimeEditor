using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static Vector2 GetNearestLineDirection(this List<Vector2> points, Vector2 linePoint1, Vector2 linePoint2) 
    {
        Vector2 nearestDirection = Vector2.zero;
        float minDistance = float.MaxValue;
        
        foreach (Vector2 point in points) 
        {
            float distance = Mathf.Abs(
                                 (linePoint2.y - linePoint1.y) * point.x - 
                                 (linePoint2.x - linePoint1.x) * point.y + 
                                 linePoint2.x * linePoint1.y - 
                                 linePoint2.y * linePoint1.x) / 
                             Mathf.Sqrt(Mathf.Pow(linePoint2.y - linePoint1.y, 2) + Mathf.Pow(linePoint2.x - linePoint1.x, 2));
            
            if (distance < minDistance) 
            {
                minDistance = distance;
                nearestDirection = (point - linePoint1).normalized;
            }
        }
        
        return nearestDirection;
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

        return orthogonalVector;
    }
}
