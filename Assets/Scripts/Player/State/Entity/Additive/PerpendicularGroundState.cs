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
        if (GetIsGround)
        {
            m_raycastPoints = GetRaycastPoints;
            if(m_raycastPoints == null || m_raycastPoints.Count <= GetPerpendicularOnGround.NEGLECTED_POINTS) return;
            GetRigidbody.transform.up = m_raycastPoints.CalculateBestFitLine().GetOrthogonalVector();
        }
    }
}
