using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    
    public InputManager()
    {
        #region MotionActions

        GetPlayerMotionActions.Move.performed += context =>
        {
            m_moveInput = context.ReadValue<Vector2>();
        };

        GetPlayerMotionActions.Move.canceled += context =>
        {
            m_moveInput = Vector2.zero;
        };

        GetPlayerMotionActions.Jump.performed += context =>
        {
            m_jumpInput = true;
        };

        GetPlayerMotionActions.Jump.canceled += context =>
        {
            m_jumpInput = false;
        };

        GetPlayerMotionActions.Run.performed += context =>
        {
            m_runInput = true;
        };

        GetPlayerMotionActions.Run.canceled += context =>
        {
            m_runInput = false;
        };

        #endregion

        #region SliceActions

        GetPlayerSliceActions.Slice.performed += context =>
        {
            m_sliceInput = true;
        };
        
        GetPlayerSliceActions.Slice.canceled += context =>
        {
            m_sliceInput = false;
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
    
    private PlayerAction.PlayerSliceActions GetPlayerSliceActions
    {
        get
        {
            return GetPlayerInputActions.PlayerSlice;
        }
    }

    #endregion

    #region PlayerMotionInput

    private Vector2 m_moveInput;

    private bool m_jumpInput;

    private bool m_runInput;

    public Vector2 GetMoveInput
    {
        get
        {
            return m_moveInput;
        }
    }

    public bool GetJumpInput
    {
        get
        {
            return m_jumpInput;
        }
    }
    
    public bool GetRunInput
    {
        get
        {
            return m_runInput;
        }
    }
    
    #endregion

    #region PlayerMotionInput

    private bool m_sliceInput;

    public bool GetSliceInput
    {
        get
        {
            return m_sliceInput;
        }
    }

    #endregion
}
