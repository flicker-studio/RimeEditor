using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
    public Vector2 moveInput;
    public bool jumpInput;
}

public class InputController
{
    private InputData m_inputData;

    public InputData GetInputData
    {
        get
        {
            m_inputData.moveInput = InputManager.Instance.GetMoveInput;
            m_inputData.jumpInput = InputManager.Instance.GetJumpInput;
            return m_inputData;
        }
    }
}
