using System.Collections.Generic;
using Frame.Static.Extensions;
using UnityEngine;

namespace Character
{
    public class PlayerRaycasting
    {
        private List<Vector2> m_raycastPointsGround;
        
        private List<Vector2> m_raycastPointsCheck;
        
        public List<Vector2> GetRaycastPointsGround
        {
            get
            {
                RaycastToGround();
                return m_raycastPointsGround;
            }
        }
    
        public List<Vector2> GetRaycastPointsCheck
        {
            get
            {
                RaycastToCheck();
                return m_raycastPointsCheck;
            }
        }
        
        private CharacterProperty m_characterProperty;
    
        private ComponentController m_componentController;
    
        private Rigidbody2D GetRigidbody => m_componentController.Rigidbody;
    
        private CharacterProperty.PlayerPerpendicularOnGround GetPerpendicularOnGround =>
            m_characterProperty.PerpendicularOnGround;
        
        private CharacterProperty.PlayerGroundCheckParameter GetGroundCheck => 
            m_characterProperty.GroundCheckParameter;
    
        public PlayerRaycasting(CharacterProperty characterProperty,ComponentController componentController)
        {
            m_characterProperty = characterProperty;
            m_componentController = componentController;
        }
        
        private void RaycastToGround()
        {
            Vector2 startPoint = GetRigidbody.transform.position
                                 + GetRigidbody.transform.up * m_characterProperty.GroundCheckParameter
                                     .CHECK_CAPSULE_RELATIVE_POSITION_Y
                                 - GetRigidbody.transform.right * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE.x / 2;
            Vector2 endPoint = GetRigidbody.transform.position
                               + GetRigidbody.transform.up * m_characterProperty.GroundCheckParameter
                                   .CHECK_CAPSULE_RELATIVE_POSITION_Y
                               + GetRigidbody.transform.right * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE.x / 2;
            m_raycastPointsGround = Raycast.CastRaysBetweenPoints(startPoint
                , endPoint, GetPerpendicularOnGround.CHECK_RAYCAST_POINTS,GetPerpendicularOnGround.START_POINT_COMPENSATION,
                Vector2.down ,GetPerpendicularOnGround.CHECK_GROUND_RAYCAST_DISTANCE,GetPerpendicularOnGround.CHECK_POINT_ANGLE,
                GetGroundCheck.CHECK_LAYER);
        }
        
        private void RaycastToCheck()
        {
            Vector2 startPoint = GetRigidbody.transform.position
                                 + GetRigidbody.transform.up * m_characterProperty.GroundCheckParameter
                                     .CHECK_CAPSULE_RELATIVE_POSITION_Y
                                 - GetRigidbody.transform.right * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE.x / 2;
            Vector2 endPoint = GetRigidbody.transform.position
                               + GetRigidbody.transform.up * m_characterProperty.GroundCheckParameter
                                   .CHECK_CAPSULE_RELATIVE_POSITION_Y
                               + GetRigidbody.transform.right * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE.x / 2;
            m_raycastPointsCheck = Raycast.CastRaysBetweenPoints(startPoint
                , endPoint, GetPerpendicularOnGround.CHECK_RAYCAST_POINTS,GetPerpendicularOnGround.START_POINT_COMPENSATION,
                Vector2.down ,GetPerpendicularOnGround.CHECK_ANGLE_RAYCAST_DISTANCE,
                GetGroundCheck.CHECK_LAYER);
        }
    }

}