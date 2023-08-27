using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveDefultState : AdditiveMotionState
{
    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_inputController.GetInputData.JumpInput)
        {
            ChangeMoveState(new JumpState(playerInformation));
        }
    }
    
    public AdditiveDefultState(PlayerInformation information) : base(information)
    {
    }
}
