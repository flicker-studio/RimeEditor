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
        SlicerMoveFollowState = 1<<11,
        SlicerRotationFollowState = 1<<12

        #endregion
    }

    public abstract class MotionStateFactory
    {
        public abstract MotionState CreateMotion(MOTIONSTATEENUM motionStateEnum, BaseInformation information,
            MotionCallBack motionCallBack);
    }
}
