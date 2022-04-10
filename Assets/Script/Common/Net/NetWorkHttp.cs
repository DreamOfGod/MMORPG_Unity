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

    #region Http请求回调类型
    public delegate void HttpCallback(UnityWebRequest.Result result, string text);
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

    #region 在URL中添加签名参数
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
    #endregion

    #region Get请求
    public void Get(string url, HttpCallback callback = null)
    {
        url = AddSign(url);
        UnityWebRequest req = UnityWebRequest.Get(url);
        DebugLogger.LogFormat("发送GET请求\n\turl:{0}", req.url);
        req.SendWebRequest().completed += (AsyncOperation asyncOperation) =>
        {
            DebugLogger.LogFormat("GET请求响应\n\turl:{0}\n\tresult:{1}\n\tresponseCode:{2}\n\terror:{3}\n\ttext:{4}", req.url, req.result, req.responseCode, req.error, req.downloadHandler.text);
            if(callback != null)
            {
                callback(req.result, req.downloadHandler.text);
            }
        };
    }
    #endregion

    #region Post请求
    public void Post(string url, Dictionary<string, object> dic = null, HttpCallback callback = null)
    {
        if(dic == null)
        {
            dic = new Dictionary<string, object>();
        }
        dic.Add("DeviceIdentifier", SystemInfo.deviceUniqueIdentifier);
        dic.Add("Time", ServerTimeUtil.Instance.ServerTime);
        dic.Add("Sign", MD5Hex(string.Format("{0}:{1}", SystemInfo.deviceUniqueIdentifier, ServerTimeUtil.Instance.ServerTime)));

        string dicJson = JsonMapper.ToJson(dic);
        WWWForm form = new WWWForm();
        form.AddField("", dicJson);
        UnityWebRequest req = UnityWebRequest.Post(url, form);
        DebugLogger.LogFormat("发送POST请求\n\turl:{0}\n\t参数:{1}", req.url, dicJson);
        req.SendWebRequest().completed += (AsyncOperation asyncOperation) =>
        {
            DebugLogger.LogFormat("POST请求响应\n\turl:{0}\n\tresult:{1}\n\tresponseCode:{2}\n\terror:{3}\n\ttext:{4}", req.url, req.result, req.responseCode, req.error, req.downloadHandler.text);
            if (callback != null)
            {
                callback(req.result, req.downloadHandler.text);
            }
        };
    }
    #endregion
}