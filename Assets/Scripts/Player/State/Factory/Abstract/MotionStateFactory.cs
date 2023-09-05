using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOTIONSTATEENUM
{
    MainDefultState = 2,
    WalkAndRunState = 4,
    SlideState = 8,
    AdditiveDefultState = 16,
    JumpState = 32,
    PerpendicularGroundState = 64,
    None = 128
}

public abstract class MotionStateFactory
{
    public abstract MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, PlayerInformation playerInformation,
        CheckStatesCallBack checkStatesCallBack);
}
