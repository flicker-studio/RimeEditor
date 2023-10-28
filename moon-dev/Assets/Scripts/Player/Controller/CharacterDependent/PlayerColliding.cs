using Frame.Static.Extensions;
using UnityEngine;

namespace Character
{
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
                m_isGround = CheckBox(m_playerTransform.position
                                      + m_playerTransform.up * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_RELATIVE_POSITION_Y
                    , m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE,m_playerTransform.rotation.eulerAngles.z);
                return m_isGround;
            }
        }
        
        public bool IsCeiling
        {
            get
            {
                m_isCeiling = CheckBox(m_playerTransform.position
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
    
        private bool CheckBox(Vector2 center, Vector2 size, float angle)
        {
            Collider2D[] colliderBuffer = center.OverlapRotatedBox(size, angle);
             for (int i = 0; i < colliderBuffer.Length; i++)
             {
                 if (m_characterProperty.GroundCheckParameter.CHECK_LAYER.ContainsLayer(colliderBuffer[i].gameObject.layer))
                 {
                     return true;
                 }
             }
             return false;
        }
        
    }
}
