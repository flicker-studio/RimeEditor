using System;

namespace Frame.StateMachine
{
    public class MainMotionStateMachine : MotionStateMachine
    {
        public override void ChangeMotionState(Type motionStateType, BaseInformation baseInformation)
        {
            MotionState motionState = CreateMotionState(motionStateType, baseInformation);
            if (motionState == null) return;

            m_motionStates.Clear();
            m_motionStates.Add(motionState);
        }


        public MainMotionStateMachine(MotionCallBack motionCallBack) : base(motionCallBack)
        {
        }
    }
}