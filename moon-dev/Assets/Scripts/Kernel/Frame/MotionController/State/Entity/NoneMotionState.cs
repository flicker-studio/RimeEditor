using Frame.StateMachine;

public class NoneMotionState : MotionState
{
    public NoneMotionState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
    }
}