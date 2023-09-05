using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtiveMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MOTIONSTATEENUM playerMoveState,PlayerInformation playerInformation)
    {
        if (playerMoveState == MOTIONSTATEENUM.None)
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
        MotionState motionState = CreateMotionState(playerMoveState, playerInformation);
        
        if (m_playerMoveStates.Contains(motionState)) return;
        m_playerMoveStates.Add(motionState);
    }


    public AddtiveMotionStateMachine(CheckGlobalStatesCallBack checkGlobalStatesCallBack) : base(checkGlobalStatesCallBack)
    {
        m_motionStateFactory = new AdditiveMotionStateFactory();
    }
}
