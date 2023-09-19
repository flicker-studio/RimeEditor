using UnityEngine;

public static class GlobalSetting
{
    public struct LayerMasks
    {
        public readonly static LayerMask GROUND = LayerMask.NameToLayer("Ground");
    }
    
    public struct PlayerMotionStateType
    {
        public static MOTIONSTATEENUM MainMotionType => MOTIONSTATEENUM.MainDefultState | MOTIONSTATEENUM.WalkAndRunState 
            | MOTIONSTATEENUM.SlideState;

        public static MOTIONSTATEENUM AdditiveMotionType => MOTIONSTATEENUM.AdditiveDefultState |
                                                                  MOTIONSTATEENUM.PerpendicularGroundState | MOTIONSTATEENUM.JumpState |
                                                                  MOTIONSTATEENUM.None;
    }
}
