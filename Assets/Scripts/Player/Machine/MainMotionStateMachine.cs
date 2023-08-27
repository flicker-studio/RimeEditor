using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MotionState playerMoveState)
    {
        m_playerMoveStates.Clear();
        m_playerMoveStates.Add(playerMoveState);
    }
}
