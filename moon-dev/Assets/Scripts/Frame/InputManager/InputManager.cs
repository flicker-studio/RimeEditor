using System;
using Struct;
using UnityEngine;

namespace Frame.Tool
{
    public class InputManager : UnityToolkit.MonoSingleton<InputManager>,UnityToolkit.IAutoCreateSingleton
    {
        public static InputManager Instance => Singleton;

        public bool CanInput = true;
    protected override void OnInit()
    {
        base.OnInit();
        #region MotionActions

        GetPlayerActions.Move.performed += context =>
        {
            if(CanInput) m_moveInput.SetInput = context.ReadValue<Vector2>();
        };

        GetPlayerActions.Move.canceled += context =>
        {
            if(CanInput) m_moveInput.SetInput = Vector2.zero;
        };

        GetPlayerActions.Jump.performed += context =>
        {
            if(CanInput) m_jumpInput.SetInput = true;
        };

        GetPlayerActions.Jump.canceled += context =>
        {
            if(CanInput) m_jumpInput.SetInput = false;
        };

        GetPlayerActions.Run.performed += context =>
        {
            if(CanInput) m_runInput.SetInput = true;
        };

        GetPlayerActions.Run.canceled += context =>
        {
            if(CanInput) m_runInput.SetInput = false;
        };
        
        GetPlayerActions.Slice.performed += context =>
        {
            if(CanInput) m_sliceInput.SetInput = true;
        };
        
        GetPlayerActions.Slice.canceled += context =>
        {
            if(CanInput) m_sliceInput.SetInput = false;
        };
        
        #endregion

        #region LevelEditor

        GetLevelEditorActions.MouseLeftButton.performed += context =>
        {
            if(CanInput) m_mouseLeftButton.SetInput = true;
        };
        
        GetLevelEditorActions.MouseLeftButton.canceled += context =>
        {
            if(CanInput) m_mouseLeftButton.SetInput = false;
        };

        GetLevelEditorActions.MouseRightButton.performed += context =>
        {
            if(CanInput) m_mouseRightButton.SetInput = true;
        };
        
        GetLevelEditorActions.MouseRightButton.canceled += context =>
        {
            if(CanInput) m_mouseRightButton.SetInput = false;
        };

        GetLevelEditorActions.MouseMiddleButton.performed += context =>
        {
            if(CanInput) m_mouseMiddleButton.SetInput = true;
        };

        GetLevelEditorActions.MouseMiddleButton.canceled += context =>
        {
            if(CanInput) m_mouseMiddleButton.SetInput = false;
        };
        
        GetLevelEditorActions.MouseScroll.performed += context =>
        {
            if(CanInput) m_mouseScroll.SetInput = context.ReadValue<float>();
        };
        
        GetLevelEditorActions.MouseScroll.canceled += context =>
        {
            if(CanInput) m_mouseScroll.SetInput = 0;
        };

        GetLevelEditorActions.ShiftButton.performed += context =>
        {
            if(CanInput) m_shiftButton.SetInput = true;
        };
        
        GetLevelEditorActions.ShiftButton.canceled += context =>
        {
            if(CanInput) m_shiftButton.SetInput = false;
        };
        
        GetLevelEditorActions.CtrlButton.performed += context =>
        {
            if(CanInput) m_ctrlButton.SetInput = true;
        };
        
        GetLevelEditorActions.CtrlButton.canceled += context =>
        {
            if(CanInput) m_ctrlButton.SetInput = false;
        };

        GetLevelEditorActions.ZButton.performed += context =>
        {
            if(CanInput) m_zButton.SetInput = true;
        };
        
        GetLevelEditorActions.ZButton.canceled += context =>
        {
            if(CanInput) m_zButton.SetInput = false;
        };
        
        GetLevelEditorActions.CButton.performed += context =>
        {
            if(CanInput) m_cButton.SetInput = true;
        };
        
        GetLevelEditorActions.CButton.canceled += context =>
        {
            if(CanInput) m_cButton.SetInput = false;
        };
        
        GetLevelEditorActions.VButton.performed += context =>
        {
            if(CanInput) m_vButton.SetInput = true;
        };
        
        GetLevelEditorActions.VButton.canceled += context =>
        {
            if(CanInput) m_vButton.SetInput = false;
        };
        
        GetLevelEditorActions.GButton.performed += context =>
        {
            if(CanInput) m_gButton.SetInput = true;
        };
        
        GetLevelEditorActions.GButton.canceled += context =>
        {
            if(CanInput) m_gButton.SetInput = false;
        };
        
        GetLevelEditorActions.PButton.performed += context =>
        {
            if(CanInput) m_pButton.SetInput = true;
        };
        
        GetLevelEditorActions.PButton.canceled += context =>
        {
            if(CanInput) m_pButton.SetInput = false;
        };
        
        GetLevelEditorActions.RButton.performed += context =>
        {
            if(CanInput) m_rButton.SetInput = true;
        };
        
        GetLevelEditorActions.RButton.canceled += context =>
        {
            if(CanInput) m_rButton.SetInput = false;
        };
        
        GetLevelEditorActions.SButton.performed += context =>
        {
            if(CanInput) m_sButton.SetInput = true;
        };
        
        GetLevelEditorActions.SButton.canceled += context =>
        {
            if(CanInput) m_sButton.SetInput = false;
        };

        GetLevelEditorActions.DeleteButton.performed += context =>
        {
            if(CanInput) m_deleteButton.SetInput = true;
        };
        
        GetLevelEditorActions.DeleteButton.canceled += context =>
        {
            if(CanInput) m_deleteButton.SetInput = false;
        };
        
        
        GetLevelEditorActions.EscapeButton.performed += context =>
        {
            if(CanInput) m_escapeButton.SetInput = true;
        };
        
        GetLevelEditorActions.EscapeButton.canceled += context =>
        {
            if(CanInput) m_escapeButton.SetInput = false;
        };

        #endregion
        
        #region Debugger

        GetDebuggerActions.Num1.performed += context =>
        {
            if(CanInput) m_num1.SetInput = true;
        };
        
        GetDebuggerActions.Num1.canceled += context =>
        {
            if(CanInput) m_num1.SetInput = false;
        };
        
        GetDebuggerActions.Num2.performed += context =>
        {
            if(CanInput) m_num2.SetInput = true;
        };
        
        GetDebuggerActions.Num2.canceled += context =>
        {
            if(CanInput) m_num2.SetInput = false;
        };
        
        GetDebuggerActions.Num3.performed += context =>
        {
            if(CanInput) m_num3.SetInput = true;
        };
        
        GetDebuggerActions.Num3.canceled += context =>
        {
            if(CanInput) m_num3.SetInput = false;
        };
        
        GetDebuggerActions.Num4.performed += context =>
        {
            if(CanInput) m_num4.SetInput = true;
        };
        
        GetDebuggerActions.Num4.canceled += context =>
        {
            if(CanInput) m_num4.SetInput = false;
        };
        
        GetDebuggerActions.Num5.performed += context =>
        {
            if(CanInput) m_num5.SetInput = true;
        };
        
        GetDebuggerActions.Num5.canceled += context =>
        {
            if(CanInput) m_num5.SetInput = false;
        };

        #endregion
    }
    
