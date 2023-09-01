using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycasting
{
    private List<Vector2> m_raycastPoints;

    public List<Vector2> GetRaycastPoints
    {
        get
        {
            RaycastToGround();
            return m_raycastPoints;
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
        m_raycastPoints = RaycastMethod.CastRaysBetweenPoints(startPoint
            , endPoint, GetPerpendicularOnGround.CHECK_RAYCAST_POINTS,GetPerpendicularOnGround.START_POINT_COMPENSATION,
            Vector2.down ,GetPerpendicularOnGround.CHECK_RAYCAST_DISTANCE,GetPerpendicularOnGround.CHECK_POINT_ANGLE,
            GetGroundCheck.CHECK_LAYER);
    }
}
