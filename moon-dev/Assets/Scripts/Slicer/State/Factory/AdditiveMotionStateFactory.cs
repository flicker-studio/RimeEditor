using Frame.StateMachine;

namespace Slicer
{
    public class AdditiveStateFactory : AdditiveMotionStateFactory
    {
        public override MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, BaseInformation information,
                MotionCallBack motionCallBack)
        {
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