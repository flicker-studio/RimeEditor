using System;
using UnityEngine;
using UnityToolkit;

namespace Moon.Transition
{
    public class WallTransition : ITransition<Robot>
    {
        public bool GetNext(out Type type, StateMachine<Robot> stateMachine, Robot owner)
        {
            type = null;
            Vector2 direction = owner.model.facingDir;
            int count = RayCaster2D.RaycastNonAlloc(owner.transform.position, direction, out var results,
                owner.config.wallCheckDistance, owner.config.wallLayer);
            
            // 巡逻状态下，如果碰到墙壁，就切换方向
            if (stateMachine.CurrentState is PatrolState && count > 0)
            {
                type = typeof(SwitchFacingState);
                return true;
            }
            
            // 切换方向状态下，如果没有碰到墙壁，就切换回巡逻状态
            if(stateMachine.CurrentState is SwitchFacingState && count <=0)
            {
                type = typeof(PatrolState);
                return true;
            }

            return false;
        }
    }
}