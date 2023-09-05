using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMotionStateMachine : MotionStateMachine
{
    public override void ChangeMotionState(MOTIONSTATEENUM playerMoveState,PlayerInformation playerInformation)
    {
        m_playerMoveStates.Clear();
        m_playerMoveStates.Add(CreateMotionState(playerMoveState,playerInformation));
    }


    public MainMotionStateMachine(CheckGlobalStatesCallBack checkGlobalStatesCallBack) : base(checkGlobalStatesCallBack)
    {
        m_motionStateFactory = new MainMotionStateFactory();
    }
}
