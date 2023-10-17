using UnityEngine;

namespace Character
{
    public class CoyoteTimer
    {
        private float m_timer = 0;
        public bool CheckTimer(PlayerInformation playerInformation)
        {
            bool check = m_timer < playerInformation.CharacterProperty.JumpProperty.COYOTE_TIME;
        
            if (!playerInformation.PlayerColliding.IsGround && !playerInformation.MotionInputController.GetMotionInputData.JumpInput)
            {
                m_timer += Time.fixedDeltaTime;
            }
            else if(playerInformation.PlayerColliding.IsGround && !playerInformation.MotionInputController.GetMotionInputData.JumpInput)
            {
                m_timer = 0;
            }
            else
            {
                m_timer = playerInformation.CharacterProperty.JumpProperty.COYOTE_TIME;
            }

            return check;
        }
    }
}
