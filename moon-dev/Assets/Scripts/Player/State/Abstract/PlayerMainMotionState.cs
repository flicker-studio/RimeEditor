using System.Collections;
using System.Collections.Generic;
using Character.Information;
using Frame.StateMachine;
using UnityEngine;

public abstract class PlayerMainMotionState : MainMotionState
{
    protected PlayerInformation m_playerInformation;
    public PlayerMainMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_playerInformation = information as PlayerInformation;
    }
}
