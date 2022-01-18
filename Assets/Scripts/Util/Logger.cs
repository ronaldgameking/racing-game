using UnityEngine;
using UnityEditor;

/// <summary>
/// A logger with toggle
/// </summary>
public class Logger
{
    public static bool Debugging = true;
    
    public static void Log(object message)
    {
        if (Debugging)
            Debug.Log(message);
    }
    public static void LogWarning(object message)
    {
        if (Debugging)
            Debug.LogWarning(message);
    }

    //[MenuItem("Tools/Logger enabled (Non-working)")]
    public static void ToggleDebugging()
    {
        Debugging = !Debugging;
        Debug.Log(Debugging ? "Debugging enabled" : "Debugging disabled");
    }
}
