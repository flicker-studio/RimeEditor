using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public abstract class LevelEditorCameraAdditiveState : AdditiveMotionState
{
    protected LevelEditorCameraInformation m_cameraInformation;
    
    protected LevelEditorCameraAdditiveState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_cameraInformation = information as LevelEditorCameraInformation;
    }
}
