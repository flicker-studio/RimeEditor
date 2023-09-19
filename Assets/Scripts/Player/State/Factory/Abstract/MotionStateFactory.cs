using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOTIONSTATEENUM
{
    None = 1<<0,
    MainDefultState = 1<<1,
    WalkAndRunState = 1<<2,
    SlideState = 1<<3,
    AdditiveDefultState = 1<<4,
    JumpState = 1<<5,
    PerpendicularGroundState = 1<<6
}

public abstract class MotionStateFactory
{
    public abstract PlayerMotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, BaseInformation information,
        MotionCallBack motionCallBack);
}
