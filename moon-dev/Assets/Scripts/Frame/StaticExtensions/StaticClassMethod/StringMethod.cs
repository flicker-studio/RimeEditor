using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        
        public static string ToSHA256(this string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
    
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);
     
            return BitConverter.ToString(by).Replace("-", "").ToLower();
        }

        public static string ReserveReciprocal(this string str,char c)
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

