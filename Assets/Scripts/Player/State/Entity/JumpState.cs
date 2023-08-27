using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AdditiveMotionState
{
    public JumpState(PlayerInformation information) : base(information)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        Debug.Log("JumpState");
        m_endTimmer += Time.fixedDeltaTime;
        if (m_endTimmer >= m_characterProperty.m_PlayerJumpProperty.PLAYER_JUMP_TIMMER)
        {
            RemoveState();
        }
    }
}
