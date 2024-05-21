using System;
using Moon.Kernel.Struct;
using Moon.Runtime.DesignPattern;
using UnityEngine;

namespace Moon.Runtime
{
    public class InputManager : MonoSingleton<InputManager>, IAutoCreateSingleton
    {
        public static InputManager Instance => Singleton;

        public bool CanInput = true;

        protected override void OnInit()
        {
            base.OnInit();

            #region MotionActions

            GetPlayerActions.Move.performed += context =>
            {
                if (CanInput) _moveInput.SetInput = context.ReadValue<Vector2>();
            };

            GetPlayerActions.Move.canceled += context =>
            {
                if (CanInput) _moveInput.SetInput = Vector2.zero;
            };

            GetPlayerActions.Jump.performed += context =>
            {
                if (CanInput) _jumpInput.SetInput = true;
            };

            GetPlayerActions.Jump.canceled += context =>
            {
                if (CanInput) _jumpInput.SetInput = false;
            };

            GetPlayerActions.Run.performed += context =>
            {
                if (CanInput) _runInput.SetInput = true;
            };

            GetPlayerActions.Run.canceled += context =>
            {
                if (CanInput) _runInput.SetInput = false;
            };

            GetPlayerActions.Slice.performed += context =>
            {
                if (CanInput) _sliceInput.SetInput = true;
            };

            GetPlayerActions.Slice.canceled += context =>
            {
                if (CanInput) _sliceInput.SetInput = false;
            };

            #endregion

            #region LevelEditor

            GetLevelEditorActions.MouseLeftButton.performed += context =>
            {
                if (CanInput) _mouseLeftButton.SetInput = true;
            };

            GetLevelEditorActions.MouseLeftButton.canceled += context =>
            {
                if (CanInput) _mouseLeftButton.SetInput = false;
            };

            GetLevelEditorActions.MouseRightButton.performed += context =>
            {
                if (CanInput) _mouseRightButton.SetInput = true;
            };

            GetLevelEditorActions.MouseRightButton.canceled += context =>
            {
                if (CanInput) _mouseRightButton.SetInput = false;
            };

            GetLevelEditorActions.MouseMiddleButton.performed += context =>
            {
                if (CanInput) _mouseMiddleButton.SetInput = true;
            };

            GetLevelEditorActions.MouseMiddleButton.canceled += context =>
            {
                if (CanInput) _mouseMiddleButton.SetInput = false;
            };

            GetLevelEditorActions.MouseScroll.performed += context =>
            {
                if (CanInput) _mouseScroll.SetInput = context.ReadValue<float>();
            };

            GetLevelEditorActions.MouseScroll.canceled += context =>
            {
                if (CanInput) _mouseScroll.SetInput = 0;
            };

            GetLevelEditorActions.ShiftButton.performed += context =>
            {
                if (CanInput) _shiftButton.SetInput = true;
            };

            GetLevelEditorActions.ShiftButton.canceled += context =>
            {
                if (CanInput) _shiftButton.SetInput = false;
            };

            GetLevelEditorActions.CtrlButton.performed += context =>
            {
                if (CanInput) _ctrlButton.SetInput = true;
            };

            GetLevelEditorActions.CtrlButton.canceled += context =>
            {
                if (CanInput) _ctrlButton.SetInput = false;
            };

            GetLevelEditorActions.ZButton.performed += context =>
            {
                if (CanInput) _zButton.SetInput = true;
            };

            GetLevelEditorActions.ZButton.canceled += context =>
            {
                if (CanInput) _zButton.SetInput = false;
            };

            GetLevelEditorActions.CButton.performed += context =>
            {
                if (CanInput) _cButton.SetInput = true;
            };

            GetLevelEditorActions.CButton.canceled += context =>
            {
                if (CanInput) _cButton.SetInput = false;
            };

            GetLevelEditorActions.VButton.performed += context =>
            {
                if (CanInput) _vButton.SetInput = true;
            };

            GetLevelEditorActions.VButton.canceled += context =>
            {
                if (CanInput) _vButton.SetInput = false;
            };

            GetLevelEditorActions.GButton.performed += context =>
            {
                if (CanInput) _gButton.SetInput = true;
            };

            GetLevelEditorActions.GButton.canceled += context =>
            {
                if (CanInput) _gButton.SetInput = false;
            };

            GetLevelEditorActions.PButton.performed += context =>
            {
                if (CanInput) _pButton.SetInput = true;
            };

            GetLevelEditorActions.PButton.canceled += context =>
            {
                if (CanInput) _pButton.SetInput = false;
            };

            GetLevelEditorActions.RButton.performed += context =>
            {
                if (CanInput) _rButton.SetInput = true;
            };

            GetLevelEditorActions.RButton.canceled += context =>
            {
                if (CanInput) _rButton.SetInput = false;
            };

            GetLevelEditorActions.SButton.performed += context =>
            {
                if (CanInput) _sButton.SetInput = true;
            };

            GetLevelEditorActions.SButton.canceled += context =>
            {
                if (CanInput) _sButton.SetInput = false;
            };

            GetLevelEditorActions.DeleteButton.performed += context =>
            {
                if (CanInput) _deleteButton.SetInput = true;
            };

            GetLevelEditorActions.DeleteButton.canceled += context =>
            {
                if (CanInput) _deleteButton.SetInput = false;
            };

            GetLevelEditorActions.EscapeButton.performed += context =>
            {
                if (CanInput) _escapeButton.SetInput = true;
            };

            GetLevelEditorActions.EscapeButton.canceled += context =>
            {
                if (CanInput) _escapeButton.SetInput = false;
            };

            #endregion

            #region Debugger

            GetDebuggerActions.Num1.performed += context =>
            {
                if (CanInput) _num1.SetInput = true;
            };

            GetDebuggerActions.Num1.canceled += context =>
            {
                if (CanInput) _num1.SetInput = false;
            };

            GetDebuggerActions.Num2.performed += context =>
            {
                if (CanInput) _num2.SetInput = true;
            };

            GetDebuggerActions.Num2.canceled += context =>
            {
                if (CanInput) _num2.SetInput = false;
            };

            GetDebuggerActions.Num3.performed += context =>
            {
                if (CanInput) _num3.SetInput = true;
            };

            GetDebuggerActions.Num3.canceled += context =>
            {
                if (CanInput) _num3.SetInput = false;
            };

            GetDebuggerActions.Num4.performed += context =>
            {
                if (CanInput) _num4.SetInput = true;
            };

            GetDebuggerActions.Num4.canceled += context =>
            {
                if (CanInput) _num4.SetInput = false;
            };

            GetDebuggerActions.Num5.performed += context =>
            {
                if (CanInput) _num5.SetInput = true;
            };

            GetDebuggerActions.Num5.canceled += context =>
            {
                if (CanInput) _num5.SetInput = false;
            };

            #endregion
        }

