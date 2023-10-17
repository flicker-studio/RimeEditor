using System.Text.RegularExpressions;

namespace Frame.Static.Extensions
{
    public static class StringMethod
    {
        public static string RemoveTrailingNumbers(this string input) {
            return Regex.Replace(input, @"\d+$", "");
        }
    }
}

