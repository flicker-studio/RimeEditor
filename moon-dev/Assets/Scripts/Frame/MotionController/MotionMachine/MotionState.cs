using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.StateMachine
{
    public abstract class MotionState
    {
        protected BaseInformation m_information;

        protected MotionCallBack m_motionCallBack;
    
        protected List<Type> CheckStates => m_motionCallBack.CheckStatesCallBack?.Invoke();
    
        protected List<Type> CheckGlobalStates => m_motionCallBack.CheckGlobalStatesCallBack?.Invoke();
        public abstract void Motion(BaseInformation information);
    
        protected void ChangeMotionState(MOTIONSTATEENUM motionStateEnum)
        {
            m_motionCallBack.ChangeMotionStateCallBack.Invoke(motionStateEnum);
        }
    
        public MotionState(BaseInformation information,MotionCallBack motionCallBack)
        {
            m_information = information;
            m_motionCallBack = motionCallBack;
        }
    }

}