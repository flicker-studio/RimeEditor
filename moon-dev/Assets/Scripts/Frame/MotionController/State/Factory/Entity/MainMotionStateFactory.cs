using Character.Information;
using Character.State;
using Slicer.Information;
using Slicer.State;

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
            
            if (information as SlicerInformation != null)
            {
                switch (motionStateEnum)
                {
                    case MOTIONSTATEENUM.SlicerMoveFollowState:
                        return new SlicerMoveFollowState(information, motionCallBack);
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
