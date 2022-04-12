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
    public delegate void HttpCallback(UnityWebRequest.Result result, object callbackData, string text);
    #endregion

    #region 请求异步回调需要的数据
    private struct RequestCallbackRequiredData
    {
        /// <summary>
        /// 请求的本地ID
        /// </summary>
        public int LocalID;
        /// <summary>
        /// 请求的回调
        /// </summary>
        public HttpCallback Callback;
        /// <summary>
        /// 执行请求的回调时原样传递的数据
        /// </summary>
        public object CallbackData;

        public RequestCallbackRequiredData(int id, HttpCallback callback, object callbackData)
        {
            LocalID = id; Callback = callback; CallbackData = callbackData;
        }
    }
    #endregion

    #region 请求对象的字典
    private Dictionary<UnityWebRequest, RequestCallbackRequiredData> m_RequestDic = new Dictionary<UnityWebRequest, RequestCallbackRequiredData>();
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
        sb.AppendFormat("Time={0}&", ServerTimeUtil.Instance.ServerTime);
        sb.AppendFormat("Sign={0}", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, ServerTimeUtil.Instance.ServerTime)));
        return url + sb.ToString();
    }

    public void Get(string url, HttpCallback callback = null, object callbackData = null)
    {
        url = AddSign(url);
        UnityWebRequest request = UnityWebRequest.Get(url);
        DebugLogger.LogFormat("发送GET请求\n\trequest ID:{0}\n\turl:{1}", m_HttpRequestLocalID, url);
        m_RequestDic.Add(request, new RequestCallbackRequiredData(m_HttpRequestLocalID++, callback, callbackData));
        request.SendWebRequest().completed += GetCallback;
    }

    private void GetCallback(AsyncOperation ao)
    {
        UnityWebRequestAsyncOperation requestAO = (UnityWebRequestAsyncOperation)ao;
        UnityWebRequest request = requestAO.webRequest;
        RequestCallbackRequiredData requestData = m_RequestDic[request];
        m_RequestDic.Remove(request);
        DebugLogger.LogFormat("GET请求响应\n\trequest ID:{0}\n\turl:{1}\n\tresult:{2}\n\tresponseCode:{3}\n\terror:{4}\n\ttext:{5}", requestData.LocalID, request.url, request.result, request.responseCode, request.error, request.downloadHandler.text);
        if (requestData.Callback != null)
        {
            requestData.Callback(request.result, requestData.CallbackData, request.downloadHandler.text);
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
        dic.Add("Time", ServerTimeUtil.Instance.ServerTime);
        dic.Add("Sign", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, ServerTimeUtil.Instance.ServerTime)));
    }

    public void Post(string url, WWWForm form = null, HttpCallback callback = null, object callbackData = null)
    {
        //if(dic == null)
        //{
        //    dic = new Dictionary<string, object>();
        //}
        //AddSign(dic);

        //string dicJson = JsonMapper.ToJson(dic);
        //WWWForm form = new WWWForm();
        ////form.AddField("", dicJson);
        //foreach(var pair in dic)
        //{
        //    form.AddField(pair.Key, pair.Value);
        //}
        if(form == null)
        {
            form = new WWWForm();
        }
        form.AddField("DeviceIdentifier", SystemInfo.deviceUniqueIdentifier);
        form.AddField("Time", ServerTimeUtil.Instance.ServerTime.ToString());
        form.AddField("Sign", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, ServerTimeUtil.Instance.ServerTime)));
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        DebugLogger.LogFormat("发送POST请求\n\trequest ID:{0}\n\turl:{1}\n\t参数:{2}", m_HttpRequestLocalID, url, form);
        m_RequestDic.Add(request, new RequestCallbackRequiredData(m_HttpRequestLocalID++, callback, callbackData));
        request.SendWebRequest().completed += PostCallback;
    }

    private void PostCallback(AsyncOperation ao)
    {
        UnityWebRequestAsyncOperation requestAO = (UnityWebRequestAsyncOperation)ao;
        UnityWebRequest request = requestAO.webRequest;
        RequestCallbackRequiredData requestData = m_RequestDic[request];
        m_RequestDic.Remove(request);
        DebugLogger.LogFormat("POST请求响应\n\trequest ID:{0}\n\turl:{1}\n\tresult:{2}\n\tresponseCode:{3}\n\terror:{4}\n\ttext:{5}", requestData.LocalID, request.url, request.result, request.responseCode, request.error, request.downloadHandler.text);
        if (requestData.Callback != null)
        {
            requestData.Callback(request.result, requestData.CallbackData, request.downloadHandler.text);
        }
    }
    #endregion
}