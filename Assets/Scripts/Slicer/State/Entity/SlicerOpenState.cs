public class SlicerOpenState : SlicerAdditiveMotionState
{
    public SlicerOpenState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        base.Motion(information);
        if (m_slicerInformation.GetNum1Down)
        {
            ChangeMotionState(MOTIONSTATEENUM.SlicerCopyState);
            RemoveState();
        }
    }
}
