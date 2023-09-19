using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : MainPlayerMotionState
{
    private float m_timer = 0f;
    public SlideState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        if (CheckSuitableSlope)
        {
            ChangeMotionState(MOTIONSTATEENUM.MainDefultState);
            return;
        }

        m_timer += Time.fixedDeltaTime;
        GetRigidbody.velocity = GetRigidbody.velocity.NewY(
            -GetMoveProperty.ACCELERATION_CURVE.Evaluate(m_timer / GetMoveProperty.SLICE_TIME_TO_MAXIMUN_SPEED)
            * GetMoveProperty.SLIDE_SPEED);
        GetRigidbody.velocity = GetRigidbody.velocity.NewX(0);
    }
}
