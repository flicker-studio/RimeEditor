
namespace Frame.StateMachine
{
    public abstract class AdditiveMotionState : MotionState 
    {
        protected float m_endTimmer = 0;

        public bool IsEnd = false;

        protected virtual void RemoveState()
        {
            IsEnd = true;
            ChangeMotionState(MOTIONSTATEENUM.None);
        }

        protected AdditiveMotionState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
        {
        }
    }
}
