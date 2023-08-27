using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private MotionController m_motionController;

    private void ControllerInit()
    {
        m_motionController = new MotionController();
    }
    
    private void Start()
    {
        ControllerInit();
        m_motionController.ChangeMotionState(new DefultState());
    }

    private void Update()
    {
        m_motionController.Motion();
    }
}
