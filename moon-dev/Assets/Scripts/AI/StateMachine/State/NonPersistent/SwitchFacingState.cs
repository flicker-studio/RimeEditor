using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 切换朝向状态
    /// </summary>
    internal class SwitchFacingState : NonPersistentState
    {
        public override void OnEnter(Robot owner)
        {
            owner.model.facing = owner.model.facing == FacingDirection2D.Left
                ? FacingDirection2D.Right
                : FacingDirection2D.Left;
            m_canSwitch = false;
            m_next = typeof(PatrolState);
            // TODO 假装有动画 且1s后播放完毕
            m_timer = Timer.Register(1, () =>
            {
                m_canSwitch = true;
            });
        }
        
    }
}