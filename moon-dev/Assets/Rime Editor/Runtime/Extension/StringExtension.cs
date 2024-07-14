using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LevelEditor.Extension
{
    /// <summary>
    ///     Extension method class of <see langword="string" />
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        ///     Remove the number at the end of the string
        /// </summary>
        public static string RemoveTrailingNumbers(this string input)
        {
            return Regex.Replace(input, @"\d+$", "");
        }

        /// <summary>
        ///     Convert the string to a lowercase SHA 256 hash
        /// </summary>
        public static string ToSHA256(this string str)
        {
            var sha256Data = Encoding.UTF8.GetBytes(str);

            var sha256 = new SHA256Managed();
            var by     = sha256.ComputeHash(sha256Data);

            return BitConverter.ToString(by).Replace("-", "").ToLower();
        }

        /// <summary>
        ///     Gets the  string suffix starting with the last <paramref name="c" /> (not included)
        /// </summary>
        /// <remarks>
        ///     If the character does not exist in the string, the source character is returned
        /// </remarks>
        /// <example>
        ///     Kernel (e) ==> l
        ///     <br />
        ///     Kernel (z) ==> Kernel
        /// </example>
        public static string GetSuffix(this string str, char c)
        {
            return str.Contains(c) ? str.Split(c).Last() : str;
        }
    }
}