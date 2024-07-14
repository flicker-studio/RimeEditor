using System;
using System.Collections.Generic;
using LevelEditor;

namespace Frame.StateMachine
{
    public class AdditiveMotionStateMachine : MotionStateMachine
    {
        public AdditiveMotionStateMachine(MotionCallBack motionCallBack) : base(motionCallBack)
        {
        }

        public override void ChangeMotionState(Type motionStateType, Information information)
        {
            if (motionStateType == typeof(NoneMotionState))
            {
                var tempList = new List<MotionState>();
                tempList.AddRange(m_motionStates);

                foreach (var state in tempList)
                    if ((state as AdditiveMotionState).IsEnd)
                        m_motionStates.Remove(state);

                return;
            }

            var motionState = CreateMotionState(motionStateType, information);
            if (motionState == null) return;
            if (m_motionStates.Contains(motionState)) return;

            m_motionStates.Add(motionState);
        }
    }
}