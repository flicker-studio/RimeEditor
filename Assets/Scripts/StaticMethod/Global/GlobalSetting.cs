using UnityEngine;

public static class GlobalSetting
{
    public struct LayerMasks
    {
        public readonly static LayerMask Ground = LayerMask.NameToLayer("Ground");
    }
}
