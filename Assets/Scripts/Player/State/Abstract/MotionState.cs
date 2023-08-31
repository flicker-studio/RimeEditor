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

    protected CharacterProperty.PlayerGroundCheckParameter GetGroundCheck => m_characterProperty.GroundCheckParameter;

    protected CharacterProperty.PlayerCeilingCheckParameter GetCeilingCheck =>
        m_characterProperty.CeilingCheckParameter;

    protected CharacterProperty.PlayerOrthogonalOnGround GetOrthogonalOnGround =>
        m_characterProperty.OrthogonalOnGround;

    protected Rigidbody2D GetRigidbody => m_componentController.Rigidbody;

    protected Collider2D GetCollider => m_componentController.Collider;

    protected InputData GetInputData => m_inputController.GetInputData;

    protected bool GetIsGround => m_playerColliding.IsGround;

    protected bool GetIsCeiling => m_playerColliding.IsCeiling;

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
