using System;
using System.Collections.Generic;

public abstract class MotionStateMachine
{
    protected List<PlayerMotionState> m_playerMoveStates;

    protected MotionCallBack m_motionCallBack;

    protected MotionStateFactory m_motionStateFactory;

    public void Motion(BaseInformation baseInformation)
    {
        List<PlayerMotionState> tempList = new List<PlayerMotionState>();
        tempList.AddRange(m_playerMoveStates);
        foreach (var motionState in tempList)
        {
            motionState.Motion(baseInformation);
        }
    }

    public abstract void ChangeMotionState(MOTIONSTATEENUM playerMoveState,BaseInformation baseInformation);
    
    public List<Type> CheckStates()
    {
        List<Type> tempList = new List<Type>();
        foreach (var motionState in m_playerMoveStates)
        {
            tempList.Add(motionState.GetType());
        }
        return tempList;
    }

    protected PlayerMotionState CreateMotionState(MOTIONSTATEENUM motionStateEnum, BaseInformation information)
    {
        return m_motionStateFactory.CreateMotion(motionStateEnum,information, m_motionCallBack);
    }

    public MotionStateMachine(MotionCallBack motionCallBack)
    {
        m_playerMoveStates = new List<PlayerMotionState>();
        m_motionCallBack = motionCallBack;
        m_motionCallBack.CheckStatesCallBack = CheckStates;
    }
}
