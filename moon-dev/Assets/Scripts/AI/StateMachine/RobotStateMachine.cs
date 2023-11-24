using System;
using System.Collections.Generic;
using UnityToolkit;

namespace Moon
{
    public sealed class RobotStateMachine : StateMachine<Robot>
    {
        public RobotStateMachine(Robot owner) : base(owner)
        {
        }
    }
}