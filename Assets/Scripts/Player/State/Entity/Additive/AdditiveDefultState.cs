using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveDefultState : PlayerAdditiveMotionState
{
    private bool m_canJump = true;

    private CoyoteTimer m_coyoteTimer;
    private JumpBufferTimer m_jumpBufferTimer;

    #region GetProperty

    private MotionInputData GetMotionInputData => m_playerInformation.GetMotionInputData;

    private bool GetIsGround => m_playerInformation.GetIsGround;

    private bool CheckSuitableSlope => m_playerInformation.CheckSuitableSlope;

    #endregion
    
    public override void Motion(BaseInformation information)
    {
        bool checkCoyoteTimer = m_coyoteTimer.CheckTimer(information as PlayerInformation);
        bool checkJumpBufferTimer = m_jumpBufferTimer.CheckTimer(information as PlayerInformation);
        if (!GetMotionInputData.JumpInput)
        {
            m_canJump = true;
        }
        if (checkJumpBufferTimer && m_canJump || GetMotionInputData.JumpInput 
            && m_canJump 
            && (GetIsGround || checkCoyoteTimer)
            && CheckSuitableSlope)
        {
            m_canJump = false;
            
            ChangeMotionState(MOTIONSTATEENUM.JumpState);
        }
    }

    public AdditiveDefultState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
        m_coyoteTimer = new CoyoteTimer();
        m_jumpBufferTimer = new JumpBufferTimer();
    }
}
