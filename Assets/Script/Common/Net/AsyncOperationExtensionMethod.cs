//===============================================
//作    者：
//创建时间：2022-04-13 14:26:39
//备    注：
//===============================================
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// AsyncOperation扩展方法
/// </summary>
public static class AsyncOperationExtensionMethod
{
    /// <summary>
    /// GetAwaiter，支持对基于事件的异步方法返回的AsyncOperation使用await等待
    /// 只能在AsyncOperation表示的异步操作完成之前await才有用，否则由于AsyncOperation的completed事件已经发生，TaskAwaiter的任务无法完成，会导致永远无法解除等待
    /// </summary>
    /// <param name="ao"></param>
    /// <returns></returns>
    public static TaskAwaiter<object> GetAwaiter(this AsyncOperation ao)
    {
        var tcs = new TaskCompletionSource<object>();
        ao.completed += (obj) => { tcs.SetResult(null); };
        return tcs.Task.GetAwaiter();
    }
}
