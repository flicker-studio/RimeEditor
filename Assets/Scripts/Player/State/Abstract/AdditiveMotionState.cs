using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdditiveMotionState : MotionState
{
    protected float m_endTimmer = 0;

    public bool IsEnd = false;
    protected AdditiveMotionState(PlayerInformation information) : base(information)
    {
    }
    
    protected void RemoveState()
    {
        IsEnd = true;
        ChangeMoveState(null);
    }
}
