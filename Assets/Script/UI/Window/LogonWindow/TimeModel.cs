//===============================================
//作    者：
//创建时间：2022-04-09 19:00:06
//备    注：
//===============================================
using System.Threading.Tasks;

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

    public async Task<RequestResult<long>> ReqServerTime()
    {
        var requestResult = await NetWorkHttp.Instance.GetTaskAsync<long>(NetWorkHttp.AccountServerURL + "time");
        if(requestResult.IsSuccess)
        {
            m_ServerInitialTime = requestResult.ResponseValue.Value - (long)RealTime.time * 1000;
            DebugLogger.LogFormat("服务器初始时间戳：{0}ms", m_ServerInitialTime);
        }
        return requestResult;
    }
}
