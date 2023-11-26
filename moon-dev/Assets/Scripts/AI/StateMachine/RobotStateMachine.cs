using System;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Moon
{
    public sealed class RobotStateMachine : StateMachine<Robot>
    {
        public RobotStateMachine(Robot owner) : base(owner)
        {
            owner.headTrigger.Enter2D += TopEnter2D;
        }

        ~RobotStateMachine()
        {
            Owner.headTrigger.Enter2D -= TopEnter2D;
        }
        
        private void TopEnter2D(Collider2D collider)
        {
            if (!collider.CompareTag(Owner.config.playerTag)) return;
            // 被玩家踩到
            if (collider.transform.position.y <= Owner.transform.position.y + Owner.config.topOffsetY) return;
            // 倒地状态下被踩到
            if (CurrentState is FlusteredState)
            {
                Change<BeActiveState>();
            }
            //其他状态下被踩到
            else
            {
                Change<BeTrampledState>();
            }
        }

        public IEnumerable<State<Robot>> GetAll()
        {
            return stateDic.Values;
        }
    }
}