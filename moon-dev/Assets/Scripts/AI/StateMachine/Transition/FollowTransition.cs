using System;
using UnityToolkit;

namespace Moon
{
    public class FollowTransition : ITransition<Robot>
    {
        public bool GetNext(out Type type, StateMachine<Robot> stateMachine, Robot owner)
        {
            type = null;
            if (owner.followTarget != null)
            {
                type = typeof(FollowState);
                return true;
            }

            if (stateMachine.CurrentState is FollowState && owner.followTarget == null)
            {
                type = typeof(PatrolState);
                return true;
            }

            return false;
        }
    }
}