using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController
{
    private List<MotionStateMachine> m_motionStateMachines;

    public void Motion()
    {
        foreach (var motionStateMachine in m_motionStateMachines)
        {
            motionStateMachine.Motion();
        }
    }
}
