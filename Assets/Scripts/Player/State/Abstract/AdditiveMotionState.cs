using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdditiveMotionState : MotionState
{
    protected float m_endTimmer = 0;

    public bool IsEnd = false;
    
    protected void RemoveState()
    {
        IsEnd = true;
        ChangeMoveState(MOTIONSTATEENUM.None);
    }

    protected AdditiveMotionState(PlayerInformation information, CheckStatesCallBack checkStatesCallBack) : base(information, checkStatesCallBack)
    {
    }
}
