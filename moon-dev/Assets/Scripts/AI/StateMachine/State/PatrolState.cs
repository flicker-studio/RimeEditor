using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 巡逻状态
    /// </summary>
    public class PatrolState : State<Robot>
    {
        public override void OnUpdate(Robot owner)
        {
            float patrolSpeed = owner.config.patrolSpeed;
            float facing = owner.model.facing == FacingDirection2D.Left ? -1 : 1;
            owner.rb2D.velocity = new UnityEngine.Vector2(patrolSpeed, 0) * facing;
        }
    }
}