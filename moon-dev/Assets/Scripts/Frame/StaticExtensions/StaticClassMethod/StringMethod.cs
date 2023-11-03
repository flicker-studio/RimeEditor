using System;
using System.Text.RegularExpressions;

namespace Frame.Static.Extensions
{
    public static class StringMethod
    {
        [Obsolete("Consider using the extension method under Moon.Extension.Method instead this.")]
        public static string RemoveTrailingNumbers(this string input)
        {
            return Regex.Replace(input, @"\d+$", "");
        }

        public static string ToSHA256(this string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);

            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);

            return BitConverter.ToString(by).Replace("-", "").ToLower();
        }

        public static string ReserveReciprocal(this string str, char c)
        {
            string newStr = "";
            for (var index = str.Length - 1; index >= 0; index--)
            {
                if (str[index] != c) newStr = $"{str[index]}{newStr}";
                else break;
            }

            return newStr;
        }
    }
}

