using System.Collections;
using System.Collections.Generic;using System.Security.Cryptography;
using UnityEngine;

public abstract class MotionState
{
    public abstract void Motion(InputData inputData);
    
    protected void ChangeMoveState(MotionState motionState)
    {
        EventCenterManager.Instance.EventTrigger<MotionState>(GameEvent.ChangeMoveState,motionState);
    }
}
