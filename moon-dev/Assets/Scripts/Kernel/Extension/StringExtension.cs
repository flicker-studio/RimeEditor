using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Moon.Kernel.Extension
{
    public static class StringExtension
    {
        public static string RemoveTrailingNumbers(this string input)
        {
            return Regex.Replace(input, @"\d+$", "");
        }

        public static string ToSHA256(this string str)
        {
            var SHA256Data = Encoding.UTF8.GetBytes(str);

            var Sha256 = new SHA256Managed();
            var by = Sha256.ComputeHash(SHA256Data);

            return BitConverter.ToString(by).Replace("-", "").ToLower();
        }

        public static string ReserveReciprocal(this string str, char c)
        {
            var newStr = "";

            for (var index = str.Length - 1; index >= 0; index--)
                if (str[index] != c)
                {
                    newStr = $"{str[index]}{newStr}";
                }
                else
                {
                    break;
                }

            return newStr;
        }
    }
}