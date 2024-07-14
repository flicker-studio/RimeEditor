using Frame.StateMachine;

namespace LevelEditor
{
    public abstract class AdditiveState : AdditiveMotionState
    {
        protected Information m_information;

        protected AdditiveState(Information Information, MotionCallBack motionCallBack) : base(Information, motionCallBack)
        {
            m_information = Information;
        }
    }
}