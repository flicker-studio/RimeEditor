using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveMotionStateFactory : PlayerMotionStateFactory
{
    public override MotionState CreateMotion(Enum motionStateEnum, BaseInformation information,
        MotionCallBack motionCallBack)
    {
        switch (motionStateEnum)
        {
            case MOTIONSTATEENUM.AdditiveDefultState:
                return new AdditiveDefultState(information,motionCallBack);
            case MOTIONSTATEENUM.JumpState:
                return new JumpState(information, motionCallBack);
            case MOTIONSTATEENUM.PerpendicularGroundState:
                return new PerpendicularGroundState(information, motionCallBack);
            default:
                return null;
        }
    }
}
