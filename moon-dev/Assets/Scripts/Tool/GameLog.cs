using UnityEngine;

public static class GameLog
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogMessage(string message)
    {
        Debug.Log(message);
    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(string message)
    {
        Debug.LogWarning(message);
    }
}