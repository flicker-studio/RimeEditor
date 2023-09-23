using Character.Information;
using Character.State;
using Slicer.Information;
using Slicer.State;

namespace Frame.StateMachine
{
    public class AdditiveMotionStateFactory : MotionStateFactory
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

            if (information as SlicerInformation != null)
            {
                switch (motionStateEnum)
                {
                    case MOTIONSTATEENUM.SlicerCloseState:
                        return new SlicerCloseState(information,motionCallBack);
                    case MOTIONSTATEENUM.SlicerOpenState:
                        return new SlicerOpenState(information,motionCallBack);
                    case MOTIONSTATEENUM.SlicerCopyState:
                        return new SlicerCopyState(information,motionCallBack);
                    case MOTIONSTATEENUM.SlicerReleaseState:
                        return new SlicerReleaseState(information,motionCallBack);
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
