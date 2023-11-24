using System;
using UnityEngine;
using UnityToolkit;

namespace Moon
{
#if UNITY_EDITOR
    [Serializable]
#endif
    public class RobotModel : Model<RobotModel>
    {
        public Vector2 facingDir => facing == FacingDirection2D.Left ? Vector2.left : Vector2.right;

        public FacingDirection2D facing;
    }
}