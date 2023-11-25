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
            owner.topTrigger.Enter2D += TopEnter2D;
        }

        private void TopEnter2D(Collider2D collider)
        {
            if (!collider.CompareTag(Owner.config.playerTag)) return;
            Debug.Log("TopEnter2D");
            if (CurrentState is FlusteredState)
            {
                Change<PatrolState>();
            }
            else
            {
                Change<FlusteredState>();
            }
        }

        public IEnumerable<State<Robot>> GetAll()
        {
            return stateDic.Values;
        }
        
    }
}