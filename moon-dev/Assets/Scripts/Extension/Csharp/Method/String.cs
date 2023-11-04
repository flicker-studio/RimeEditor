using System.Text.RegularExpressions;

namespace Moon.Extension.Csharp.Method
{
    /// <summary>
    ///     <see langword="string" /> static extension class
    /// </summary>
    public static class String
    {
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
            return Regex.Replace(input, @"\d+$", "");
        }
    }
}