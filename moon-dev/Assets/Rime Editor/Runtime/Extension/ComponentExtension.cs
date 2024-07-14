using UnityEngine;

namespace LevelEditor.Extension
{
    public static class ComponentExtension
    {
        public static void CopyComponent(this Component original, Component target)
        {
            var type   = target.GetType();
            var fields = type.GetFields();
            foreach (var field in fields) field.SetValue(original, field.GetValue(target));
        }
    }
}