using Frame.StateMachine;

namespace Slicer
{
    public class SlicerOpenState : SlicerAdditiveMotionState
    {
        public SlicerOpenState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
        }

        public override void Motion(BaseInformation information)
        {
            base.Motion(information);
            if (m_slicerInformation.GetMouseLeftButtonDown)
            {
                ChangeMotionState(typeof(SlicerCopyState));
                RemoveState();
            }
        }
    }
}
