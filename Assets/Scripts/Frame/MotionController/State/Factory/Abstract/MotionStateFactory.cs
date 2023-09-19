using System;

public abstract class MotionStateFactory
{
    public abstract MotionState CreateMotion(Enum motionStateEnum, BaseInformation information,
        MotionCallBack motionCallBack);
}
