using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate List<Type> CheckStatesCallBack();
public abstract class MotionStateMachine
{
    
    protected List<MotionState> m_playerMoveStates;
    
    protected CheckStatesCallBack m_checkStatesCallBack;

    protected MotionStateFactory m_motionStateFactory;

    public void Motion(PlayerInformation playerInformation)
    {
        List<MotionState> tempList = new List<MotionState>();
        tempList.AddRange(m_playerMoveStates);
        foreach (var motionState in tempList)
        {
            motionState.Motion(playerInformation);
        }
    }

    public abstract void ChangeMotionState(MOTIONSTATEENUM playerMoveState,PlayerInformation playerInformation);

    protected List<Type> CheckStates()
    {
        List<Type> tempList = new List<Type>();
        foreach (var motionState in m_playerMoveStates)
        {
            tempList.Add(motionState.GetType());
        }
        return tempList;
    }

    protected MotionState CreateMotionState(MOTIONSTATEENUM motionStateEnum, PlayerInformation playerInformation)
    {
        return m_motionStateFactory.CreateMotion(motionStateEnum,playerInformation,m_checkStatesCallBack);
    }
}
