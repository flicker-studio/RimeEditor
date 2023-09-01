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
            if(GetIsGround) GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetMoveProperty.JELLY_EFFECT_COMPENSATION);
            ChangeMoveState(new MainDefultState(playerInformation));
            return;
        }
        timmer += Time.fixedDeltaTime;
        float angle = Vector2.Angle(Vector2.up, GetRigidbody.transform.up) * Mathf.Deg2Rad;
        float magnification = timmer / GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED;
        if (!GetInputData.RunInput)
        {
            SetSpeed(GetInputData.MoveInput.x
                     * GetMoveProperty.ACCELERATION_CURVE.Evaluate(magnification)
                     * GetMoveProperty.PLAYER_MOVE_SPEED,magnification,angle);
        }
        else
        {
            SetSpeed(GetInputData.MoveInput.x
                     * GetMoveProperty.ACCELERATION_CURVE.Evaluate(magnification)
                     * GetMoveProperty.PLAYER_MOVE_RUN_SPEED,magnification,angle);
        }
    }

    private void SetSpeed(float speed,float magnification,float angle)
    {
        if (GetIsGround)
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetMoveProperty.SLOP_Y_AXIS_SPEED_COMPENSATION 
                                                               * Mathf.Clamp(magnification,0,1));
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(speed * Mathf.Cos(angle));
        }
        else
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(speed);
        }
    }
}
