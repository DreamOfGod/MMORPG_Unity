//===============================================
//作    者：
//创建时间：2022-04-09 19:00:06
//备    注：
//===============================================
using System;
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
    public long ServerTimeMillionsecond
    {
        get
        {
            return m_ServerInitialTime + (long)RealTime.time * 1000;
        }
    }

    /// <summary>
    /// 服务器当前时间戳，单位s
    /// </summary>
    public long ServerTimeSecond
    {
        get
        {
            return m_ServerInitialTime / 1000 + (long)RealTime.time;
        }
    }

    public async Task<RequestResult<long>> ReqServerTime()
    {
        var requestResult = await NetWorkHttp.Instance.GetAsync<long>($"{ NetWorkHttp.AccountServerURL }time");
        if(requestResult.IsSuccess)
        {
            m_ServerInitialTime = requestResult.ResponseData.Data - (long)RealTime.time * 1000;
            var timestampOrigin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();//时间戳起点
            var oneYear = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Local);//公元1年
            var ticks = (timestampOrigin - oneYear).Ticks + m_ServerInitialTime * 10000;//根据服务器时间戳计算出的当前时刻的计时周期
            DebugLogger.Log($"服务器初始时间戳：{ m_ServerInitialTime }ms（{ new DateTime(ticks) }）");
        }
        return requestResult;
    }
}
