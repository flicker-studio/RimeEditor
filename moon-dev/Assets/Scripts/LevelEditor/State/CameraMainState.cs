using Frame.StateMachine;

namespace LevelEditor
{
    public abstract class CameraMainState : MainMotionState
    {
        protected Information m_information;

        protected CameraMainState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_information = information as Information;
        }
    }
}