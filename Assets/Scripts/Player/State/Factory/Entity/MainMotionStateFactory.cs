using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMotionStateFactory : MotionStateFactory
{
    public override MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, PlayerInformation playerInformation,
        CheckStatesCallBack checkStatesCallBack)
    {
        switch (motionStateEnum)
        {
            case MOTIONSTATEENUM.MainDefultState:
                return new MainDefultState(playerInformation, checkStatesCallBack);
            case MOTIONSTATEENUM.WalkAndRunState:
                return new WalkAndRunState(playerInformation, checkStatesCallBack);
            case MOTIONSTATEENUM.SlideState:
                return new SlideState(playerInformation, checkStatesCallBack);
            default:
                return null;
        }
    }
}
