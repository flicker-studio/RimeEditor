using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerColliding
{
    private bool m_isGround;

    private Transform m_playerTransform;

    public bool IsGround
    {
        get
        {
            return m_isGround;
        }
    }

    PlayerColliding(Transform transform)
    {
        m_playerTransform = transform;
    }
    
    Collider2D[] OverlapRotatedBox(Vector3 center, Vector3 size, float angle)
    {
        int maxColliders = 50;
        Collider2D[] colliderBuffer = new Collider2D[maxColliders];
        int numColliders = Physics2D.OverlapCapsuleNonAlloc(center, 
            size / 2,CapsuleDirection2D.Vertical, angle,colliderBuffer);

        // Trim the colliderBuffer array
        Collider2D[] colliders = new Collider2D[numColliders];
        for (int i = 0; i < numColliders; i++)
        {
            colliders[i] = colliderBuffer[i];
            // Debug.Log($"裁切中枢检测到方块：{colliders[i].gameObject.name}");
        }

        return colliders;
    }
}
