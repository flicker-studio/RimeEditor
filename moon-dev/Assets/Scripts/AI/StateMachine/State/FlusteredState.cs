using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 慌乱的状态 -> 翻倒在地
    /// </summary>
    public class FlusteredState : State<Robot>
    {
        public override void OnEnter(Robot owner)
        {
            owner.rb2D.velocity = UnityEngine.Vector2.zero;
            // owner.animator.SetTrigger("Flustered");
        }
    }
}