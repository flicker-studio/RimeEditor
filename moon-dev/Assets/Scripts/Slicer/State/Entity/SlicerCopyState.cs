using Frame.StateMachine;
using Slicer.Command;

namespace Slicer.State
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
            if (m_slicerInformation.GetNum1Down)
            {
                ChangeMotionState(MOTIONSTATEENUM.SlicerReleaseState);
                RemoveState();
            }
        }
    }
}
