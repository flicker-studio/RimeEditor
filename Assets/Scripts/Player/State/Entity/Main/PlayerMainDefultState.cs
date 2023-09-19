using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainDefultState : MainPlayerMotionState
{
    private float timmer = 0;

    private float m_oriSpeed;
    
    public override void Motion(BaseInformation information)
    {
        if (!CheckSuitableSlope)
        {
            GetRigidbody.Freeze(FREEZEAXIS.RotZ);
            ChangeMotionState(MOTIONSTATEENUM.SlideState);
            return;
        }
        if (GetMotionInputData.MoveInput.x != 0)
        {
            GetRigidbody.Freeze(FREEZEAXIS.RotZ);
            ChangeMotionState(MOTIONSTATEENUM.WalkAndRunState);
            return;
        }
        timmer += Time.fixedDeltaTime;
        if (timmer <= GetMoveProperty.GROUND_TIME_TO_STOP)
        {
            if (GetIsGround)
            {
                GetRigidbody.velocity = GetRigidbody.velocity.NewX(m_oriSpeed
                                                                   * (1-GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.GROUND_TIME_TO_STOP)));
            }
            else
            {
                GetRigidbody.velocity = GetRigidbody.velocity.NewX(m_oriSpeed
                                                                   * (1-GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.AIR_TIME_TO_STOP)));
            }
        }
        else
        {
            if (GetIsGround)
            {
                GetRigidbody.Freeze(FREEZEAXIS.PosXAndRotZ);
                return;
            }
            if (!CheckGlobalStates.Contains(typeof(JumpState)))
            {
                GetRigidbody.Freeze(FREEZEAXIS.RotZ);
            }
        }
    }


    public PlayerMainDefultState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
        m_oriSpeed = GetRigidbody.velocity.x;
    }
}