        #region PlayerAction
        
        private PlayerAction _playerActions;

        private PlayerAction GetPlayerInputActions
        {
            get
            {
                if (_playerActions == null)
                {
                    _playerActions = new PlayerAction();
                    _playerActions.Enable();
                }
                
                return _playerActions;
            }
        }

        private PlayerAction.PlayerActions GetPlayerActions
        {
            get { return GetPlayerInputActions.Player; }
        }

        private PlayerAction.LevelEditorActions GetLevelEditorActions
        {
            get { return GetPlayerInputActions.LevelEditor; }
        }

        #endregion

        #region Debugger

        private PlayerAction.DebuggerActions GetDebuggerActions
        {
            get { return GetPlayerInputActions.Debugger; }
        }

        #endregion

        #region PlayerMotionInput
        
        private Input<Vector2> _moveInput;
        
        private Input<bool> _jumpInput;
        
        private Input<bool> _runInput;
        
        private Input<bool> _sliceInput;
        
        public Vector2 GetMoveInput => _moveInput.GetInput;
        
        public bool GetMoveInputDown => _moveInput.GetInputDown;
        
        public bool GetMoveInputUp => _moveInput.GetInputUp;
        
        public bool GetJumpInput => _jumpInput.GetInput;
        
        public bool GetJumpInputDown => _jumpInput.GetInputDown;
        
        public bool GetJumpInputUp => _jumpInput.GetInputUp;
        
        public bool GetRunInput => _runInput.GetInput;
        
        public bool GetRunInputDown => _runInput.GetInputDown;
        
        public bool GetRunInputUp => _runInput.GetInputUp;
        
        public bool GetSliceInput => _sliceInput.GetInput;
        
        public bool GetSliceInputDown => _sliceInput.GetInputDown;
        
        public bool GetSliceInputUp => _sliceInput.GetInputUp;

        #endregion

        #region LevelEditor
        
        private Input<bool> _mouseLeftButton;
        
        private Input<bool> _mouseRightButton;
        
        private Input<bool> _mouseMiddleButton;
        
        private Input<float> _mouseScroll;
        
        private Input<bool> _shiftButton;
        
        private Input<bool> _ctrlButton;
        
        private Input<bool> _zButton;
        
        private Input<bool> _cButton;
        
        private Input<bool> _vButton;
        
        private Input<bool> _gButton;
        
