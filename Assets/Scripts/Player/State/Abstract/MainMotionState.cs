using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainMotionState : MotionState
{
    protected MainMotionState(PlayerInformation information, CheckStatesCallBack checkStatesCallBack) : base(information, checkStatesCallBack)
    {
    }
}
