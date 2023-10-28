using Frame.StateMachine;


namespace Character
{
    public abstract class PlayerAdditiveMotionState : AdditiveMotionState
    {
        protected PlayerInformation m_playerInformation;
        public PlayerAdditiveMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_playerInformation = information as PlayerInformation;
        }
    }
}
