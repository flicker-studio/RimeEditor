using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainMotionState : MotionState
{
    protected MainMotionState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
    }
}
