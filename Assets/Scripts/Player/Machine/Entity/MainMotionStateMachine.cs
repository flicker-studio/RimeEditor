using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MOTIONSTATEENUM playerMoveState,BaseInformation baseInformation)
    {
        m_playerMoveStates.Clear();
        m_playerMoveStates.Add(CreateMotionState(playerMoveState,baseInformation));
    }


    public MainMotionStateMachine(MotionCallBack motionCallBack): base(motionCallBack)
    {
        m_motionStateFactory = new MainMotionStateFactory();
    }
}
