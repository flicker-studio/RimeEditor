using System;
using System.Collections.Generic;

namespace Frame.StateMachine
{
    public delegate List<Type> CheckStatesCallBack();

    public delegate List<Type> CheckGlobalStatesCallBack();

    public delegate void ChangeMotionStateCallBack(MOTIONSTATEENUM motionStateEnum);

    public class MotionCallBack
    {
        public CheckStatesCallBack CheckStatesCallBack;

        public CheckGlobalStatesCallBack CheckGlobalStatesCallBack;

        public ChangeMotionStateCallBack ChangeMotionStateCallBack;
    }

}