using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAndRunState : MainPlayerMotionState
{
    private float m_timer = 0f;

    private float m_slopetimer = 0f;
    

    public override void Motion(BaseInformation playerInformation)
    {
        if (!CheckSuitableSlope)
        {
            m_slopetimer += Time.fixedDeltaTime;
            if (m_slopetimer >= GetMoveProperty.SLOPE_START_TIME_COMPENSATE)
            {
                if(GetIsGround) GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetMoveProperty.JELLY_EFFECT_COMPENSATION);
                ChangeMotionState(MOTIONSTATEENUM.SlideState);
                return;
            }
        }
        else
        {
            m_slopetimer = 0f;
        }
        
        if (GetMotionInputData.MoveInput.x == 0)
        {
            if(GetIsGround) GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetMoveProperty.JELLY_EFFECT_COMPENSATION);
            ChangeMotionState(MOTIONSTATEENUM.MainDefultState);
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
        m_timer += Time.fixedDeltaTime;
        float angle = Vector2.Angle(Vector2.up, GetRigidbody.transform.up) * Mathf.Deg2Rad;
        float magnification;
        if (GetIsGround)
        {
            magnification = Mathf.Clamp01(m_timer / GetMoveProperty.GROUND_TIME_TO_MAXIMUN_SPEED);
        }
        else
        {
            magnification = Mathf.Clamp01(m_timer / GetMoveProperty.AIR_TIME_TO_MAXIMUN_SPEED);
        }
        
        if (!GetMotionInputData.RunInput)
        {
            SetSpeed(GetMotionInputData.MoveInput.x
                     * GetMoveProperty.ACCELERATION_CURVE.Evaluate(magnification)
                     * GetMoveProperty.PLAYER_MOVE_SPEED,magnification,angle);
        }
        else
        {
            SetSpeed(GetMotionInputData.MoveInput.x
                     * GetMoveProperty.ACCELERATION_CURVE.Evaluate(magnification)
                     * GetMoveProperty.PLAYER_MOVE_RUN_SPEED,magnification,angle);
        }
    }

    private void SetSpeed(float speed,float magnification,float angle)
    {
        if (GetIsGround)
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(speed * Mathf.Cos(angle) * magnification);
            GetRigidbody.velocity = GetRigidbody.velocity.NewY(-Mathf.Abs(speed * Mathf.Sin(angle)) * 
                                                               GetMoveProperty.SLOP_Y_AXIS_SPEED_COMPENSATION *
                                                               magnification);
        }
        else
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(speed * magnification);
        }
    }

    public WalkAndRunState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
    }
    
}
