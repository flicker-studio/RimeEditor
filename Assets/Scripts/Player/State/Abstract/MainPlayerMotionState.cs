using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainPlayerMotionState : PlayerMotionState
{
    protected MainPlayerMotionState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
    {
    }
}
