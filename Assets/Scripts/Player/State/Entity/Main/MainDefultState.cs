using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDefultState : MainMotionState
{
    private float timmer = 0;

    private float m_oriSpeed;
    
    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_inputController.GetInputData.MoveInput.x != 0)
        {
            GetRigidbody.Freeze(FREEZEAXIS.RotZ);
            ChangeMoveState(MOTIONSTATEENUM.WalkAndRunState);
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
            GetRigidbody.Freeze(FREEZEAXIS.PosXAndRotZ);
        }
    }


    public MainDefultState(PlayerInformation information, CheckStatesCallBack checkStatesCallBack) : base(information, checkStatesCallBack)
    {
        m_oriSpeed = GetRigidbody.velocity.x;
    }
}
