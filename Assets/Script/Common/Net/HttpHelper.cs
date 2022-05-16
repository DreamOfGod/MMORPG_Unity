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
/// Http通讯帮助类
/// </summary>
public class HttpHelper
{
    #region 账号服务器URL
    /// <summary>
    /// 账号服务器URL
    /// </summary>
    public const string AccountServerURL = "http://192.168.1.4:1000/";
    #endregion

    #region 单例
    private HttpHelper() { }
    public static readonly HttpHelper Instance = new HttpHelper();
    #endregion

    #region 请求的本地序号
    private int m_ReqLocalSeqNum = 0;
    #endregion

    #region 计算字符串MD5，并转成十六进制字符串
    //计算字符串MD5，并转成十六进制字符串
    private string MD5Hex(string str)
    {
        var bytes = Encoding.Unicode.GetBytes(str);
        bytes = MD5.Create().ComputeHash(bytes);
        var hexSb = new StringBuilder();
        for (var i = 0; i < bytes.Length; ++i)
        {
            hexSb.AppendFormat("{0: X2}", bytes[i]);
        }
        return hexSb.ToString();
    }
    #endregion

    #region 在URL中添加签名参数
    //在URL中添加签名参数
    private string AddSign(string url)
    {
        var sb = new StringBuilder();
        sb.Append(url);
        var idx = url.IndexOf('?');
        if (idx < 0)
        {
            sb.Append('?');
        }
        else if (idx < url.Length - 1 && url[url.Length - 1] != '&')
        {
            sb.Append('&');
        }
        
        sb.Append($"DeviceIdentifier={ SystemInfo.deviceUniqueIdentifier }&");
        sb.Append($"Time={ TimeModel.Instance.ServerTimeMillionsecond }&");
        var md5HexStr = MD5Hex($"{ SystemInfo.deviceUniqueIdentifier }:{ TimeModel.Instance.ServerTimeMillionsecond }");
        sb.Append($"Sign={ md5HexStr }");
        return sb.ToString();
    }
    #endregion

    #region 在表单中添加签名参数
    //在表单中添加签名参数
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
    /// </summary>
    /// <typeparam name="RespDataType">响应的数据的类型</typeparam>
    /// <param name="url">url</param>
    /// <returns>请求结果</returns>
    public async Task<RequestResult<RespDataType>> GetAsync<RespDataType>(string url)
    {
        url = AddSign(url);
        var request = UnityWebRequest.Get(url);
        var reqSeqNum = m_ReqLocalSeqNum++;

        var logStr =
$@"发送GET请求
    req seq num:{ reqSeqNum }
    url:{ url }
";
        DebugLogger.Log(logStr);

        await request.SendWebRequest();

        logStr =
$@"GET请求响应
    req seq num:{ reqSeqNum }
    url:{ request.url }
    result:{ request.result }
    responseCode:{ request.responseCode }
    error:{ request.error }
    text:{ request.downloadHandler.text }
";

        DebugLogger.Log(logStr);

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
        var reqSeqNum = m_ReqLocalSeqNum++;
        var logStr =
$@"发送POST请求
    req seq num:{ reqSeqNum }
    url:{ url }
    参数:{ form }
";
        DebugLogger.Log(logStr);

        await request.SendWebRequest();

        logStr =
$@"POST请求响应
    req seq num:{ reqSeqNum }
    url:{ request.url }
    result:{ request.result }
    responseCode:{ request.responseCode }
    error:{ request.error }
    text:{ request.downloadHandler.text }
";
        DebugLogger.Log(logStr);

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