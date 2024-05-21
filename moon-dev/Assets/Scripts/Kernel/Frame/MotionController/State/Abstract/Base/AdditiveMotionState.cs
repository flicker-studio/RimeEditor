

namespace Frame.StateMachine
{
    public abstract class AdditiveMotionState : MotionState 
    {
        protected float m_endTimmer = 0;

        public bool IsEnd { get; private set; }

        
        protected virtual void RemoveState()
        {
            IsEnd = true;
            ChangeMotionState(typeof(NoneMotionState));
        }

        protected AdditiveMotionState(BaseInformation baseInformation,MotionCallBack motionCallBack):base(baseInformation, motionCallBack)
        {
        }
    }
}
