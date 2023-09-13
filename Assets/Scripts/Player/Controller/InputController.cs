using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
    public Vector2 MoveInput;
    public bool JumpInput;
    public bool RunInput;
    public bool SliceInput;
    
    public bool MoveInputDown => InputManager.Instance.GetMoveInputDown;
    public bool MoveInputUp => InputManager.Instance.GetMoveInputUp;
    public bool JumpInputDown => InputManager.Instance.GetJumpInputDown;
    public bool JumpInputUp => InputManager.Instance.GetJumpInputUp;
    public bool RunInputDown => InputManager.Instance.GetRunInputDown;
    public bool RunInputUp => InputManager.Instance.GetRunInputUp;
    public bool SliceInputDown => InputManager.Instance.GetSliceInputDown;
    
    public bool SliceInputUp => InputManager.Instance.GetSliceInputUp;
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
            m_inputData.SliceInput = InputManager.Instance.GetSliceInput;
            return m_inputData;
        }
    }
}
