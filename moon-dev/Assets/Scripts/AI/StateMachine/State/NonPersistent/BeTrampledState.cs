using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 被玩家踩到后
    /// </summary>
    internal class BeTrampledState: NonPersistentState
    {
        public override void OnEnter(Robot owner)
        {
            // owner.animator.SetTrigger("BeTrampled");
            m_canSwitch = false;
            m_next = typeof(FlusteredState);
            Timer.Register(1, () =>
            {
                m_canSwitch = true;
            });
        }
    }
}