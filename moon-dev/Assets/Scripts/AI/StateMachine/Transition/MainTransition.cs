using System;
using UnityEngine;
using UnityToolkit;

namespace Moon
{
    internal class MainTransition : ITransition<Robot>
    {
        public bool GetNext(out Type type, StateMachine<Robot> stateMachine, Robot owner)
        {
            // 当前状态是暂时过渡状态
            if (stateMachine.CurrentState is NonPersistentState nonPersistentState)
            {
                if(nonPersistentState.canSwitch)
                {
                    type = nonPersistentState.next;
                    return true;
                }
                type = null;
                return false;
            }
            
            //当前状态是持久状态
            Vector3 pos = owner.transform.position;
            Vector2 direction = owner.model.facingDir;
            
            // 检测前方是否有墙
            int wallColCount = RayCaster2D.RaycastNonAlloc(pos, direction, out var wallResults,
                owner.config.facingCheckDistance, owner.config.wallLayer);
            // 检测前方是否有障碍物
            int playerColCount = RayCaster2D.RaycastNonAlloc(pos, owner.model.facingDir,
                out var blockResults,
                owner.config.facingCheckDistance, owner.config.playerLayer);
            
            
            // 撞到玩家
            if(stateMachine.CurrentState is PatrolState && playerColCount > 0)
            {
                type = typeof(HitPlayerState);
                return true;
            }
            
            // 撞到墙
            if (stateMachine.CurrentState is PatrolState && wallColCount > 0)
            {
                type = typeof(SwitchFacingState);
                return true;
            }
            type = null;
            return false;
        }
    }
}