using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public abstract class LevelEditorCameraMainState : MainMotionState
{
    protected LevelEditorCameraInformation m_cameraInformation;
    
    protected LevelEditorCameraMainState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_cameraInformation = information as LevelEditorCameraInformation;
    }
}
