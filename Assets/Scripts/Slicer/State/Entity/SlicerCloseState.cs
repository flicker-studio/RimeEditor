
public class SlicerCloseState : SlicerAdditiveMotionState
{
    public SlicerCloseState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        if (m_slicerInformation.GetNum1Down)
        {
            ChangeMotionState(MOTIONSTATEENUM.SlicerOpenState);
            RemoveState();
        }
    }
}
