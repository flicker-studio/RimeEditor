using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public class NoneMotionState : MotionState
{
    public NoneMotionState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information){}
}
