using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlicerAdditiveMotionState : AdditiveMotionState
{
    protected SlicerInformation m_slicerInformation;

    protected ICommand m_sliceCommand;
    
    private bool m_firstExecute = true;

    protected bool GetFirstExecute
    {
        get
        {
            if (m_firstExecute)
            {
                m_firstExecute = false;
                return true;
            }

            return false;
        }
    }
    
    public SlicerAdditiveMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_slicerInformation = information as SlicerInformation;
    }
}
