using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MainMotionState
{
    private float timmer = 0f;
    
    public WalkState(PlayerInformation information) : base(information)
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
        GetRigidbody.velocity = GetRigidbody.velocity.NewX(GetInputData.MoveInput.x
                                                          * GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer/GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED) 
                                                          * GetMoveProperty.PLAYER_MOVE_SPEED);
        Debug.Log(GetRigidbody.velocity.x);
    }
}
