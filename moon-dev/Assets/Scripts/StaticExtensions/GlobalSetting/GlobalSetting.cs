using Frame.StateMachine;
using UnityEngine;

namespace Frame.Static.Global
{
    public static class GlobalSetting
    {
        public struct LayerMasks
        {
            public readonly static LayerMask GROUND = LayerMask.NameToLayer("Ground");
        }
    
        public struct ObjNameTag
        {
            public readonly static string rigidbodyTag = "<rigidbody>";
        }
    }

}
