using System.Collections;
using System.Collections.Generic;using System.Security.Cryptography;
using UnityEngine;

public abstract class MotionState
{
    protected InputController m_inputController;

    protected ComponentController m_componentController;

    protected CharacterProperty m_characterProperty;

    public MotionState(PlayerInformation information)
    {
        m_inputController = information.InputController;
        m_componentController = information.ComponentController;
        m_characterProperty = information.CharacterProperty;
    }
    public abstract void Motion(PlayerInformation playerInformation);
    
    protected void ChangeMoveState(MotionState motionState)
    {
        EventCenterManager.Instance.EventTrigger<MotionState>(GameEvent.ChangeMoveState,motionState);
    }
}
