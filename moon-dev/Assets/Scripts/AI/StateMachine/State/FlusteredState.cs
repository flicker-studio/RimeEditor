using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 慌乱的状态 -> 翻倒在地
    /// </summary>
    internal class FlusteredState : State<Robot>
    {
        public override void OnEnter(Robot owner)
        {
            // owner.animator.SetTrigger("Flustered");
        }
    }
}