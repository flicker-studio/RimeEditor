using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public abstract class LevelEditorCameraMainState : MainMotionState
{
    protected LevelEditorInformation MInformation;
    
    protected LevelEditorCameraMainState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        MInformation = information as LevelEditorInformation;
    }
}
