using System.Collections;
using System.Collections.Generic;
using Frame.Tool;
using UnityEngine;

namespace Character.Information
{
    public struct MotionInputData
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

    public class MotionInputController
    {
        private MotionInputData m_MotionInputData;

        public MotionInputData GetMotionInputData
        {
            get
            {
                m_MotionInputData.MoveInput = InputManager.Instance.GetMoveInput;
                m_MotionInputData.JumpInput = InputManager.Instance.GetJumpInput;
                m_MotionInputData.RunInput = InputManager.Instance.GetRunInput;
                m_MotionInputData.SliceInput = InputManager.Instance.GetSliceInput;
                return m_MotionInputData;
            }
        }
    }
}
