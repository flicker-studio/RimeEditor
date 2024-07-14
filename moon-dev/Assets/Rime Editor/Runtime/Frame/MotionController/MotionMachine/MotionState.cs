using System;
using System.Collections.Generic;
using LevelEditor;

namespace Frame.StateMachine
{
    /// <summary>
    ///     State machine base class
    /// </summary>
    public abstract class MotionState
    {
        protected MotionCallBack m_motionCallBack;
        protected Information    MInformation;

        public MotionState(Information information, MotionCallBack motionCallBack)
        {
            MInformation     = information;
            m_motionCallBack = motionCallBack;
        }

        protected IList<Type> CheckStates => m_motionCallBack.CheckStatesCallBack?.Invoke();

        protected IList<Type> CheckGlobalStates => m_motionCallBack.CheckGlobalStatesCallBack?.Invoke();

        public abstract void Motion(Information information);

        protected void ChangeMotionState(Type motionStateType)
        {
            if (!motionStateType.IsSubclassOf(typeof(MotionState))) return;

            m_motionCallBack.ChangeMotionStateCallBack?.Invoke(motionStateType);
        }
    }
}