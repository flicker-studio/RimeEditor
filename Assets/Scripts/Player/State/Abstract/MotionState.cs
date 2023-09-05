using System;
using System.Collections;
using System.Collections.Generic;using System.Security.Cryptography;
using UnityEngine;

public abstract class MotionState
{
    private InputController m_inputController;

    private ComponentController m_componentController;

    private CharacterProperty m_characterProperty;

    private PlayerColliding m_playerColliding;

    private PlayerRaycasting m_playerRaycasting;

    private CheckStatesCallBack m_checkStatesCallBack;

    #region GetProperty

    protected CharacterProperty.PlayerMoveProperty GetMoveProperty => m_characterProperty.MoveProperty;
    
    protected CharacterProperty.PlayerJumpProperty GetJumpProperty => m_characterProperty.JumpProperty;

    protected CharacterProperty.PlayerGroundCheckParameter GetGroundCheck => m_characterProperty.GroundCheckParameter;

    protected CharacterProperty.PlayerCeilingCheckParameter GetCeilingCheck =>
        m_characterProperty.CeilingCheckParameter;

    protected CharacterProperty.PlayerPerpendicularOnGround GetPerpendicularOnGround =>
        m_characterProperty.PerpendicularOnGround;

    protected Rigidbody2D GetRigidbody => m_componentController.Rigidbody;

    protected Collider2D GetCollider => m_componentController.Collider;

    protected InputData GetInputData => m_inputController.GetInputData;

    protected List<Vector2> GetRaycastGroundPoints => m_playerRaycasting.GetRaycastPointsGround;
    
    protected List<Vector2> GetRaycastCheckPoints => m_playerRaycasting.GetRaycastPointsCheck;

    protected bool GetIsGround => m_playerColliding.IsGround;

    protected bool GetIsCeiling => m_playerColliding.IsCeiling;

    protected List<Type> CheckStates => m_checkStatesCallBack?.Invoke(false);
    
    protected List<Type> CheckGlobalStates => m_checkStatesCallBack?.Invoke(true);

    #endregion

    public MotionState(PlayerInformation information,CheckStatesCallBack checkStatesCallBack)
    {
        m_inputController = information.InputController;
        m_componentController = information.ComponentController;
        m_characterProperty = information.CharacterProperty;
        m_playerColliding = information.PlayerColliding;
        m_playerRaycasting = information.PlayerRaycasting;
        m_checkStatesCallBack = checkStatesCallBack;
    }

    public abstract void Motion(PlayerInformation playerInformation);
    
    protected void ChangeMoveState(MOTIONSTATEENUM motionStateEnum)
    {
        EventCenterManager.Instance.EventTrigger<MOTIONSTATEENUM>(GameEvent.ChangeMoveState,motionStateEnum);
    }
}
