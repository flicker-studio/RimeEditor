
using Frame.StateMachine;

namespace Slicer.State
{
    public class SlicerCloseState : SlicerAdditiveMotionState
    {
        public SlicerCloseState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
        }

        public override void Motion(BaseInformation information)
        {
            base.Motion(information);
            if (m_slicerInformation.GetNum1Down)
            {
                ChangeMotionState(MOTIONSTATEENUM.SlicerOpenState);
                RemoveState();
            }
        }
    }
}