        private Input<bool> _pButton;
        
        private Input<bool> _rButton;
        
        private Input<bool> _sButton;
        
        private Input<bool> _deleteButton;
        
        private Input<bool> _escapeButton;
        
        public bool GetMouseLeftButton => _mouseLeftButton.GetInput;
        
        public bool GetMouseLeftButtonDown => _mouseLeftButton.GetInputDown;
        
        public bool GetMouseLeftButtonUp => _mouseLeftButton.GetInputUp;
        
        public bool GetMouseRightButton => _mouseRightButton.GetInput;
        
        public bool GetMouseRightButtonDown => _mouseRightButton.GetInputDown;
        
        public bool GetMouseRightButtonUp => _mouseRightButton.GetInputUp;
        
        public bool GetMouseMiddleButton => _mouseMiddleButton.GetInput;
        
        public bool GetMouseMiddleButtonDown => _mouseMiddleButton.GetInputDown;
        
        public bool GetMouseMiddleButtonUp => _mouseMiddleButton.GetInputUp;
        
        public float GetMouseScroll => _mouseScroll.GetInput;
        
        public bool GetMouseScrollDown => _mouseScroll.GetInputDown;
        
        public bool GetMouseScrollUp => _mouseScroll.GetInputUp;
        
        public bool GetShiftButton => _shiftButton.GetInput;
        
        public bool GetShiftButtonDown => _shiftButton.GetInputDown;
        
        public bool GetShiftButtonUp => _shiftButton.GetInputUp;
        
        public bool GetCtrlButton => _ctrlButton.GetInput;
        
        public bool GetCtrlButtonDown => _ctrlButton.GetInputDown;
        
        public bool GetCtrlButtonUp => _ctrlButton.GetInputUp;
        
        public bool GetZButton => _zButton.GetInput;
        
        public bool GetZButtonDown => _zButton.GetInputDown;
        public bool GetZButtonUp   => _zButton.GetInputUp;
        
        public bool GetCButton     => _zButton.GetInput;
        public bool GetCButtonDown => _cButton.GetInputDown;
        public bool GetCButtonUp   => _cButton.GetInputUp;
        
        public bool GetVButton     => _zButton.GetInput;
        public bool GetVButtonDown => _vButton.GetInputDown;
        public bool GetVButtonUp   => _vButton.GetInputUp;
        
        public bool GetGButtonDown => _gButton.GetInputDown;
        public bool GetGButtonUp   => _gButton.GetInputUp;
        
        public bool GetPButtonDown => _pButton.GetInputDown;
        public bool GetPButtonUp   => _pButton.GetInputUp;
        
        public bool GetRButtonDown => _rButton.GetInputDown;
        public bool GetRButtonUp   => _rButton.GetInputUp;
        
        public bool GetSButtonDown => _sButton.GetInputDown;
        public bool GetSButtonUp   => _sButton.GetInputUp;
        
        public bool GetDelexteButton => _deleteButton.GetInput;
        
        public bool GetDeleteButtonDown => _deleteButton.GetInputDown;
        
        public bool GetDeleteButtonUp => _deleteButton.GetInputUp;
        
        public bool GetEscapeButton => _escapeButton.GetInput;
        
        public bool GetEscapeButtonDown => _escapeButton.GetInputDown;
        
        public bool GetEscapeButtonUp => _escapeButton.GetInputUp;

        #endregion

        #region Debugger
        
        private Input<bool> _num1;
        
        private Input<bool> _num2;
        
        private Input<bool> _num3;
        
        private Input<bool> _num4;
        
        private Input<bool> _num5;
        
        public bool GetDebuggerNum1Down => _num1.GetInputDown;
        
        public bool GetDebuggerNum1Up => _num1.GetInputUp;
        
        public bool GetDebuggerNum2Down => _num2.GetInputDown;
        
        public bool GetDebuggerNum2Up => _num2.GetInputUp;
        
        public bool GetDebuggerNum3Down => _num3.GetInputDown;
        
        public bool GetDebuggerNum3Up => _num3.GetInputUp;
        
        public bool GetDebuggerNum4Down => _num4.GetInputDown;
        
        public bool GetDebuggerNum4Up => _num4.GetInputUp;
        
        public bool GetDebuggerNum5Down => _num5.GetInputDown;
        
        public bool GetDebuggerNum5Up => _num5.GetInputUp;

        #endregion

        #region Actions

        public Action AddEscapeButtonDownAction
        {
            set => _escapeButton.DownAction += value;
        }

        public Action RemoveEscapeButtonDownAction
        {
            set => _escapeButton.DownAction -= value;
        }

        #endregion
    }
}