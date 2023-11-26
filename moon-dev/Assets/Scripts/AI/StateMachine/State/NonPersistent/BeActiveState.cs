using System;
using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 倒地后被激活（玩家踩）的状态
    /// </summary>
    internal class BeActiveState: NonPersistentState
    {
        public override void OnEnter(Robot owner)
        {
            m_canSwitch = false;
            m_next = typeof(IdleState);
            // TODO 假装有动画 且1s后播放完毕
            m_timer = Timer.Register(1, () =>
            {
                m_canSwitch = true;
            });
        }

    }
}