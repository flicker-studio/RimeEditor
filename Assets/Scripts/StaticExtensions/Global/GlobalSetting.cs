using UnityEngine;

public static class GlobalSetting
{
    public struct LayerMasks
    {
        public readonly static LayerMask GROUND = LayerMask.NameToLayer("Ground");
    }
    
    public struct PlayerMotionStateType
    {
        public static MOTIONSTATEENUM MainMotionType => MOTIONSTATEENUM.PlyerMainDefultState | MOTIONSTATEENUM.PlayerWalkAndRunState 
            | MOTIONSTATEENUM.PlayerSlideState;

        public static MOTIONSTATEENUM AdditiveMotionType => MOTIONSTATEENUM.PlayerAdditiveDefultState |
                                                                  MOTIONSTATEENUM.PlayerPerpendicularGroundState | MOTIONSTATEENUM.PlayerJumpState |
                                                                  MOTIONSTATEENUM.None;
    }
}
