using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveDefultState : AdditiveMotionState
{
    private bool m_canJump = true;
    public override void Motion(PlayerInformation playerInformation)
    {
        if (!m_inputController.GetInputData.JumpInput)
        {
            m_canJump = true;
        }
        if (m_inputController.GetInputData.JumpInput && m_canJump && m_playerColliding.IsGround)
        {
            m_canJump = false;
            ChangeMoveState(new JumpState(playerInformation));
        }
    }
    
    public AdditiveDefultState(PlayerInformation information) : base(information)
    {
    }
}
