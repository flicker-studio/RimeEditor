using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    
    public InputManager()
    {
        GetPlayerActions.Move.performed += context =>
        {
            m_moveInput = context.ReadValue<Vector2>();
        };

        GetPlayerActions.Move.canceled += context =>
        {
            m_moveInput = Vector2.zero;
        };

        GetPlayerActions.Jump.performed += context =>
        {
            m_jumpInput = true;
        };

        GetPlayerActions.Jump.canceled += context =>
        {
            m_jumpInput = false;
        };
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

    private PlayerAction.PlayerMotionActions GetPlayerActions
    {
        get
        {
            return GetPlayerInputActions.PlayerMotion;
        }
    }

    #endregion

    #region PlayerInput

    private Vector2 m_moveInput;

    private bool m_jumpInput;

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
    
    #endregion
}
