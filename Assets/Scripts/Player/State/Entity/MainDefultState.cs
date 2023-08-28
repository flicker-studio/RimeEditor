using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDefultState : MainMotionState
{
    public override void Motion(PlayerInformation playerInformation)
    {
        if (m_inputController.GetInputData.MoveInput.x != 0)
        {
            ChangeMoveState(new WalkState(playerInformation));
        }

        m_componentController.Rigidbody.velocity = m_componentController.Rigidbody.velocity.NewX(0);
        // Debug.Log("DefultState");
    }

    public MainDefultState(PlayerInformation information) : base(information)
    {
    }
}
