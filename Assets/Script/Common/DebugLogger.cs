//===============================================
//作    者：
//创建时间：2022-04-10 14:54:40
//备    注：
//===============================================

using System.Diagnostics;

/// <summary>
/// 调式模式日志
/// 需要格式化字符串的使用模板字符串
/// </summary>
public class DebugLogger
{
    [Conditional(ScriptingDefineSymbols.DebugLog)]
    public static void Log(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    [Conditional(ScriptingDefineSymbols.DebugLog)]
    public static void LogError(object message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [Conditional(ScriptingDefineSymbols.DebugLog)]
    public static void LogWarning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }
}