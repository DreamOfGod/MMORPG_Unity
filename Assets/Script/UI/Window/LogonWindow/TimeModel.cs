//===============================================
//作    者：
//创建时间：2022-04-09 19:00:06
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.Networking;

public class TimeModel: Singleton<TimeModel>
{
    /// <summary>
    /// 服务器初始时间戳，单位ms
    /// </summary>
    private long m_ServerInitialTime;

    /// <summary>
    /// 服务器当前时间戳，单位ms
    /// </summary>
    public long ServerTime
    {
        get
        {
            return m_ServerInitialTime + (long)RealTime.time * 1000;
        }
    }

    public void ReqServerTime(Action<UnityWebRequest.Result> callback = null)
    {
        NetWorkHttp.Instance.Get<long>(NetWorkHttp.AccountServerURL + "time", ReqServerTimeCallback, callback);
    }

    private void ReqServerTimeCallback(UnityWebRequest.Result result, object callbackData, ResponseValue<long> responseValue)
    {
        Action<UnityWebRequest.Result> callback = (Action<UnityWebRequest.Result>)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            m_ServerInitialTime = responseValue.Value - (long)RealTime.time * 1000;
            Debug.LogFormat("服务器初始时间戳：{0}ms", m_ServerInitialTime);
        }
        if (callback != null)
        {
            callback(result);
        }
    }
}
