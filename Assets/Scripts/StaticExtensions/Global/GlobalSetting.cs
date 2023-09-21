using UnityEngine;

public static class GlobalSetting
{
    public struct LayerMasks
    {
        public readonly static LayerMask GROUND = LayerMask.NameToLayer("Ground");
    }
    
    public struct PlayerMotionStateType
    {
        public readonly static MOTIONSTATEENUM MainMotionType = MOTIONSTATEENUM.PlyerMainDefultState | MOTIONSTATEENUM.PlayerWalkAndRunState 
            | MOTIONSTATEENUM.PlayerSlideState;

        public readonly static MOTIONSTATEENUM AdditiveMotionType = MOTIONSTATEENUM.PlayerAdditiveDefultState |
                                                                  MOTIONSTATEENUM.PlayerPerpendicularGroundState | MOTIONSTATEENUM.PlayerJumpState |
                                                                  MOTIONSTATEENUM.None;
    }
    
    public struct ObjNameTag
    {
        public readonly static string rigidbodyTag = "<rigidbody>";
    }
}
