using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MOTIONSTATEENUM playerMoveState,BaseInformation baseInformation)
    {
        MotionState motionState = CreateMotionState(playerMoveState, baseInformation);
        if(motionState == null) return;
        m_motionStates.Clear();
        m_motionStates.Add(motionState);
    }


    public MainMotionStateMachine(MotionCallBack motionCallBack): base(motionCallBack)
    {
        m_motionStateFactory = new MainMotionStateFactory();
    }
}
