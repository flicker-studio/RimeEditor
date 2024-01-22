using System.Text.RegularExpressions;

namespace Moon.Kernel.Extension
{
    /// <summary>
    /// Static extension methods
    /// </summary>
    public static class Method
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

        /// <summary>
        ///     Remove the number at the end of a string
        /// </summary>
        /// <remarks>
        ///     Note! The function doesn't remove spaces!
        /// </remarks>
        /// <param name="input">The string you entered</param>
        /// <returns>Strings stripped of numbers</returns>
        public static string RemoveTrailingNumbers(this string input)
        {
            //TODO:Optimized memory performance
            return Regex.Replace(input, @"\d+$", "");
        }
    }
}