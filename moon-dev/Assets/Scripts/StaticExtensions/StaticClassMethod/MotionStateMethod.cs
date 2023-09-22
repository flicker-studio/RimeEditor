using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MotionStateMethod
{
    private static MOTIONSTATEENUM MainMotionType => MOTIONSTATEENUM.PlyerMainDefultState | MOTIONSTATEENUM.PlayerWalkAndRunState 
        | MOTIONSTATEENUM.PlayerSlideState;

    private static MOTIONSTATEENUM AdditiveMotionType => MOTIONSTATEENUM.PlayerAdditiveDefultState |
                                                         MOTIONSTATEENUM.PlayerPerpendicularGroundState | MOTIONSTATEENUM.PlayerJumpState |
                                                         MOTIONSTATEENUM.None;
    
    public static bool CheckMotionIsMain(this MOTIONSTATEENUM motionStateEnum)
    {
        if (MainMotionType.HasFlag(motionStateEnum)) return true;
        else return false;
    }
    
}
