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
            ChangeMoveState(new WalkAndRunState(playerInformation));
        }
        
        timmer += Time.fixedDeltaTime;
        GetRigidbody.velocity = GetRigidbody.velocity.NewX(m_oriSpeed
            * (1-GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.GROUND_TIME_TO_STOP)));
    }

    public MainDefultState(PlayerInformation information) : base(information)
    {
        m_oriSpeed = GetRigidbody.velocity.x;
    }
}
