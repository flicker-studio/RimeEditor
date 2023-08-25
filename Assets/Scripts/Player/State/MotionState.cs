using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotionState
{
    public abstract void Motion();
    
    protected void ChangeMoveState(MotionState motionState)
    {
        EventCenterManager.Instance.EventTrigger<MotionState>(GameEvent.ChangeMoveState,motionState);
    }
}
