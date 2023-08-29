using System.Collections;
using System.Collections.Generic;using System.Security.Cryptography;
using UnityEngine;

public abstract class MotionState
{
    protected InputController m_inputController;

    protected ComponentController m_componentController;

    protected CharacterProperty m_characterProperty;

    protected PlayerColliding m_playerColliding;

    #region GetProperty

    protected CharacterProperty.PlayerMoveProperty GetMoveProperty => m_characterProperty.MoveProperty;
    
    protected CharacterProperty.PlayerJumpProperty GetJumpProperty => m_characterProperty.JumpProperty;

    protected Rigidbody2D GetRigidbody => m_componentController.Rigidbody;

    protected CapsuleCollider2D GetCollider => m_componentController.CapsuleCollider;

    protected InputData GetInputData => m_inputController.GetInputData;

    #endregion

    public MotionState(PlayerInformation information)
    {
        m_inputController = information.InputController;
        m_componentController = information.ComponentController;
        m_characterProperty = information.CharacterProperty;
        m_playerColliding = information.PlayerColliding;
    }
    public abstract void Motion(PlayerInformation playerInformation);
    
    protected void ChangeMoveState(MotionState motionState)
    {
        EventCenterManager.Instance.EventTrigger<MotionState>(GameEvent.ChangeMoveState,motionState);
    }
}
