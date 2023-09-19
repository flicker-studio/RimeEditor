using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMotionStateFactory : PlayerMotionStateFactory
{
    public override MotionState CreateMotion(Enum motionStateEnum, BaseInformation information,
        MotionCallBack motionCallBack)
    {
        switch (motionStateEnum)
        {
            case MOTIONSTATEENUM.MainDefultState:
                return new MainDefultState(information, motionCallBack);
            case MOTIONSTATEENUM.WalkAndRunState:
                return new WalkAndRunState(information, motionCallBack);
            case MOTIONSTATEENUM.SlideState:
                return new SlideState(information, motionCallBack);
            default:
                return null;
        }
    }
}
