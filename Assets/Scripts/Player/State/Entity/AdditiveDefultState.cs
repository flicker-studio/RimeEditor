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
        if (!m_inputController.GetInputData.JumpInput)
        {
            m_canJump = true;
        }
        if (checkJumpBufferTimer && m_canJump || m_inputController.GetInputData.JumpInput 
            && m_canJump 
            && (m_playerColliding.IsGround || checkCoyoteTimer))
        {
            m_canJump = false;
            ChangeMoveState(new JumpState(playerInformation));
        }
    }
    public AdditiveDefultState(PlayerInformation information) : base(information)
    {
        m_coyoteTimer = new CoyoteTimer();
        m_jumpBufferTimer = new JumpBufferTimer();
    }
}
