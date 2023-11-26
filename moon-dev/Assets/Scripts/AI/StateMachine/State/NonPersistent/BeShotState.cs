namespace Moon
{
    /// <summary>
    /// 被发射的状态
    /// </summary>
    public class BeShotState : NonPersistentState
    {
        public override void OnEnter(Robot owner)
        {
            m_canSwitch = false;
            m_next = typeof(BeTrampledState);
        }

        public override void OnUpdate(Robot owner)
        {
            if (owner.rb2D.velocity.magnitude < GameConst.Tolerance)
            {
                m_canSwitch = true;
                return;
            }
            owner.transform.position = owner.rb2D.transform.position;
        }
    }
}