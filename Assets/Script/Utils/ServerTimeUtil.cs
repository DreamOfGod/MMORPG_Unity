//===============================================
//作    者：
//创建时间：2022-04-09 19:00:06
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTimeUtil: Singleton<ServerTimeUtil>
{
    private long m_ServerInitialTime;

    public long ServerTime
    {
        get
        {
            return m_ServerInitialTime + (long)RealTime.time;
        }
    }

    public void ReqServerTime(Action<UnityWebRequest.Result> callback = null)
    {
        NetWorkHttp.Instance.Get(NetWorkHttp.AccountServerURL + "api/time", ReqServerTimeCallback, callback);
    }

    private void ReqServerTimeCallback(UnityWebRequest.Result result, object callbackData, string text)
    {
        Action<UnityWebRequest.Result> callback = (Action<UnityWebRequest.Result>)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            m_ServerInitialTime = long.Parse(text) - (long)RealTime.time;
            Debug.LogFormat("服务器初始时间戳：{0}", m_ServerInitialTime);
        }
        if (callback != null)
        {
            callback(result);
        }
    }
}
