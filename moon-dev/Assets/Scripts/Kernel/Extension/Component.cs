namespace Moon.Kernel.Extension
{
    public static class Component
    {
        public static void CopyComponent(this UnityEngine.Component original, UnityEngine.Component target)
        {
            var type = target.GetType();
            var fields = type.GetFields();
            foreach (var field in fields) field.SetValue(original, field.GetValue(target));
        }
    }
}