using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public abstract class LevelEditorAdditiveState : AdditiveMotionState
{
    protected LevelEditorInformation m_information;
    
    protected LevelEditorAdditiveState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        m_information = baseInformation as LevelEditorInformation;
    }
}
