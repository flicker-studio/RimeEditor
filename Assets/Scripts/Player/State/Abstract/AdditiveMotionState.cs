using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdditiveMotionState : MotionState
{
    protected float m_endTimmer = 0;

    public bool IsEnd = false;

    protected abstract void RemoveState();

    protected AdditiveMotionState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
    }
}
