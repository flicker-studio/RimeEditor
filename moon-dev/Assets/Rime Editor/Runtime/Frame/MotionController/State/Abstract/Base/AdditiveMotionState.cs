using LevelEditor;

namespace Frame.StateMachine
{
    public abstract class AdditiveMotionState : MotionState
    {
        protected float m_endTimmer = 0;

        protected AdditiveMotionState(Information Information, MotionCallBack motionCallBack) : base(Information, motionCallBack)
        {
        }

        public bool IsEnd { get; private set; }

        protected virtual void RemoveState()
        {
            IsEnd = true;
            ChangeMotionState(typeof(NoneMotionState));
        }
    }
}