using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MainMotionState
{
    public WalkState(PlayerInformation information) : base(information)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_inputController.GetInputData.MoveInput.x == 0)
        {
            ChangeMoveState(new DefultState(playerInformation));
        }
        m_componentController.Rigidbody.velocity = 
            m_inputController.GetInputData.MoveInput 
            * m_characterProperty.m_playerMoveProperty.PLAYER_MOVE_SPEED;
        Debug.Log("WalkState");
    }
}
