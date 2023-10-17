using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Frame.Static.Extensions;
using UnityEngine;

namespace Frame.StateMachine
{
    public class MotionController
{
    private List<MotionStateMachine> m_motionStateMachines;

    private BaseInformation m_information;

    private MotionCallBack m_motionCallBack;

    private MainMotionStateFactory m_mainMotionStateFactory;

    private AdditiveMotionStateFactory m_additiveMotionStateFactory;

    public MotionController(BaseInformation information,MainMotionStateFactory mainMotionStateFactory
        ,AdditiveMotionStateFactory additiveMotionStateFactory)
    {
        m_motionStateMachines = new List<MotionStateMachine>();
        m_information = information;
        m_motionCallBack = new MotionCallBack
        {
            CheckGlobalStatesCallBack = CheckGlobalStates,
            ChangeMotionStateCallBack = ChangeMotionState
        };
        m_mainMotionStateFactory = mainMotionStateFactory;
        m_additiveMotionStateFactory = additiveMotionStateFactory;
    }
    
    public MotionController(BaseInformation information,AdditiveMotionStateFactory additiveMotionStateFactory)
    {
        m_motionStateMachines = new List<MotionStateMachine>();
        m_information = information;
        m_motionCallBack = new MotionCallBack
        {
            CheckGlobalStatesCallBack = CheckGlobalStates,
            ChangeMotionStateCallBack = ChangeMotionState
        };
        m_additiveMotionStateFactory = additiveMotionStateFactory;
    }
    
    public MotionController(BaseInformation information,MainMotionStateFactory mainMotionStateFactory)
    {
        m_motionStateMachines = new List<MotionStateMachine>();
        m_information = information;
        m_motionCallBack = new MotionCallBack
        {
            CheckGlobalStatesCallBack = CheckGlobalStates,
            ChangeMotionStateCallBack = ChangeMotionState
        };
        m_mainMotionStateFactory = mainMotionStateFactory;
    }
    
    public void Motion(BaseInformation baseInformation)
    {
        List<MotionStateMachine> tempList = new List<MotionStateMachine>();
        tempList.AddRange(m_motionStateMachines);
        foreach (var motionStateMachine in tempList)
        {
            motionStateMachine.Motion(baseInformation);
        }
    }

    public void ChangeMotionState(MOTIONSTATEENUM motionStateEnum)
    {
        if (motionStateEnum.CheckMotionIsMain())
        {
            ChangeMotionStateInMainMachine(motionStateEnum);
        }
        else
        {
            ChangeMotionStateInAdditiveMachine(motionStateEnum);
        }
    }

    private void ChangeMotionStateInMainMachine(MOTIONSTATEENUM motionStateEnum)
    {
        MotionStateMachine motionMachine = m_motionStateMachines.FirstOrDefault(state => state is MainMotionStateMachine);   
        if (motionMachine == null)
        {
            motionMachine = new MainMotionStateMachine(m_mainMotionStateFactory,m_motionCallBack);
            m_motionStateMachines.Add(motionMachine);
        }
        motionMachine.ChangeMotionState(motionStateEnum,m_information);
    }

    private void ChangeMotionStateInAdditiveMachine(MOTIONSTATEENUM motionStateEnum)
    {
        MotionStateMachine motionMachine = m_motionStateMachines.FirstOrDefault(state => state is AddtiveMotionStateMachine);   
        if (motionMachine == null)
        {
            motionMachine = new AddtiveMotionStateMachine(m_additiveMotionStateFactory,m_motionCallBack);
            m_motionStateMachines.Add(motionMachine);
        }
        motionMachine.ChangeMotionState(motionStateEnum,m_information);
    }

    private List<Type> CheckGlobalStates()
    {
        List<Type> tempList = new List<Type>();
        foreach (var motionStateMachine in m_motionStateMachines)
        {
            tempList.AddRange(motionStateMachine.CheckStates());
        }

        return tempList;
    }
}

}