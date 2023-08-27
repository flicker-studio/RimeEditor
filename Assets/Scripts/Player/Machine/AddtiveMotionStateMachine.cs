using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtiveMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MotionState playerMoveState)
    {
        if (m_playerMoveStates.Contains(playerMoveState)) m_playerMoveStates.Remove(playerMoveState);
        m_playerMoveStates.Add(playerMoveState);
    }
}
