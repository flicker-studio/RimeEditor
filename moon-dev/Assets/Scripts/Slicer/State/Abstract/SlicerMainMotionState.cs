public abstract class SlicerMainMotionState : MainMotionState
{
    protected SlicerInformation m_slicerInformation;
    
    protected SlicerMainMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_slicerInformation = information as SlicerInformation;
    }
}
