using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdditiveMotionState : MotionState
{
    override public void Motion()
    {
        EndTimer();
    }

    protected abstract void EndTimer();
}
