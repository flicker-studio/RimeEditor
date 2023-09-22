using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMainMotionState : MainMotionState
{
    protected PlayerInformation m_playerInformation;
    public PlayerMainMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_playerInformation = information as PlayerInformation;
    }
}
