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
}
