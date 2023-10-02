using Frame.StateMachine;
using Frame.Tool;
using Slicer.Information;

namespace Slicer.State
{
    public abstract class SlicerAdditiveMotionState : AdditiveMotionState
    {
        protected SlicerInformation m_slicerInformation;

        protected SliceCommand MSliceDoCommand;
    
        private bool m_firstExecute = true;

        protected bool GetFirstExecute
        {
            get
            {
                if (m_firstExecute)
                {
                    m_firstExecute = false;
                    return true;
                }

                return false;
            }
        }

        public override void Motion(BaseInformation information)
        {
            if (MSliceDoCommand != null && GetFirstExecute)
            {
                MSliceDoCommand.Execute();
            }
        }

        public SlicerAdditiveMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_slicerInformation = information as SlicerInformation;
        }
    }
}
