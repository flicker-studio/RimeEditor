using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : MainMotionState
{
    private float m_timer = 0f;
    public SlideState(PlayerInformation information, CheckStatesCallBack checkStatesCallBack) : base(information, checkStatesCallBack)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        if (CheckSuitableSlope)
        {
            ChangeMoveState(MOTIONSTATEENUM.MainDefultState);
            return;
        }

        m_timer += Time.fixedDeltaTime;
        GetRigidbody.velocity = GetRigidbody.velocity.NewY(
            -GetMoveProperty.ACCELERATION_CURVE.Evaluate(m_timer / GetMoveProperty.SLICE_TIME_TO_MAXIMUN_SPEED)
            * GetMoveProperty.SLIDE_SPEED);
        GetRigidbody.velocity = GetRigidbody.velocity.NewX(0);
    }
}
