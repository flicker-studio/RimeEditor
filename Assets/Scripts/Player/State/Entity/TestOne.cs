using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOne : MainMotionState
{
    public override void Motion(InputData inputData)
    {
        Debug.Log("TestOne");
        if (inputData.moveInput.x == 0)
        {
            ChangeMoveState(new DefultState());
        }else if (inputData.moveInput.x == -1)
        {
            ChangeMoveState(new TestTwo());
        }
    }
}
