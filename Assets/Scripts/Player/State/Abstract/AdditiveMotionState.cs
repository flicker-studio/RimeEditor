using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdditiveMotionState : MotionState
{
    protected AdditiveMotionState(PlayerInformation information) : base(information)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        EndTimmer();
    }

    protected abstract void EndTimmer();
}
