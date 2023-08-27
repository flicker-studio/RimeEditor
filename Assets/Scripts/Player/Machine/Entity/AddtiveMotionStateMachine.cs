using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtiveMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MotionState playerMoveState)
    {
        if (playerMoveState == null)
        {
            List<MotionState> tempList = new List<MotionState>();
            tempList.AddRange(m_playerMoveStates);
            foreach (var state in tempList)
            {
                if ((state as AdditiveMotionState).IsEnd)
                {
                    m_playerMoveStates.Remove(state);
                }
            }
            return;
        }
        if (m_playerMoveStates.Contains(playerMoveState)) m_playerMoveStates.Remove(playerMoveState);
        m_playerMoveStates.Add(playerMoveState);
    }
}
