using System;
using UnityToolkit;

namespace Moon
{
    public abstract class NonPersistentState : State<Robot>
    {
        protected Timer m_timer;
        protected Type m_next;
        public Type next => m_next;
        protected bool m_canSwitch = true;
        public bool canSwitch => m_canSwitch;
        public override void OnExit(Robot owner)
        {
            m_canSwitch = true;
            m_timer?.Cancel();
            m_timer = null;
        }
    }
}