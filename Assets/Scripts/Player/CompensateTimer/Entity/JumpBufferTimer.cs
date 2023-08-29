using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBufferTimer : CompensateTimer
{
    private bool m_jumpBufferFlag = false;
    private bool m_lastMoveInput = false;
    
    public override bool CheckTimer(PlayerInformation playerInformation)
    {
        bool checkJumpBuffer = m_timer < playerInformation.CharacterProperty.JumpProperty.JUMPING_BUFFER_TIME 
                               && m_jumpBufferFlag && playerInformation.PlayerColliding.IsGround;
        
        if (checkJumpBuffer)
        {
            m_jumpBufferFlag = false;
        }
        
        if (playerInformation.InputController.GetInputData.JumpInput 
            && !m_lastMoveInput  && !playerInformation.PlayerColliding.IsGround && !m_jumpBufferFlag)
        {
            m_jumpBufferFlag = true;
            m_timer = 0f;
        }

        if (m_jumpBufferFlag && !playerInformation.PlayerColliding.IsGround)
        {
            m_timer += Time.fixedDeltaTime;
        }
        else
        {
            m_timer = playerInformation.CharacterProperty.JumpProperty.JUMPING_BUFFER_TIME;
        }
        m_lastMoveInput = playerInformation.InputController.GetInputData.JumpInput;

        return checkJumpBuffer;
    }
}
