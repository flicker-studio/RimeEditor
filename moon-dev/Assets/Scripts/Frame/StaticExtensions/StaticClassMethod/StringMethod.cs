using System.Text.RegularExpressions;

namespace Frame.Static.Extensions
{
    public static class StringMethod
    {
        public static string RemoveInvalidCharacter(this string input)
        {
            input = Regex.Replace(input, "<.*?>", string.Empty);
            return Regex.Replace(input, @"\d+$", "");
        }
    }
}

