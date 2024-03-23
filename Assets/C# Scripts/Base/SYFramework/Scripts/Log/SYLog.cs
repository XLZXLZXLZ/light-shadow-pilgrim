using System;
using System.Collections.Generic;
using UnityEngine;

public static class SYLog
{
    // TODO:让他可以在GlobalSettings面板上更改是否可以Log
    public static void Log(string message)
    {
        Debug.Log(message);
    }

    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    public static void LogError(string message)
    {
        Debug.LogError(message);
    }
}



