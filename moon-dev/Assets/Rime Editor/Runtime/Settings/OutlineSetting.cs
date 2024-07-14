using UnityEngine;

namespace LevelEditor.Settings
{
    [CreateAssetMenu(menuName = "CustomProperty/OutlineSetting", order = 1, fileName = "OutlineSetting")]
    public class OutlineSetting : ScriptableObject
    {
        public Material OutlineMask;

        public Material OutlineFill;
    }
}