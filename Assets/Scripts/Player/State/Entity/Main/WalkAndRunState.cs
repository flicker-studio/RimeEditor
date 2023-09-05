using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAndRunState : MainMotionState
{
    private float timmer = 0f;
    

    public override void Motion(PlayerInformation playerInformation)
    {
        if (GetInputData.MoveInput.x == 0 
            || !CheckGlobalStates.Contains(typeof(JumpState)) 
            && GetRaycastCheckPoints.CalculateBestFitLine().GetOrthogonalVector().y 
            <= Mathf.Cos(GetPerpendicularOnGround.CHECK_POINT_ANGLE * Mathf.Deg2Rad))
        {
            if(GetIsGround) GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetMoveProperty.JELLY_EFFECT_COMPENSATION);
            ChangeMoveState(MOTIONSTATEENUM.MainDefultState);
            return;
        }

        TurnToHorizontalPlaneInAir();
        WalkAndRun();
    }

    private void TurnToHorizontalPlaneInAir()
    {
        if(GetIsGround) return;
        if (Mathf.Abs(GetRigidbody.transform.up.x) < Mathf.Sin(GetMoveProperty.AIR_ANGULAR_TOLERANCE_RANGE * Mathf.Deg2Rad))
        {
            GetRigidbody.transform.up = Vector3.up;
            return;
        }
        if (GetRigidbody.transform.up.x > 0)
        {
            GetRigidbody.transform.Rotate(0,0,GetMoveProperty.AIR_ANGULAR_VELOCITY_Z);
        }else if (GetRigidbody.transform.up.x < 0)
        {
            GetRigidbody.transform.Rotate(0,0,-GetMoveProperty.AIR_ANGULAR_VELOCITY_Z);
        }
    }
    
    private void WalkAndRun()
    {
        timmer += Time.fixedDeltaTime;
        float angle = Vector2.Angle(Vector2.up, GetRigidbody.transform.up) * Mathf.Deg2Rad;
        float magnification;
        if (GetIsGround)
        {
            magnification = timmer / GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED;
        }
        else
        {
            magnification = timmer / GetMoveProperty.AIR_TIME_TO_MAXIMUN_SPEED;
        }
        
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
            if (!CheckGlobalStates.Contains(typeof(JumpState)) && GetRigidbody.velocity.y >= 0)
            {
                GetRigidbody.velocity = GetRigidbody.velocity.NewY(0);
            } 
        }
    }

    public WalkAndRunState(PlayerInformation information, CheckStatesCallBack checkStatesCallBack) : base(information, checkStatesCallBack)
    {
    }
}
