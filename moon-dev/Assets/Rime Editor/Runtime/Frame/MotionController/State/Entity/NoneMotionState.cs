using Frame.StateMachine;
using LevelEditor;

public class NoneMotionState : MotionState
{
    public NoneMotionState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
    }

    public override void Motion(Information information)
    {
    }
}