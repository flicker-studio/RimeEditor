using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOTIONSTATEENUM
{
    MainDefultState,
    WalkAndRunState,
    AdditiveDefultState,
    JumpState,
    PerpendicularGroundState,
    None
}

public abstract class MotionStateFactory
{
    public abstract MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, PlayerInformation playerInformation,
        CheckStatesCallBack checkStatesCallBack);
}
