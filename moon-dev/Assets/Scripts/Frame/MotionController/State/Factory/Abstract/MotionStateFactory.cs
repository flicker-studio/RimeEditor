using System;

namespace Frame.StateMachine
{
    public enum MOTIONSTATEENUM
    {
        #region Player

        None = 1<<0,
        PlyerMainDefultState = 1<<1,
        PlayerWalkAndRunState = 1<<2,
        PlayerSlideState = 1<<3,
        PlayerAdditiveDefultState = 1<<4,
        PlayerJumpState = 1<<5,
        PlayerPerpendicularGroundState = 1<<6,
        
        #endregion

        #region Slicer

        SlicerCloseState = 1<<7,
        SlicerOpenState = 1<<8,
        SlicerCopyState = 1<<9,
        SlicerReleaseState = 1<<10,

        #endregion

        #region LevelEditorCamera

        CameraDefultState = 1<<11,
        CameraMoveState = 1<<12,
        CameraChangeZState = 1 << 13,
        MouseSelecteState = 1 << 14,
        PositionAxisDragState = 1 << 15,
        RotationAxisDragState = 1 << 16,
        ItemTransformPanelShowState = 1 << 17,
        ActionPanelShowState = 1 << 18,
        ControlHandlePanelShowState = 1 << 19
        #endregion
    }

    public abstract class MotionStateFactory
    {
        public abstract MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, BaseInformation information,
            MotionCallBack motionCallBack);
    }
}