    #region PlayerAction

    private PlayerAction m_playerActions;

    private PlayerAction GetPlayerInputActions
    {
        get
        {
            if (m_playerActions == null)
            {
                m_playerActions = new PlayerAction();
                m_playerActions.Enable();
            }

            return m_playerActions;
        }
    }

    private PlayerAction.PlayerActions GetPlayerActions
    {
        get
        {
            return GetPlayerInputActions.Player;
        }
    }

    private PlayerAction.LevelEditorActions GetLevelEditorActions
    {
        get
        {
            return GetPlayerInputActions.LevelEditor;
        }
    }
    

    #endregion

    #region Debugger

    private PlayerAction.DebuggerActions GetDebuggerActions
    {
        get
        {
            return GetPlayerInputActions.Debugger;
        }
    }

    #endregion

    #region PlayerMotionInput

    private InputProperty<Vector2> m_moveInput = new InputProperty<Vector2>();

    private InputProperty<bool> m_jumpInput = new InputProperty<bool>();

    private InputProperty<bool> m_runInput = new InputProperty<bool>();

    private InputProperty<bool> m_sliceInput = new InputProperty<bool>();


    public Vector2 GetMoveInput => m_moveInput.GetInput;

    public bool GetMoveInputDown => m_moveInput.GetInputDown;

    public bool GetMoveInputUp => m_moveInput.GetInputUp;
    
    public bool GetJumpInput => m_jumpInput.GetInput;

    public bool GetJumpInputDown => m_jumpInput.GetInputDown;

    public bool GetJumpInputUp => m_jumpInput.GetInputUp;

    public bool GetRunInput => m_runInput.GetInput;

    public bool GetRunInputDown => m_runInput.GetInputDown;

    public bool GetRunInputUp => m_runInput.GetInputUp;

    public bool GetSliceInput => m_sliceInput.GetInput;

    public bool GetSliceInputDown => m_sliceInput.GetInputDown;

    public bool GetSliceInputUp => m_sliceInput.GetInputUp;
    
    #endregion

    #region LevelEditor

    private InputProperty<bool> m_mouseLeftButton = new InputProperty<bool>();

    private InputProperty<bool> m_mouseRightButton = new InputProperty<bool>();

    private InputProperty<bool> m_mouseMiddleButton = new InputProperty<bool>();
    
    private InputProperty<float> m_mouseScroll = new InputProperty<float>();

    private InputProperty<bool> m_shiftButton = new InputProperty<bool>();

    private InputProperty<bool> m_ctrlButton = new InputProperty<bool>();
    
    private InputProperty<bool> m_zButton = new InputProperty<bool>();
    
    private InputProperty<bool> m_cButton = new InputProperty<bool>();
    
