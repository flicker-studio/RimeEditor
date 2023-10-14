using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.StateMachine
{
    public abstract class MotionState
    {
        protected BaseInformation m_baseInformation;

        protected MotionCallBack m_motionCallBack;
    
        protected IList<Type> CheckStates => m_motionCallBack.CheckStatesCallBack?.Invoke();
    
        protected IList<Type> CheckGlobalStates => m_motionCallBack.CheckGlobalStatesCallBack?.Invoke();
        public abstract void Motion(BaseInformation information);
    
        protected void ChangeMotionState(MOTIONSTATEENUM motionStateEnum)
        {
            m_motionCallBack.ChangeMotionStateCallBack.Invoke(motionStateEnum);
        }
    
        public MotionState(BaseInformation baseInformation,MotionCallBack motionCallBack)
        {
            m_baseInformation = baseInformation;
            m_motionCallBack = motionCallBack;
        }
    }

}