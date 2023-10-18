using System;
using System.Collections.Generic;

namespace Frame.StateMachine
{
    public abstract class MotionState
    {
        protected BaseInformation m_baseInformation;

        protected MotionCallBack m_motionCallBack;
    
        protected IList<Type> CheckStates => m_motionCallBack.CheckStatesCallBack?.Invoke();
    
        protected IList<Type> CheckGlobalStates => m_motionCallBack.CheckGlobalStatesCallBack?.Invoke();
        public abstract void Motion(BaseInformation information);
    
        protected void ChangeMotionState(Type motionStateType)
        {
            if (!motionStateType.IsSubclassOf(typeof(MotionState))) return;
            m_motionCallBack.ChangeMotionStateCallBack?.Invoke(motionStateType);
        }
    
        public MotionState(BaseInformation baseInformation,MotionCallBack motionCallBack)
        {
            m_baseInformation = baseInformation;
            m_motionCallBack = motionCallBack;
        }
    }

}