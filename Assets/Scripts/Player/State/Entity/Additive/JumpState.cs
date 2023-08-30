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
        m_endTimmer += Time.fixedDeltaTime;
        GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetJumpProperty.PLAYER_MAXIMAL_JUMP_SPEED *
            (1 - GetJumpProperty.ACCELERATION_CURVE.Evaluate(m_endTimmer / GetJumpProperty.PLAYER_MAXIMAL_JUMP_TIME)));
        if (m_endTimmer >= GetJumpProperty.PLAYER_MAXIMAL_JUMP_TIME ||
            m_endTimmer >= GetJumpProperty.PLAYER_SMALLEST_JUMP_TIME && !GetInputData.JumpInput ||
            m_playerColliding.IsCeiling)
        {
            GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetJumpProperty.PLAYER_JUMP_FINISH_SPEED_COMPENSATION);
            RemoveState();
        }
    }
}
