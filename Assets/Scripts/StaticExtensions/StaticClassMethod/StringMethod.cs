using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StringMethod
{
    public static string RemoveTrailingNumbers(this string input) {
        return Regex.Replace(input, @"\d+$", "");
    }
}
