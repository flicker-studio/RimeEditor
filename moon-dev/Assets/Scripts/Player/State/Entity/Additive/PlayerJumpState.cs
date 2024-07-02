using Frame.StateMachine;
using Moon.Kernel.Extension;
using UnityEngine;

namespace Character
{
    public class PlayerJumpState : PlayerAdditiveMotionState
    {
        #region GetProperty

        private Rigidbody2D GetRigidbody => m_playerInformation.GetRigidbody;

        private CharacterProperty.PlayerJumpProperty GetJumpProperty => m_playerInformation.GetJumpProperty;

        private bool GetIsCeiling => m_playerInformation.GetIsCeiling;

        private MotionInputData GetMotionInputData => m_playerInformation.GetMotionInputData;

        #endregion

        public override void Motion(BaseInformation information)
        {
            m_endTimmer += Time.fixedDeltaTime;

            GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetJumpProperty.PLAYER_MAXIMAL_JUMP_SPEED *
                                                               (1 - GetJumpProperty.ACCELERATION_CURVE.Evaluate(m_endTimmer / GetJumpProperty.PLAYER_MAXIMAL_JUMP_TIME)));

            if (m_endTimmer >= GetJumpProperty.PLAYER_MAXIMAL_JUMP_TIME ||
                m_endTimmer >= GetJumpProperty.PLAYER_SMALLEST_JUMP_TIME && !GetMotionInputData.JumpInput ||
                GetIsCeiling)
            {
                GetRigidbody.velocity = GetRigidbody.velocity.NewY(GetJumpProperty.PLAYER_JUMP_FINISH_SPEED_COMPENSATION);
                RemoveState();
            }
        }

        public PlayerJumpState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
        }
    }
}