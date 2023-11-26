using UnityEngine;
using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 巡逻状态
    /// </summary>
    internal class PatrolState : State<Robot>
    {
        public override void OnUpdate(Robot owner)
        {
            float patrolSpeed = owner.config.patrolSpeed;
            float facing = owner.model.facing == FacingDirection2D.Left ? -1 : 1;
            owner.transform.Translate(Vector2.right * (patrolSpeed * facing * Time.deltaTime));
        }

        public override void OnExit(Robot owner)
        {
            
        }
    }
}