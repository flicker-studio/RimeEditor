using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefultState : MainMotionState
{
    public override void Motion(InputData inputData)
    {
        Debug.Log("DefultState");
        if (inputData.moveInput.x == 1)
        {
            ChangeMoveState(new TestOne());
        }else if (inputData.moveInput.x == -1)
        {
            ChangeMoveState(new TestTwo());
        }
    }
}
