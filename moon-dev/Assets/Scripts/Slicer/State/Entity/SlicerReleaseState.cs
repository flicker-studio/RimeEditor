using Frame.StateMachine;

namespace Slicer
{
    public class SlicerReleaseState : SlicerAdditiveMotionState
    {
        public SlicerReleaseState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            MSliceDoCommand = new ReleaseSlicer(m_slicerInformation);
        }

        public override void Motion(BaseInformation information)
        {
            base.Motion(information);
            if (m_slicerInformation.GetNum1Down)
            {
                ChangeMotionState(typeof(SlicerCloseState));
                RemoveState();
            }
        }
    }
}
