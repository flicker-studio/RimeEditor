using System;
using System.Collections.Generic;

namespace Frame.StateMachine
{
    public abstract class MotionStateMachine
    {
        protected List<MotionState> m_motionStates;

        protected MotionCallBack m_motionCallBack;

        protected MotionStateFactory m_motionStateFactory;

        public void Motion(BaseInformation baseInformation)
        {
            List<MotionState> tempList = new List<MotionState>();
            tempList.AddRange(m_motionStates);
            foreach (var motionState in tempList)
            {
                motionState.Motion(baseInformation);
            }
        }

        public abstract void ChangeMotionState(MOTIONSTATEENUM playerMoveState,BaseInformation baseInformation);
    
        public List<Type> CheckStates()
        {
            List<Type> tempList = new List<Type>();
            foreach (var motionState in m_motionStates)
            {
                tempList.Add(motionState.GetType());
            }
            return tempList;
        }

        protected MotionState CreateMotionState(MOTIONSTATEENUM motionStateEnum, BaseInformation information)
        {
            return m_motionStateFactory.CreateMotion(motionStateEnum,information, m_motionCallBack);
        }

        public MotionStateMachine(MotionCallBack motionCallBack)
        {
            m_motionStates = new List<MotionState>();
            m_motionCallBack = motionCallBack;
            m_motionCallBack.CheckStatesCallBack = CheckStates;
        }
    }

}
