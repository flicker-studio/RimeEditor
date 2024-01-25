using System.Collections.Generic;
using Frame.StateMachine;
using Moon.Kernel.Extension;
using UnityEngine;

namespace Character
{
    public class PlayerPerpendicularGroundState : PlayerAdditiveMotionState
    {
        private List<Vector2> m_raycastPoints;

        #region GetProperty

        private bool GetIsGround => m_playerInformation.GetIsGround;

        private List<Vector2> GetRaycastGroundPoints => m_playerInformation.GetRaycastGroundPoints;

        private CharacterProperty.PlayerPerpendicularOnGround GetPerpendicularOnGround =>
            m_playerInformation.GetPerpendicularOnGround;

        private Rigidbody2D GetRigidbody => m_playerInformation.GetRigidbody;

        #endregion

        public override void Motion(BaseInformation information)
        {
            if (GetIsGround)
            {
                m_raycastPoints = GetRaycastGroundPoints;
                if (m_raycastPoints == null || m_raycastPoints.Count <= GetPerpendicularOnGround.NEGLECTED_POINTS) return;

                GetRigidbody.transform.up = m_raycastPoints.CalculateBestFitLine().GetOrthogonalVector();
            }
        }

        public PlayerPerpendicularGroundState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
        }
    }
}