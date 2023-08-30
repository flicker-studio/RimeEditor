using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAndRunState : MainMotionState
{
    private float timmer = 0f;
    
    public WalkAndRunState(PlayerInformation information) : base(information)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_inputController.GetInputData.MoveInput.x == 0)
        {
            ChangeMoveState(new MainDefultState(playerInformation));
            return;
        }
        timmer += Time.fixedDeltaTime;
        if (!GetInputData.RunInput)
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(GetInputData.MoveInput.x
            * GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED) 
            * GetMoveProperty.PLAYER_MOVE_SPEED);
        }
        else
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(GetInputData.MoveInput.x
            * GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED) 
            * GetMoveProperty.PLAYER_MOVE_RUN_SPEED);
        }
    }
}
