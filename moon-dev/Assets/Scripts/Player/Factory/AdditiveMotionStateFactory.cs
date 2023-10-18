using Frame.StateMachine;

namespace Character
{
    public class AdditiveStateFactory : AdditiveMotionStateFactory
    {
        public override MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, BaseInformation information,
                MotionCallBack motionCallBack)
        {
            if (information as PlayerInformation != null)
            {
                switch (motionStateEnum)
                {
                    case MOTIONSTATEENUM.PlayerAdditiveDefultState:
                        return new PlayerAdditiveDefultState(information,motionCallBack);
                    case MOTIONSTATEENUM.PlayerJumpState:
                        return new PlayerJumpState(information, motionCallBack);
                    case MOTIONSTATEENUM.PlayerPerpendicularGroundState:
                        return new PlayerPerpendicularGroundState(information, motionCallBack);
                    default:
                        return null;
                }
            }
                return null;
        }
    }
}