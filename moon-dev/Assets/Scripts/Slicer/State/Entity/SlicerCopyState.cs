using Frame.StateMachine;

namespace Slicer
{
    public class SlicerCopyState : SlicerAdditiveMotionState
    {
        public SlicerCopyState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            MSliceDoCommand = new CopySlicer(m_slicerInformation);
        }

        public override void Motion(BaseInformation information)
        {
            base.Motion(information);
            if (m_slicerInformation.GetMouseLeftButtonDown)
            {
                ChangeMotionState(typeof(SlicerReleaseState));
                RemoveState();
            }
        }
    }
}
