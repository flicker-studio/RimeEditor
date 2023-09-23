namespace Frame.StateMachine
{
    public abstract class MainMotionState : MotionState
    {
        protected MainMotionState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
        {
        }
    }
}
