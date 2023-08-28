using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
    public Vector2 MoveInput;
    public bool JumpInput;
    public bool RunInput;
}

public class InputController
{
    private InputData m_inputData;

    public InputData GetInputData
    {
        get
        {
            m_inputData.MoveInput = InputManager.Instance.GetMoveInput;
            m_inputData.JumpInput = InputManager.Instance.GetJumpInput;
            m_inputData.RunInput = InputManager.Instance.GetRunInput;
            return m_inputData;
        }
    }
}
