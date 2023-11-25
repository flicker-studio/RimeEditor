using UnityToolkit;

namespace Moon
{
    /// <summary>
    /// 切换朝向状态
    /// </summary>
    public class SwitchFacingState : State<Robot>
    {
        public override void OnEnter(Robot owner)
        {
            owner.model.facing = owner.model.facing == FacingDirection2D.Left
                ? FacingDirection2D.Right
                : FacingDirection2D.Left;
        }
    }
}