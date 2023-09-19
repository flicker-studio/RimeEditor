using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAdditiveMotionState : AdditiveMotionState
{
    protected PlayerInformation m_playerInformation;
    public PlayerAdditiveMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_playerInformation = information as PlayerInformation;
    }

    protected override void RemoveState()
    {
        IsEnd = true;
        ChangeMotionState(MOTIONSTATEENUM.None);
    }
}
