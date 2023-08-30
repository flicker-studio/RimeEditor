using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerColliding
{
    private bool m_isGround;

    private bool m_isCeiling;

    private Transform m_playerTransform;

    private CharacterProperty m_characterProperty;

    public bool IsGround
    {
        get
        {
            m_isGround = OverlapRotatedBox(m_playerTransform.position
                                           + m_playerTransform.up * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_RELATIVE_POSITION_Y
                , m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE,m_playerTransform.rotation.eulerAngles.z);
            return m_isGround;
        }
    }
    
    public bool IsCeiling
    {
        get
        {
            m_isCeiling = OverlapRotatedBox(m_playerTransform.position
                                            +  m_playerTransform.up * m_characterProperty.CeilingCheckParameter.CHECK_CAPSULE_RELATIVE_POSITION_Y
                , m_characterProperty.CeilingCheckParameter.CHECK_CAPSULE_SIZE,m_playerTransform.rotation.eulerAngles.z);
            return m_isCeiling;
        }
    }

    public PlayerColliding(Transform transform,CharacterProperty characterProperty)
    {
        m_playerTransform = transform;
        m_characterProperty = characterProperty;
    }
    
    private bool OverlapRotatedBox(Vector2 center, Vector2 size, float angle)
    {
        int maxColliders = 50;
        Collider2D[] colliderBuffer = new Collider2D[maxColliders];
        int numColliders = Physics2D.OverlapBoxNonAlloc(center, 
            size, angle,colliderBuffer);
        
        for (int i = 0; i < numColliders; i++)
        {
            if (colliderBuffer[i].gameObject.layer == GlobalSetting.LayerMasks.Ground)
            {
                return true;
            }
        }

        return false;
    }
    
}
