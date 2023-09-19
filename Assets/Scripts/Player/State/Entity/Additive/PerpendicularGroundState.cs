using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpendicularGroundState : PlayerAdditiveMotionState
{
    private List<Vector2> m_raycastPoints;
    

    public override void Motion(BaseInformation information)
    {
        if (GetIsGround)
        {
            m_raycastPoints = GetRaycastGroundPoints;
            if(m_raycastPoints == null || m_raycastPoints.Count <= GetPerpendicularOnGround.NEGLECTED_POINTS) return;
            GetRigidbody.transform.up = m_raycastPoints.CalculateBestFitLine().GetOrthogonalVector();
        }
    }

    public PerpendicularGroundState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
    }
}
