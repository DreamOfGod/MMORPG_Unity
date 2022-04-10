//===============================================
//作    者：
//创建时间：2022-04-10 14:54:40
//备    注：
//===============================================

using UnityEngine;
/// <summary>
/// 调式模式日志
/// </summary>
public class DebugLogger
{
    public static void Log(object message)
    {
#if DEBUG_LOG
        Debug.Log(message);
#endif
    }

    public static void LogFormat(string format, params object[] args)
    {
#if DEBUG_LOG
        Debug.LogFormat(format, args);
#endif
    }

    public static void LogError(object message)
    {
#if DEBUG_LOG
        Debug.LogError(message);
#endif
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
#if DEBUG_LOG
        Debug.LogErrorFormat(format, args);
#endif
    }
}