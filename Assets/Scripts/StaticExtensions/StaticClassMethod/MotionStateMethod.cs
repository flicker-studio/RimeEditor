using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MotionStateMethod
{
    private static MOTIONSTATEENUM MainMotionType => MOTIONSTATEENUM.MainDefultState | MOTIONSTATEENUM.WalkAndRunState 
        | MOTIONSTATEENUM.SlideState;

    private static MOTIONSTATEENUM AdditiveMotionType => MOTIONSTATEENUM.AdditiveDefultState |
                                                         MOTIONSTATEENUM.PerpendicularGroundState | MOTIONSTATEENUM.JumpState |
                                                         MOTIONSTATEENUM.None;
    
    public static bool CheckMotionIsMain(this MOTIONSTATEENUM motionStateEnum)
    {
        if (MainMotionType.HasFlag(motionStateEnum)) return true;
        else return false;
    }
    
}
