using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotionStateMachine
{
    protected List<MotionState> m_playerMoveStates;

    public void Motion()
    {
        foreach (var motionState in m_playerMoveStates)
        {
            motionState.Motion();
        }
    }

    public MotionStateMachine()
    {
        m_playerMoveStates = new List<MotionState>();
    }

    public abstract void ChangeMotionState(MotionState playerMoveState);
}
