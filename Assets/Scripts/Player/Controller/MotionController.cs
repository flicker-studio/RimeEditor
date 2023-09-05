using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MotionController
{
    private List<MotionStateMachine> m_motionStateMachines;

    private PlayerInformation m_playerInformation;

    public MotionController(PlayerInformation playerInformation)
    {
        m_motionStateMachines = new List<MotionStateMachine>();
        EventCenterManager.Instance.AddEventListener<MOTIONSTATEENUM>(GameEvent.ChangeMoveState,ChangeMotionState);
        m_playerInformation = playerInformation;
    }
    
    public void Motion(PlayerInformation playerInformation)
    {
        List<MotionStateMachine> tempList = new List<MotionStateMachine>();
        tempList.AddRange(m_motionStateMachines);
        foreach (var motionStateMachine in tempList)
        {
            motionStateMachine.Motion(playerInformation);
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
            motionMachine = new MainMotionStateMachine();
            m_motionStateMachines.Add(motionMachine);
        }
        motionMachine.ChangeMotionState(motionStateEnum,m_playerInformation);
    }

    private void ChangeMotionStateInAdditiveMachine(MOTIONSTATEENUM motionStateEnum)
    {
        MotionStateMachine motionMachine = m_motionStateMachines.FirstOrDefault(state => state is AddtiveMotionStateMachine);   
        if (motionMachine == null)
        {
            motionMachine = new AddtiveMotionStateMachine();
            m_motionStateMachines.Add(motionMachine);
        }
        motionMachine.ChangeMotionState(motionStateEnum,m_playerInformation);
    }
}
