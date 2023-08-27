using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefultState : MainMotionState
{
    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_inputController.GetInputData.MoveInput.x != 0)
        {
            ChangeMoveState(new WalkState(playerInformation));
        }
        Debug.Log("DefultState");
    }

    public DefultState(PlayerInformation information) : base(information)
    {
    }
}
