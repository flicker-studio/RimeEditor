using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveMotionStateFactory : MotionStateFactory
{
    public override MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, PlayerInformation playerInformation,
        CheckStatesCallBack checkStatesCallBack)
    {
        switch (motionStateEnum)
        {
            case MOTIONSTATEENUM.AdditiveDefultState:
                return new AdditiveDefultState(playerInformation, checkStatesCallBack);
            case MOTIONSTATEENUM.JumpState:
                return new JumpState(playerInformation, checkStatesCallBack);
            case MOTIONSTATEENUM.PerpendicularGroundState:
                return new PerpendicularGroundState(playerInformation, checkStatesCallBack);
            default:
                return null;
        }
    }
}
