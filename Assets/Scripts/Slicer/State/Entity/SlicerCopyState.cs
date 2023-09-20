using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerCopyState : SlicerAdditiveMotionState
{
    public SlicerCopyState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_sliceCommand = new CopySlicer(m_slicerInformation);
    }

    public override void Motion(BaseInformation information)
    {
        if (GetFirstExecute)
        {
            m_sliceCommand.Execute();
        }
        if (m_slicerInformation.GetNum1Down)
        {
            ChangeMotionState(MOTIONSTATEENUM.SlicerReleaseState);
            RemoveState();
        }
    }
}
