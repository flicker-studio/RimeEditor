using UnityToolkit;

namespace Moon
{
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