using Character.Information;
using Character.State;

namespace Frame.StateMachine
{
    public class MainMotionStateFactory : MotionStateFactory
    {
        public override MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, BaseInformation information,
            MotionCallBack motionCallBack)
        {
            if (information as PlayerInformation != null)
            {
                switch (motionStateEnum)
                {
                    case MOTIONSTATEENUM.PlyerMainDefultState:
                        return new PlayerMainDefultState(information, motionCallBack);
                    case MOTIONSTATEENUM.PlayerWalkAndRunState:
                        return new PlayerWalkAndRunState(information, motionCallBack);
                    case MOTIONSTATEENUM.PlayerSlideState:
                        return new PlayerSlideState(information, motionCallBack);
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
