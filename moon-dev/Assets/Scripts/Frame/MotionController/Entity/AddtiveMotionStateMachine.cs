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
            List<MotionState> tempList = new List<MotionState>();
            tempList.AddRange(m_motionStates);
            foreach (var state in tempList)
            {
                if ((state as AdditiveMotionState).IsEnd)
                {
                    m_motionStates.Remove(state);
                }
            }
            return;
        }
        MotionState motionState = CreateMotionState(playerMoveState, information);
        if(motionState == null) return;
        if (m_motionStates.Contains(motionState)) return;
        m_motionStates.Add(motionState);
    }


    public AddtiveMotionStateMachine(MotionCallBack motionCallBack): base(motionCallBack)
    {
        m_motionStateFactory = new AdditiveMotionStateFactory();
    }
}