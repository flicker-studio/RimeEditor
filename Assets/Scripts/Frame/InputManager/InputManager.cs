using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    
    public InputManager()
    {
        #region MotionActions

        GetPlayerMotionActions.Move.performed += context =>
        {
            m_moveInput.SetInput = context.ReadValue<Vector2>();
        };

        GetPlayerMotionActions.Move.canceled += context =>
        {
            m_moveInput.SetInput = Vector2.zero;
        };

        GetPlayerMotionActions.Jump.performed += context =>
        {
            m_jumpInput.SetInput = true;
        };

        GetPlayerMotionActions.Jump.canceled += context =>
        {
            m_jumpInput.SetInput = false;
        };

        GetPlayerMotionActions.Run.performed += context =>
        {
            m_runInput.SetInput = true;
        };

        GetPlayerMotionActions.Run.canceled += context =>
        {
            m_runInput.SetInput = false;
        };
        
        GetPlayerMotionActions.Slice.performed += context =>
        {
            m_sliceInput.SetInput = true;
        };
        
        GetPlayerMotionActions.Slice.canceled += context =>
        {
            m_sliceInput.SetInput = false;
        };
        
        #endregion

        #region Debugger

        GetDebuggerActions.Num1.performed += context =>
        {
            m_num1.SetInput = true;
        };
        
        GetDebuggerActions.Num1.canceled += context =>
        {
            m_num1.SetInput = false;
        };
        
        GetDebuggerActions.Num2.performed += context =>
        {
            m_num2.SetInput = true;
        };
        
        GetDebuggerActions.Num2.canceled += context =>
        {
            m_num2.SetInput = false;
        };
        
        GetDebuggerActions.Num3.performed += context =>
        {
            m_num3.SetInput = true;
        };
        
        GetDebuggerActions.Num3.canceled += context =>
        {
            m_num3.SetInput = false;
        };
        
        GetDebuggerActions.Num4.performed += context =>
        {
            m_num4.SetInput = true;
        };
        
        GetDebuggerActions.Num4.canceled += context =>
        {
            m_num4.SetInput = false;
        };
        
        GetDebuggerActions.Num5.performed += context =>
        {
            m_num5.SetInput = true;
        };
        
        GetDebuggerActions.Num5.canceled += context =>
        {
            m_num5.SetInput = false;
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

    private PlayerAction.PlayerMotionActions GetPlayerMotionActions
    {
        get
        {
            return GetPlayerInputActions.PlayerMotion;
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
}
