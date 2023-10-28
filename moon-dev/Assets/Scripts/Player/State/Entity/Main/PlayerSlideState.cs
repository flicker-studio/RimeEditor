using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;

namespace Character
{
    public class PlayerSlideState : PlayerMainMotionState
    {
        private float m_timer = 0f;

        #region GetProperty

        private bool CheckSuitableSlope => m_playerInformation.CheckSuitableSlope;

        private Rigidbody2D GetRigidbody => m_playerInformation.GetRigidbody;

        private CharacterProperty.PlayerMoveProperty GetMoveProperty => m_playerInformation.GetMoveProperty;

        #endregion
        public PlayerSlideState(BaseInformation information,MotionCallBack motionCallBack):base(information, motionCallBack)
        {
        }

        public override void Motion(BaseInformation information)
        {
            if (CheckSuitableSlope)
            {
                ChangeMotionState(typeof(PlayerMainDefultState));
                return;
            }

            m_timer += Time.fixedDeltaTime;
            GetRigidbody.velocity = GetRigidbody.velocity.NewY(
                -GetMoveProperty.ACCELERATION_CURVE.Evaluate(m_timer / GetMoveProperty.SLICE_TIME_TO_MAXIMUN_SPEED)
                * GetMoveProperty.SLIDE_SPEED);
            GetRigidbody.velocity = GetRigidbody.velocity.NewX(0);
        }
    }

}