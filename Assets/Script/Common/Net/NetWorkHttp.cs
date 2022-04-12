//===============================================
//作    者：
//创建时间：2022-03-16 15:27:55
//备    注：
//===============================================
using LitJson;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Http通讯管理
/// </summary>
public class NetWorkHttp : Singleton<NetWorkHttp>
{
    /// <summary>
    /// 账号服务器URL
    /// </summary>
    public const string AccountServerURL = "http://127.0.0.1:8080/";

    #region Http请求的本地编号
    private int m_HttpRequestLocalID = 0;
    #endregion

    #region Http请求回调类型
    public delegate void HttpCallback<RespValType>(UnityWebRequest.Result result, object callbackData, ResponseValue<RespValType> responseValue);
    #endregion

    #region 请求异步回调需要的数据
    private struct RequestCallbackRequiredData<RespValType>
    {
        /// <summary>
        /// 请求的本地ID
        /// </summary>
        public int LocalID;
        /// <summary>
        /// 请求的回调
        /// </summary>
        public HttpCallback<RespValType> Callback;
        /// <summary>
        /// 执行请求的回调时原样传递的数据
        /// </summary>
        public object CallbackData;

        public RequestCallbackRequiredData(int id, HttpCallback<RespValType> callback, object callbackData)
        {
            LocalID = id; Callback = callback; CallbackData = callbackData;
        }
    }
    #endregion

    #region 请求对象的字典
    private Dictionary<UnityWebRequest, object> m_RequestDic = new Dictionary<UnityWebRequest, object>();
    #endregion

    #region 计算字符串MD5，并转成十六进制字符串
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

    #region Get请求
    /// <summary>
    /// 在URL中添加签名参数
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
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

    public void Get<RespValType>(string url, HttpCallback<RespValType> callback = null, object callbackData = null)
    {
        url = AddSign(url);
        UnityWebRequest request = UnityWebRequest.Get(url);
        DebugLogger.LogFormat("发送GET请求\n\trequest ID:{0}\n\turl:{1}", m_HttpRequestLocalID, url);
        m_RequestDic.Add(request, new RequestCallbackRequiredData<RespValType>(m_HttpRequestLocalID++, callback, callbackData));
        request.SendWebRequest().completed += GetCallback<RespValType>;
    }

    private void GetCallback<RespValType>(AsyncOperation ao)
    {
        UnityWebRequestAsyncOperation requestAO = (UnityWebRequestAsyncOperation)ao;
        UnityWebRequest request = requestAO.webRequest;
        RequestCallbackRequiredData<RespValType> requestData = (RequestCallbackRequiredData<RespValType>)m_RequestDic[request];
        m_RequestDic.Remove(request);
        DebugLogger.LogFormat("GET请求响应\n\trequest ID:{0}\n\turl:{1}\n\tresult:{2}\n\tresponseCode:{3}\n\terror:{4}\n\ttext:{5}", requestData.LocalID, request.url, request.result, request.responseCode, request.error, request.downloadHandler.text);
        if (requestData.Callback != null)
        {
            ResponseValue<RespValType> responseValue = JsonMapper.ToObject<ResponseValue<RespValType>>(request.downloadHandler.text);
            requestData.Callback(request.result, requestData.CallbackData, responseValue);
        }
    }
    #endregion

    #region Post请求
    /// <summary>
    /// 在字典中添加签名参数
    /// </summary>
    /// <param name="dic"></param>
    private void AddSign(Dictionary<string, object> dic)
    {
        dic.Add("DeviceIdentifier", SystemInfo.deviceUniqueIdentifier);
        dic.Add("Time", TimeModel.Instance.ServerTime);
        dic.Add("Sign", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, TimeModel.Instance.ServerTime)));
    }

    public void Post<RespValType>(string url, WWWForm form = null, HttpCallback<RespValType> callback = null, object callbackData = null)
    {
        if(form == null)
        {
            form = new WWWForm();
        }
        form.AddField("DeviceIdentifier", SystemInfo.deviceUniqueIdentifier);
        form.AddField("Time", TimeModel.Instance.ServerTime.ToString());
        form.AddField("Sign", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, TimeModel.Instance.ServerTime)));
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        DebugLogger.LogFormat("发送POST请求\n\trequest ID:{0}\n\turl:{1}\n\t参数:{2}", m_HttpRequestLocalID, url, form);
        m_RequestDic.Add(request, new RequestCallbackRequiredData<RespValType>(m_HttpRequestLocalID++, callback, callbackData));
        request.SendWebRequest().completed += PostCallback<RespValType>;
    }

    private void PostCallback<RespValType>(AsyncOperation ao)
    {
        UnityWebRequestAsyncOperation requestAO = (UnityWebRequestAsyncOperation)ao;
        UnityWebRequest request = requestAO.webRequest;
        RequestCallbackRequiredData<RespValType> requestData = (RequestCallbackRequiredData<RespValType>)m_RequestDic[request];
        m_RequestDic.Remove(request);
        DebugLogger.LogFormat("POST请求响应\n\trequest ID:{0}\n\turl:{1}\n\tresult:{2}\n\tresponseCode:{3}\n\terror:{4}\n\ttext:{5}", requestData.LocalID, request.url, request.result, request.responseCode, request.error, request.downloadHandler.text);
        if (requestData.Callback != null)
        {
            ResponseValue<RespValType> responseValue = JsonMapper.ToObject<ResponseValue<RespValType>>(request.downloadHandler.text);
            requestData.Callback(request.result, requestData.CallbackData, responseValue);
        }
    }
    #endregion
}