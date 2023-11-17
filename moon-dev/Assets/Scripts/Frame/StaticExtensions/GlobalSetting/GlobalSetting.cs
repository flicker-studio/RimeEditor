using Frame.StateMachine;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Frame.Static.Global
{
    public static class GlobalSetting
    {
        public struct Scenes
        {
            public readonly static string LEVEL_EDITOR = "LevelEditor";
            public readonly static string LEVEL_PLAY = "LevelPlay";
        }
        
        public struct LayerMasks
        {
            public readonly static LayerMask GROUND = LayerMask.NameToLayer("Ground");
        }
        
        public struct Tags
        {
            public readonly static string CONTROL_HANDLE = "ControlHandle";
        }
    
        public struct ObjNameTag
        {
            public readonly static string RIGIDBODY_TAG = "<rigidbody>";
        }
    }

}
