using System;
using System.Collections.Generic;

namespace Frame.StateMachine
{
    public abstract class MotionStateMachine
    {
        protected List<MotionState> m_motionStates;

        protected MotionCallBack m_motionCallBack;

        public void Motion(BaseInformation baseInformation)
        {
            List<MotionState> tempList = new List<MotionState>();
            tempList.AddRange(m_motionStates);
            foreach (var motionState in tempList)
            {
                motionState.Motion(baseInformation);
            }
        }

        public abstract void ChangeMotionState(Type motionStateType,BaseInformation baseInformation);
    
        public List<Type> CheckStates()
        {
            List<Type> tempList = new List<Type>();
            foreach (var motionState in m_motionStates)
            {
                tempList.Add(motionState.GetType());
            }
            return tempList;
        }

        protected MotionState CreateMotionState(Type motionStateType, BaseInformation information)
        {
            return Activator.CreateInstance(motionStateType, information, m_motionCallBack) as MotionState;
        }

        public MotionStateMachine(MotionCallBack motionCallBack)
        {
            m_motionStates = new List<MotionState>();
            m_motionCallBack = motionCallBack;
            m_motionCallBack.CheckStatesCallBack = CheckStates;
        }
        
    }

}
