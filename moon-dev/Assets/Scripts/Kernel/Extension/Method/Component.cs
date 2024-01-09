namespace Moon.Kernel.Extension.Method
{
    /// <summary>
    ///     <see cref="UnityEngine.Component" /> static extension class
    /// </summary>
    public static class Component
    {
        /// <summary>
        ///     Set the fields of all components in <paramref name="original" /> to the corresponding values in
        ///     <paramref name="target" />
        /// </summary>
        /// <param name="original">The component to which the value is assigned</param>
        /// <param name="target">The component that gets the value</param>
        public static void CopyComponentValue(this UnityEngine.Component original, UnityEngine.Component target)
        {
            var type = target.GetType();
            var fields = type.GetFields();

            foreach (var field in fields) field.SetValue(original, field.GetValue(target));
        }
    }
}