using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotionState
{
    protected BaseInformation m_information;

    protected MotionCallBack m_motionCallBack;
    public abstract void Motion(BaseInformation information);
    
    protected void ChangeMotionState(MOTIONSTATEENUM motionStateEnum)
    {
        m_motionCallBack.ChangeMotionStateCallBack.Invoke(motionStateEnum);
    }
    
    public MotionState(BaseInformation information,MotionCallBack motionCallBack)
    {
        m_information = information;
        m_motionCallBack = motionCallBack;
    }
}
