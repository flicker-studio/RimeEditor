using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAdditiveMotionState : PlayerMotionState
{
    protected float m_endTimmer = 0;

    public bool IsEnd = false;
    
    protected void RemoveState()
    {
        IsEnd = true;
        ChangeMotionState(MOTIONSTATEENUM.None);
    }

    protected PlayerAdditiveMotionState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
    }
}
