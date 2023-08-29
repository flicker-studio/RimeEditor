using UnityEngine;

public class CoyoteTimer : CompensateTimer
{
    public override bool CheckTimer(PlayerInformation playerInformation)
    {
        bool check = m_timer < playerInformation.CharacterProperty.JumpProperty.COYOTE_TIME;
        
        if (!playerInformation.PlayerColliding.IsGround && !playerInformation.InputController.GetInputData.JumpInput)
        {
            m_timer += Time.fixedDeltaTime;
        }
        else if(playerInformation.PlayerColliding.IsGround && !playerInformation.InputController.GetInputData.JumpInput)
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
