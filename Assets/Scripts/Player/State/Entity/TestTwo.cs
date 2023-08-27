using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTwo : MainMotionState
{
    public override void Motion(InputData inputData)
    {
        if (inputData.moveInput.x == 0)
        {
            ChangeMoveState(new DefultState());
        }else if (inputData.moveInput.x == 1)
        {
            ChangeMoveState(new TestOne());
        }
        Debug.Log("TestTwo");
    }
}
