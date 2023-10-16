using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

namespace Frame.Static.Extensions
{
    public static class MotionStateMethod
    {
        private static MOTIONSTATEENUM MainMotionType => MOTIONSTATEENUM.PlyerMainDefultState | MOTIONSTATEENUM.PlayerWalkAndRunState 
            | MOTIONSTATEENUM.PlayerSlideState;

        private static MOTIONSTATEENUM AdditiveMotionType => MOTIONSTATEENUM.PlayerAdditiveDefultState |
                                                             MOTIONSTATEENUM.PlayerPerpendicularGroundState | MOTIONSTATEENUM.PlayerJumpState |
                                                             MOTIONSTATEENUM.SlicerCloseState | MOTIONSTATEENUM.PlayerJumpState |
                                                             MOTIONSTATEENUM.SlicerCopyState | MOTIONSTATEENUM.SlicerOpenState |
                                                             MOTIONSTATEENUM.LevelEditorCameraMoveState | MOTIONSTATEENUM.LevelEditorCameraDefultState |
                                                             MOTIONSTATEENUM.LevelEditorCameraChangeZState | MOTIONSTATEENUM.MouseSelecteState |
                                                             MOTIONSTATEENUM.PositionAxisDragState | MOTIONSTATEENUM.RotationAxisDragState |
                                                             MOTIONSTATEENUM.ItemTransformPanelShowState | MOTIONSTATEENUM.ActionPanelShowState |
                                                             MOTIONSTATEENUM.ControlHandlePanelShowState |
                                                             MOTIONSTATEENUM.None;
    
        public static bool CheckMotionIsMain(this MOTIONSTATEENUM motionStateEnum)
        {
            if (MainMotionType.HasFlag(motionStateEnum)) return true;
            else return false;
        }
    
    }
}