    private InputProperty<bool> m_vButton = new InputProperty<bool>();

    private InputProperty<bool> m_gButton = new InputProperty<bool>();
    
    private InputProperty<bool> m_pButton = new InputProperty<bool>();
    
    private InputProperty<bool> m_rButton = new InputProperty<bool>();
    
    private InputProperty<bool> m_sButton = new InputProperty<bool>();

    private InputProperty<bool> m_deleteButton = new InputProperty<bool>();

    private InputProperty<bool> m_escapeButton = new InputProperty<bool>();

    public bool GetMouseLeftButton => m_mouseLeftButton.GetInput;
    
    public bool GetMouseLeftButtonDown => m_mouseLeftButton.GetInputDown;
    
    public bool GetMouseLeftButtonUp => m_mouseLeftButton.GetInputUp;

    public bool GetMouseRightButton => m_mouseRightButton.GetInput;
    
    public bool GetMouseRightButtonDown => m_mouseRightButton.GetInputDown;
    
    public bool GetMouseRightButtonUp => m_mouseRightButton.GetInputUp;
    
    public bool GetMouseMiddleButton => m_mouseMiddleButton.GetInput;
    
    public bool GetMouseMiddleButtonDown => m_mouseMiddleButton.GetInputDown;
    
    public bool GetMouseMiddleButtonUp => m_mouseMiddleButton.GetInputUp;

    public float GetMouseScroll => m_mouseScroll.GetInput;
    
    public bool GetMouseScrollDown => m_mouseScroll.GetInputDown;
    
    public bool GetMouseScrollUp => m_mouseScroll.GetInputUp;

    public bool GetShiftButton => m_shiftButton.GetInput;

    public bool GetShiftButtonDown => m_shiftButton.GetInputDown;

    public bool GetShiftButtonUp => m_shiftButton.GetInputUp;

    public bool GetCtrlButton => m_ctrlButton.GetInput;
    
    public bool GetCtrlButtonDown => m_ctrlButton.GetInputDown;

    public bool GetCtrlButtonUp => m_ctrlButton.GetInputUp;

    public bool GetZButton => m_zButton.GetInput;
    
    public bool GetZButtonDown => m_zButton.GetInputDown;
    public bool GetZButtonUp => m_zButton.GetInputUp;
    
    public bool GetCButton => m_zButton.GetInput;
    public bool GetCButtonDown => m_cButton.GetInputDown;
    public bool GetCButtonUp => m_cButton.GetInputUp;
    
    public bool GetVButton => m_zButton.GetInput;
    public bool GetVButtonDown => m_vButton.GetInputDown;
    public bool GetVButtonUp => m_vButton.GetInputUp;
    
    public bool GetGButtonDown => m_gButton.GetInputDown;
    public bool GetGButtonUp => m_gButton.GetInputUp;
    
    public bool GetPButtonDown => m_pButton.GetInputDown;
    public bool GetPButtonUp => m_pButton.GetInputUp;
    
    public bool GetRButtonDown => m_rButton.GetInputDown;
    public bool GetRButtonUp => m_rButton.GetInputUp;
    
    public bool GetSButtonDown => m_sButton.GetInputDown;
    public bool GetSButtonUp => m_sButton.GetInputUp;

    public bool GetDelexteButton => m_deleteButton.GetInput;
    
    public bool GetDeleteButtonDown => m_deleteButton.GetInputDown;
    
    public bool GetDeleteButtonUp => m_deleteButton.GetInputUp;
    
    public bool GetEscapeButton => m_escapeButton.GetInput;
    
    public bool GetEscapeButtonDown => m_escapeButton.GetInputDown;
    
    public bool GetEscapeButtonUp => m_escapeButton.GetInputUp;

    #endregion

    #region Debugger

    private InputProperty<bool> m_num1;

    private InputProperty<bool> m_num2;

    private InputProperty<bool> m_num3;

    private InputProperty<bool> m_num4;

    private InputProperty<bool> m_num5;

    public bool GetDebuggerNum1Down => m_num1.GetInputDown;

    public bool GetDebuggerNum1Up => m_num1.GetInputUp;

    public bool GetDebuggerNum2Down => m_num2.GetInputDown;

    public bool GetDebuggerNum2Up => m_num2.GetInputUp;

    public bool GetDebuggerNum3Down => m_num3.GetInputDown;

    public bool GetDebuggerNum3Up => m_num3.GetInputUp;

    public bool GetDebuggerNum4Down => m_num4.GetInputDown;

    public bool GetDebuggerNum4Up => m_num4.GetInputUp;

    public bool GetDebuggerNum5Down => m_num5.GetInputDown;

    public bool GetDebuggerNum5Up => m_num5.GetInputUp;

    #endregion

    #region Actions

    public Action AddEscapeButtonDownAction
    {
        set => m_escapeButton.DownAction += value;
    }
    
    public Action RemoveEscapeButtonDownAction
    {
        set => m_escapeButton.DownAction -= value;
    }

    #endregion
}
}
