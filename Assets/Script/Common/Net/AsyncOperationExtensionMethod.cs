//===============================================
//作    者：
//创建时间：2022-04-13 14:26:39
//备    注：
//===============================================
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// AsyncOperation扩展方法
/// </summary>
public static class AsyncOperationExtensionMethod
{
    public static TaskAwaiter<object> GetAwaiter(this AsyncOperation ao)
    {
        var tcs = new TaskCompletionSource<object>();
        ao.completed += (obj) => { tcs.SetResult(null); };
        return tcs.Task.GetAwaiter();
    }
}
