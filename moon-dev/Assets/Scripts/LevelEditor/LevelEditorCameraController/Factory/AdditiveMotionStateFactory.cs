using Frame.StateMachine;

namespace LevelEditor
{
    public class AdditiveStateFactory : AdditiveMotionStateFactory
    {
        public override MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, BaseInformation information,
                MotionCallBack motionCallBack)
        {
            if (information as Information != null)
            {
                switch (motionStateEnum)
                {
                    case MOTIONSTATEENUM.CameraDefultState:
                        return new CameraDefultState(information, motionCallBack);
                    case MOTIONSTATEENUM.CameraMoveState:
                        return new CameraMoveState(information, motionCallBack);
                    case MOTIONSTATEENUM.CameraChangeZState:
                        return new CameraChangeZState(information, motionCallBack);
                    case MOTIONSTATEENUM.MouseSelecteState:
                        return new MouseSelecteState(information, motionCallBack);
                    case MOTIONSTATEENUM.PositionAxisDragState:
                        return new PositionAxisDragState(information, motionCallBack);
                    case MOTIONSTATEENUM.RotationAxisDragState:
                        return new RotationAxisDragState(information, motionCallBack);
                    case MOTIONSTATEENUM.ItemTransformPanelShowState:
                        return new ItemTransformPanelShowState(information, motionCallBack);
                    case MOTIONSTATEENUM.ActionPanelShowState:
                        return new ActionPanelShowState(information, motionCallBack);
                    case MOTIONSTATEENUM.ControlHandlePanelShowState:
                        return new ControlHandlePanelShowState(information, motionCallBack);
                    default:
                        return null;
                }
            }
    
            return null;
        }
    }
}