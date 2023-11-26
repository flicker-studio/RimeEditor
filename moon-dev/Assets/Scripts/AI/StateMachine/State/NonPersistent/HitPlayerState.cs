using UnityEngine;
using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 机器人撞到玩家的状态
    /// </summary>
    internal class HitPlayerState: NonPersistentState
    {
        public override void OnEnter(Robot owner)
        {
            // owner.animator.SetTrigger("HitPlayer");
            m_canSwitch = false;
            m_next = typeof(IdleState);
            Timer.Register(1, () =>
            {
                m_canSwitch = true;
            });
        }
    }
}