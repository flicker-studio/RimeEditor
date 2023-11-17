using Frame.StateMachine;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Frame.Static.Global
{
    public static class GlobalSetting
    {
        public struct LayerMasks
        {
            public readonly static LayerMask GROUND = LayerMask.NameToLayer("Ground");
        }
        
        public struct Tags
        {
            public readonly static string CONTROLHANDLE = "ControlHandle";
        }
    
        public struct ObjNameTag
        {
            public readonly static string rigidbodyTag = "<rigidbody>";
        }
    }

}
