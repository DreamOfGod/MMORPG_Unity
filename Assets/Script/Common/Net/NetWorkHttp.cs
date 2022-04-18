//===============================================
//作    者：
//创建时间：2022-03-16 15:27:55
//备    注：
//===============================================
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
 
/// <summary>
/// Http通讯管理
/// </summary>
public class NetWorkHttp : Singleton<NetWorkHttp>
{
    #region 账号服务器URL
    /// <summary>
    /// 账号服务器URL
    /// </summary>
    public const string AccountServerURL = "http://127.0.0.1:8080/";
    #endregion

    #region Http请求的本地编号
    private int m_HttpRequestLocalID = 0;
    #endregion

    #region 计算字符串MD5，并转成十六进制字符串
    /// <summary>
    /// 计算字符串MD5，并转成十六进制字符串
    /// </summary>
    /// <param name="str">原始字符串</param>
    /// <returns>十六进制MD5字符串</returns>
    private string MD5Hex(string str)
    {
        byte[] bytes = Encoding.Unicode.GetBytes(str);
        bytes = MD5.Create().ComputeHash(bytes);
        StringBuilder hexSb = new StringBuilder();
        foreach (byte b in bytes)
        {
            hexSb.AppendFormat("{0: X2}", b);
        }
        return hexSb.ToString();
    }
    #endregion

    #region 在URL中添加签名参数
    /// <summary>
    /// 在URL中添加签名参数
    /// </summary>
    /// <param name="url">url</param>
    /// <returns>新的url字符串</returns>
    private string AddSign(string url)
    {
        int idx = url.IndexOf('?');
        if (idx < 0)
        {
            url += '?';

        }
        else if (idx < url.Length - 1 && url[url.Length - 1] != '&')
        {
            url += '&';
        }
        StringBuilder sb = new StringBuilder();
        sb.Append($"DeviceIdentifier={ SystemInfo.deviceUniqueIdentifier }&");
        sb.Append($"Time={ TimeModel.Instance.ServerTimeMillionsecond }&");
        string md5HexStr = MD5Hex($"{ SystemInfo.deviceUniqueIdentifier }:{ TimeModel.Instance.ServerTimeMillionsecond }");
        sb.Append($"Sign={ md5HexStr }");
        return $"{ url }{ sb }";
    }
    #endregion

    #region 在字典中添加签名参数
    /// <summary>
    /// 在字典中添加签名参数
    /// </summary>
    /// <param name="dic">字典</param>
    private void AddSign(WWWForm form)
    {
        form.AddField("DeviceIdentifier", SystemInfo.deviceUniqueIdentifier);
        form.AddField("Time", TimeModel.Instance.ServerTimeMillionsecond.ToString());
        form.AddField("Sign", MD5Hex($"{ SystemInfo.deviceUniqueIdentifier }:{ TimeModel.Instance.ServerTimeMillionsecond }"));
    }
    #endregion

    #region 基于任务的异步Get
    /// <summary>
    /// 基于任务的异步Get
    /// 默认情况下，异步方法将在主unity线程上运行，因为Unity提供了一个默认的SynchronizationContext，称为UnitySynchronizationContext，它自动收集在每个帧中排队的所有异步代码，并继续在主要unity线程上运行它们
    /// </summary>
    /// <typeparam name="RespDataType">响应的数据的类型</typeparam>
    /// <param name="url">url</param>
    /// <returns>请求结果</returns>
    public async Task<RequestResult<RespDataType>> GetAsync<RespDataType>(string url)
    {
        url = AddSign(url);
        var request = UnityWebRequest.Get(url);
        int requestID = m_HttpRequestLocalID++;

        string logString =
$@"发送GET请求
    request ID:{ requestID }
    url:{ url }
";
        DebugLogger.Log(logString);

        await request.SendWebRequest();

        logString =
$@"GET请求响应
    request ID:{ requestID }
    url:{ request.url }
    result:{ request.result }
    responseCode:{ request.responseCode }
    error:{ request.error }
    text:{ request.downloadHandler.text }
";

        DebugLogger.Log(logString);

        var requestResult = new RequestResult<RespDataType>();
        requestResult.Result = request.result;
        if (request.result == UnityWebRequest.Result.Success)
        {
            requestResult.ResponseData = JsonConvert.DeserializeObject<ResponseData<RespDataType>>(request.downloadHandler.text);
        }
        return requestResult;
    }
    #endregion

    #region 基于任务的异步Post
    /// <summary>
    /// 基于任务的异步Post
    /// </summary>
    /// <typeparam name="RespDataType">响应的数据的类型</typeparam>
    /// <param name="url">url</param>
    /// <param name="form">表单</param>
    /// <returns>请求结果</returns>
    public async Task<RequestResult<RespDataType>> PostAsync<RespDataType>(string url, WWWForm form = null)
    {
        if (form == null)
        {
            form = new WWWForm();
        }
        AddSign(form);
        var request = UnityWebRequest.Post(url, form);
        int requestID = m_HttpRequestLocalID++;
        string logString =
$@"发送POST请求
    request ID:{ requestID }
    url:{ url }
    参数:{ form }
";
        DebugLogger.Log(logString);

        await request.SendWebRequest();

        logString =
$@"POST请求响应
    request ID:{ requestID }
    url:{ request.url }
    result:{ request.result }
    responseCode:{ request.responseCode }
    error:{ request.error }
    text:{ request.downloadHandler.text }
";
        DebugLogger.Log(logString);

        var requestResult = new RequestResult<RespDataType>();
        requestResult.Result = request.result;
        if(request.result == UnityWebRequest.Result.Success)
        {
            requestResult.ResponseData = JsonConvert.DeserializeObject<ResponseData<RespDataType>>(request.downloadHandler.text);
        }
        return requestResult;
    }
    #endregion
}