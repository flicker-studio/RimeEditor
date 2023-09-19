using System;
using System.Collections.Generic;using System.Security.Cryptography;
using UnityEngine;

public abstract class PlayerMotionState : MotionState
{
    private MotionInputController m_MotionInputController;

    private ComponentController m_componentController;

    private CharacterProperty m_characterProperty;

    private PlayerColliding m_playerColliding;

    private PlayerRaycasting m_playerRaycasting;

    private MotionCallBack m_motionCallBack;

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

    protected MotionInputData GetMotionInputData => m_MotionInputController.GetMotionInputData;

    protected List<Vector2> GetRaycastGroundPoints => m_playerRaycasting.GetRaycastPointsGround;
    
    protected List<Vector2> GetRaycastCheckPoints => m_playerRaycasting.GetRaycastPointsCheck;

    protected bool GetIsGround => m_playerColliding.IsGround;

    protected bool GetIsCeiling => m_playerColliding.IsCeiling;

    protected List<Type> CheckStates => m_motionCallBack.CheckStatesCallBack?.Invoke();
    
    protected List<Type> CheckGlobalStates => m_motionCallBack.CheckGlobalStatesCallBack?.Invoke();

    protected bool CheckSuitableSlope => GetRaycastCheckPoints.CalculateBestFitLine().GetOrthogonalVector().y
                                           > Mathf.Cos(GetPerpendicularOnGround.CHECK_POINT_ANGLE * Mathf.Deg2Rad);

    #endregion

    public PlayerMotionState(BaseInformation information,MotionCallBack motionCallBack) : base(information,motionCallBack)
    {
        PlayerInformation playerInformation = information as PlayerInformation;
        m_MotionInputController = playerInformation.MotionInputController;
        m_componentController = playerInformation.ComponentController;
        m_characterProperty = playerInformation.CharacterProperty;
        m_playerColliding = playerInformation.PlayerColliding;
        m_playerRaycasting = playerInformation.PlayerRaycasting;
        m_motionCallBack = motionCallBack;
    }
}
