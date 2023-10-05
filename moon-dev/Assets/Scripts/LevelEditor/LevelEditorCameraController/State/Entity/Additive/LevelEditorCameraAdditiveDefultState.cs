using Frame.StateMachine;
using UnityEngine;

public class LevelEditorCameraAdditiveDefultState : LevelEditorCameraAdditiveState
{
    public LevelEditorCameraAdditiveDefultState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        if (m_cameraInformation.GetMouseMiddleButtonDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorCameraMoveState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraMoveState);
            }
        }

        if (m_cameraInformation.GetMouseSrollDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorCameraChangeFovState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraChangeFovState);
            }
        }

        if (m_cameraInformation.GetMouseLeftButtonDown)
        {
            if (!CheckStates.Contains(typeof(MouseSelecteState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.MouseSelecteState);
            }
        }
    }
}
