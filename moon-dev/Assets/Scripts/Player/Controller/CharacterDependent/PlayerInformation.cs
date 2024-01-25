using System.Collections.Generic;
using Frame.StateMachine;
using Moon.Kernel.Extension;
using UnityEngine;

namespace Character
{
    public class PlayerInformation : BaseInformation
    {
        public MotionInputController MotionInputController;

        public ComponentController ComponentController;

        public CharacterProperty CharacterProperty;

        public PlayerColliding PlayerColliding;

        public PlayerRaycasting PlayerRaycasting;

        #region GetProperty

        public CharacterProperty.PlayerMoveProperty GetMoveProperty => CharacterProperty.MoveProperty;

        public CharacterProperty.PlayerJumpProperty GetJumpProperty => CharacterProperty.JumpProperty;

        public CharacterProperty.PlayerGroundCheckParameter GetGroundCheck => CharacterProperty.GroundCheckParameter;

        public CharacterProperty.PlayerCeilingCheckParameter GetCeilingCheck =>
            CharacterProperty.CeilingCheckParameter;

        public CharacterProperty.PlayerPerpendicularOnGround GetPerpendicularOnGround =>
            CharacterProperty.PerpendicularOnGround;

        public Rigidbody2D GetRigidbody => ComponentController.Rigidbody;

        public Collider2D GetCollider => ComponentController.Collider;

        public MotionInputData GetMotionInputData => MotionInputController.GetMotionInputData;

        public List<Vector2> GetRaycastGroundPoints => PlayerRaycasting.GetRaycastPointsGround;

        public List<Vector2> GetRaycastCheckPoints => PlayerRaycasting.GetRaycastPointsCheck;

        public bool GetIsGround => PlayerColliding.IsGround;

        public bool GetIsCeiling => PlayerColliding.IsCeiling;

        public bool CheckSuitableSlope => GetRaycastCheckPoints.CalculateBestFitLine().GetOrthogonalVector().y
                                          > Mathf.Cos(GetPerpendicularOnGround.CHECK_POINT_ANGLE * Mathf.Deg2Rad);

        #endregion

        public PlayerInformation(Transform transform)
        {
            MotionInputController = new MotionInputController();
            ComponentController = new ComponentController(transform);
            CharacterProperty = Resources.Load<CharacterProperty>("GlobalSettings/CharacterProperty");
            PlayerColliding = new PlayerColliding(transform, CharacterProperty);
            PlayerRaycasting = new PlayerRaycasting(CharacterProperty, ComponentController);
        }
    }
}