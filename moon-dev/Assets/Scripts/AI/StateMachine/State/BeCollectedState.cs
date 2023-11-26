using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 被玩家收纳的状态
    /// </summary>
    public class BeCollectedState: State<Robot>
    {
        public override void OnEnter(Robot owner)
        {
            owner.meshRenderer.enabled = false;
        }

        public override void OnExit(Robot owner)
        {
            owner.meshRenderer.enabled = true;
        }
    }
}