using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerReleaseState : SlicerAdditiveMotionState
{
    public SlicerReleaseState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        if (m_slicerInformation.GetNum1Down)
        {
            ChangeMotionState(MOTIONSTATEENUM.SlicerCloseState);
            RemoveState();
        }
    }
}
