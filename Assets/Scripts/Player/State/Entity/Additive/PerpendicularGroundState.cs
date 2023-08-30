using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpendicularGroundState : AdditiveMotionState
{
    public PerpendicularGroundState(PlayerInformation information) : base(information)
    {
    }

    public override void Motion(PlayerInformation playerInformation)
    {
        throw new System.NotImplementedException();
    }
}
