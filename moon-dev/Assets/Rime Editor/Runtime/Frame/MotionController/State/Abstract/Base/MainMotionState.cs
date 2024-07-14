using LevelEditor;

namespace Frame.StateMachine
{
    public abstract class MainMotionState : MotionState
    {
        protected MainMotionState(Information information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
        }
    }
}