using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MainMotionState
{
    private float timmer = 0f;
    public WalkState(PlayerInformation information) : base(information)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_inputController.GetInputData.MoveInput.x == 0)
        {
            ChangeMoveState(new MainDefultState(playerInformation));
        }

        timmer += Time.fixedDeltaTime;
        m_componentController.Rigidbody.velocity = m_componentController.Rigidbody.velocity.NewX(
            m_inputController.GetInputData.MoveInput.x
            * m_characterProperty.m_playerMoveProperty.PLAYER_MOVE_SPEED);
    }
}
