using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtiveMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MOTIONSTATEENUM playerMoveState,BaseInformation information)
    {
        if (playerMoveState == MOTIONSTATEENUM.None)
        {
            List<PlayerMotionState> tempList = new List<PlayerMotionState>();
            tempList.AddRange(m_playerMoveStates);
            foreach (var state in tempList)
            {
                if ((state as PlayerAdditiveMotionState).IsEnd)
                {
                    m_playerMoveStates.Remove(state);
                }
            }
            return;
        }
        PlayerMotionState playerMotionState = CreateMotionState(playerMoveState, information);
        
        if (m_playerMoveStates.Contains(playerMotionState)) return;
        m_playerMoveStates.Add(playerMotionState);
    }


    public AddtiveMotionStateMachine(MotionCallBack motionCallBack): base(motionCallBack)
    {
        m_motionStateFactory = new AdditiveMotionStateFactory();
    }
}
