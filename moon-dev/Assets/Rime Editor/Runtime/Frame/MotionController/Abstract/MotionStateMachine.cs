using System;
using System.Collections.Generic;
using LevelEditor;

namespace Frame.StateMachine
{
    public abstract class MotionStateMachine
    {
        protected MotionCallBack    m_motionCallBack;
        protected List<MotionState> m_motionStates;

        public MotionStateMachine(MotionCallBack motionCallBack)
        {
            m_motionStates                       = new List<MotionState>();
            m_motionCallBack                     = motionCallBack;
            m_motionCallBack.CheckStatesCallBack = CheckStates;
        }

        public void Motion(Information Information)
        {
            var tempList = new List<MotionState>();
            tempList.AddRange(m_motionStates);
            foreach (var motionState in tempList) motionState.Motion(Information);
        }

        public abstract void ChangeMotionState(Type motionStateType, Information Information);

        public List<Type> CheckStates()
        {
            var tempList = new List<Type>();
            foreach (var motionState in m_motionStates) tempList.Add(motionState.GetType());

            return tempList;
        }

        protected MotionState CreateMotionState(Type motionStateType, Information information)
        {
            return Activator.CreateInstance(motionStateType, information, m_motionCallBack) as MotionState;
        }
    }
}