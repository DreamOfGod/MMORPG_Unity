//===============================================
//作    者：
//创建时间：2022-03-16 15:27:55
//备    注：
//===============================================
using LitJson;
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
        sb.AppendFormat("DeviceIdentifier={0}&", SystemInfo.deviceUniqueIdentifier);
        sb.AppendFormat("Time={0}&", TimeModel.Instance.ServerTime);
        sb.AppendFormat("Sign={0}", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, TimeModel.Instance.ServerTime)));
        return url + sb.ToString();
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
        form.AddField("Time", TimeModel.Instance.ServerTime.ToString());
        form.AddField("Sign", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, TimeModel.Instance.ServerTime)));
    }
    #endregion

    #region 基于任务的异步Get
    /// <summary>
    /// 基于任务的异步Get
    /// </summary>
    /// <typeparam name="RespValType">响应的值的类型</typeparam>
    /// <param name="url">url</param>
    /// <returns>请求结果</returns>
    public async Task<RequestResult<RespValType>> GetTaskAsync<RespValType>(string url)
    {
        url = AddSign(url);
        var request = UnityWebRequest.Get(url);
        int requestID = m_HttpRequestLocalID++;
        DebugLogger.LogFormat("发送GET请求\n\trequest ID:{0}\n\turl:{1}", requestID, url);
        await request.SendWebRequest();
        DebugLogger.LogFormat("GET请求响应\n\trequest ID:{0}\n\turl:{1}\n\tresult:{2}\n\tresponseCode:{3}\n\terror:{4}\n\ttext:{5}", requestID, request.url, request.result, request.responseCode, request.error, request.downloadHandler.text);
        var requestResult = new RequestResult<RespValType>();
        requestResult.Result = request.result;
        if (request.result == UnityWebRequest.Result.Success)
        {
            requestResult.ResponseValue = JsonMapper.ToObject<ResponseValue<RespValType>>(request.downloadHandler.text);
        }
        return requestResult;
    }
    #endregion

    #region 基于任务的异步Post
    /// <summary>
    /// 基于任务的异步Post
    /// </summary>
    /// <typeparam name="RespValType">响应的值类型</typeparam>
    /// <param name="url">url</param>
    /// <param name="form">表单</param>
    /// <returns>请求结果</returns>
    public async Task<RequestResult<RespValType>> PostTaskAsync<RespValType>(string url, WWWForm form = null)
    {
        if(form == null)
        {
            form = new WWWForm();
        }
        AddSign(form);
        var request = UnityWebRequest.Post(url, form);
        int requestID = m_HttpRequestLocalID++;
        DebugLogger.LogFormat("发送POST请求\n\trequest ID:{0}\n\turl:{1}\n\t参数:{2}", requestID, url, form);
        await request.SendWebRequest();
        DebugLogger.LogFormat("POST请求响应\n\trequest ID:{0}\n\turl:{1}\n\tresult:{2}\n\tresponseCode:{3}\n\terror:{4}\n\ttext:{5}", requestID, request.url, request.result, request.responseCode, request.error, request.downloadHandler.text);
        var requestResult = new RequestResult<RespValType>();
        requestResult.Result = request.result;
        if(request.result == UnityWebRequest.Result.Success)
        {
            requestResult.ResponseValue = JsonMapper.ToObject<ResponseValue<RespValType>>(request.downloadHandler.text);
        }
        return requestResult;
    }
    #endregion
}