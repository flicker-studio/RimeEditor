using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotionStateMachine
{
    protected List<MotionState> m_playerMoveStates;

    public void Motion(PlayerInformation playerInformation)
    {
        List<MotionState> tempList = new List<MotionState>();
        tempList.AddRange(m_playerMoveStates);
        foreach (var motionState in tempList)
        {
            motionState.Motion(playerInformation);
        }
    }

    public MotionStateMachine()
    {
        m_playerMoveStates = new List<MotionState>();
    }

    public abstract void ChangeMotionState(MotionState playerMoveState);
}
