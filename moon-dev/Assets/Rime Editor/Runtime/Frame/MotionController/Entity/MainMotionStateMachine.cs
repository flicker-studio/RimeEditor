using System;
using LevelEditor;

namespace Frame.StateMachine
{
    public class MainMotionStateMachine : MotionStateMachine
    {
        public MainMotionStateMachine(MotionCallBack motionCallBack) : base(motionCallBack)
        {
        }

        public override void ChangeMotionState(Type motionStateType, Information Information)
        {
            var motionState = CreateMotionState(motionStateType, Information);
            if (motionState == null) return;

            m_motionStates.Clear();
            m_motionStates.Add(motionState);
        }
    }
}