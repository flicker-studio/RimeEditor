using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpendicularGroundState : AdditiveMotionState
{
    private List<Vector2> m_raycastPoints;
    public PerpendicularGroundState(PlayerInformation information) : base(information)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_playerColliding.IsGround)
        {
            Vector2 startPoint = GetRigidbody.transform.position
                                 + GetRigidbody.transform.up * m_characterProperty.GroundCheckParameter
                                     .CHECK_CAPSULE_RELATIVE_POSITION_Y
            - GetRigidbody.transform.right * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE.x;
            Vector2 endPoint = GetRigidbody.transform.position
                                 + GetRigidbody.transform.up * m_characterProperty.GroundCheckParameter
                                     .CHECK_CAPSULE_RELATIVE_POSITION_Y
                                 + GetRigidbody.transform.right * m_characterProperty.GroundCheckParameter.CHECK_CAPSULE_SIZE.x;
            m_raycastPoints = RaycastMethod.CastRaysBetweenPoints(startPoint
                , endPoint, GetOrthogonalOnGround.CHECK_RAYCAST_POINTS,GetOrthogonalOnGround.START_POINT_COMPENSATION,
                Vector2.down ,GetOrthogonalOnGround.CHECK_RAYCAST_DISTANCE,
                GetGroundCheck.CHECK_LAYER);
            if(m_raycastPoints.Count == 0) return;
            GetRigidbody.transform.up = m_raycastPoints.CalculateBestFitLine().GetOrthogonalVector();
        }
    }
}
