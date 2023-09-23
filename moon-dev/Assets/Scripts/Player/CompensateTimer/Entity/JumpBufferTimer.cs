using System.Collections;
using System.Collections.Generic;
using Character.Information;
using UnityEngine;

public class JumpBufferTimer
{
    private float m_timer = 0f;
    private bool m_jumpBufferFlag = false;
    private bool m_lastMoveInput = false;
    
    public bool CheckTimer(PlayerInformation playerInformation)
    {
        bool checkJumpBuffer = m_timer < playerInformation.CharacterProperty.JumpProperty.JUMPING_BUFFER_TIME 
                               && m_jumpBufferFlag && playerInformation.PlayerColliding.IsGround;
        
        if (checkJumpBuffer)
        {
            m_jumpBufferFlag = false;
        }
        
        if (playerInformation.MotionInputController.GetMotionInputData.JumpInput 
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
        m_lastMoveInput = playerInformation.MotionInputController.GetMotionInputData.JumpInput;

        return checkJumpBuffer;
    }
}
