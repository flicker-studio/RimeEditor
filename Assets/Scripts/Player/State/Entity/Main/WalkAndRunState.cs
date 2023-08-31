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
        float angle = Vector2.Angle(Vector2.up, GetRigidbody.transform.up) * Mathf.Deg2Rad;
        if (!GetInputData.RunInput)
        {
            SetSpeed(GetInputData.MoveInput.x
                     * GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer /
                                                                   GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED)
                     * GetMoveProperty.PLAYER_MOVE_SPEED,angle);
        }
        else
        {
            SetSpeed(GetInputData.MoveInput.x
                     * GetMoveProperty.ACCELERATION_CURVE.Evaluate(timmer /
                                                                   GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED)
                     * GetMoveProperty.PLAYER_MOVE_RUN_SPEED,angle);
        }
    }

    private void SetSpeed(float speed,float angle)
    {
        if (GetIsGround)
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(speed * Mathf.Cos(angle));
            GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetMoveProperty.SLOP_Y_AXIS_SPEED_COMPENSATION);
        }
        else
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(speed);
        }
    }
}
