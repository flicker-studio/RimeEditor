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

            // 如果有障碍物 则速度为0
            int blockCount = RayCaster2D.RaycastNonAlloc(owner.transform.position, owner.model.facingDir,
                out var blockResults,
                owner.config.facingCheckDistance, owner.config.blockLayer);
            
            if (blockCount > 0) // 1是自己
            {
                patrolSpeed = 0;
            }

            owner.rb2D.velocity = new UnityEngine.Vector2(patrolSpeed, 0) * facing;
        }

        public override void OnExit(Robot owner)
        {
            owner.rb2D.velocity = UnityEngine.Vector2.zero;
        }
    }
}