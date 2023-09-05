using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveDefultState : AdditiveMotionState
{
    private bool m_canJump = true;

    private CoyoteTimer m_coyoteTimer;
    private JumpBufferTimer m_jumpBufferTimer;
    
    public override void Motion(PlayerInformation playerInformation)
    {
        bool checkCoyoteTimer = m_coyoteTimer.CheckTimer(playerInformation);
        bool checkJumpBufferTimer = m_jumpBufferTimer.CheckTimer(playerInformation);
        if (!GetInputData.JumpInput)
        {
            m_canJump = true;
        }
        if (checkJumpBufferTimer && m_canJump || GetInputData.JumpInput 
            && m_canJump 
            && (GetIsGround || checkCoyoteTimer))
        {
            m_canJump = false;
            
            ChangeMoveState(MOTIONSTATEENUM.JumpState);
        }
    }

    public AdditiveDefultState(PlayerInformation information, CheckStatesCallBack checkStatesCallBack) : base(information, checkStatesCallBack)
    {
        m_coyoteTimer = new CoyoteTimer();
        m_jumpBufferTimer = new JumpBufferTimer();
    }
}
