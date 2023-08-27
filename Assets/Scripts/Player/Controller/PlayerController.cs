using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MotionController m_motionController;

    private InputController m_inputController;

    private void ControllerInit()
    {
        m_motionController = new MotionController();
        m_inputController = new InputController();
    }
    
    private void Start()
    {
        ControllerInit();
        m_motionController.ChangeMotionState(new DefultState());
    }

    private void Update()
    {
        m_motionController.Motion(m_inputController.GetInputData);
    }
}
