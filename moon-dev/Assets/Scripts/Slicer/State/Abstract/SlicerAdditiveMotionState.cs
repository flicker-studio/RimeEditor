public abstract class SlicerAdditiveMotionState : AdditiveMotionState
{
    protected SlicerInformation m_slicerInformation;

    protected ICommand m_sliceCommand;
    
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
        if (m_sliceCommand != null && GetFirstExecute)
        {
            m_sliceCommand.Execute();
        }
    }

    public SlicerAdditiveMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_slicerInformation = information as SlicerInformation;
    }
}