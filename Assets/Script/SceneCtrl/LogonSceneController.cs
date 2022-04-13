//===============================================
//作    者：
//创建时间：2022-02-23 16:44:48
//备    注：
//===============================================
using System.Threading;
using UnityEngine;

public class LogonSceneController : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.O))
        {
            TimeModel.Instance.ReqServerTime();
            DebugLogger.LogFormat("当前线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}