using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public abstract class LevelEditorCameraAdditiveState : AdditiveMotionState
{
    protected LevelEditorInformation m_information;
    
    protected LevelEditorCameraAdditiveState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        m_information = baseInformation as LevelEditorInformation;
    }
}
