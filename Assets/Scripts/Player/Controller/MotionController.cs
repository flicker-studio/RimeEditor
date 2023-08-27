using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MotionController
{
    private List<MotionStateMachine> m_motionStateMachines;

    public MotionController()
    {
        m_motionStateMachines = new List<MotionStateMachine>();
        EventCenterManager.Instance.AddEventListener<MotionState>(GameEvent.ChangeMoveState,ChangeMotionState);
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

    public void ChangeMotionState(MotionState motionState)
    {
        if (motionState as MainMotionState != null)
        {
            ChangeMotionStateInMainMachine(motionState);
        }
        else
        {
            ChangeMotionStateInAdditiveMachine(motionState);
        }
    }

    private void ChangeMotionStateInMainMachine(MotionState motionState)
    {
        MotionStateMachine motionMachine = m_motionStateMachines.FirstOrDefault(state => state is MainMotionStateMachine);   
        if (motionMachine == null)
        {
            motionMachine = new MainMotionStateMachine();
            m_motionStateMachines.Add(motionMachine);
        }
        motionMachine.ChangeMotionState(motionState);
    }

    private void ChangeMotionStateInAdditiveMachine(MotionState motionState)
    {
        MotionStateMachine motionMachine = m_motionStateMachines.FirstOrDefault(state => state is AddtiveMotionStateMachine);   
        if (motionMachine == null)
        {
            motionMachine = new AddtiveMotionStateMachine();
            m_motionStateMachines.Add(motionMachine);
        }
        motionMachine.ChangeMotionState(motionState);
    }
}
