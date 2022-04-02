//===============================================
//作    者：
//创建时间：2022-03-16 15:27:55
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Http通讯管理
/// </summary>
public class NetWorkHttp : Singleton<NetWorkHttp>
{
    #region Http请求回调类型
    /// <summary>
    /// Http请求回调类型
    /// </summary>
    /// <param name="error"></param>
    /// <param name="data"></param>
    public delegate void HttpCallback(UnityWebRequest.Result result, string text);
    #endregion

    #region Get请求
    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="url"></param>
    public void Get(string url, HttpCallback callback)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);
        req.SendWebRequest().completed += (AsyncOperation asyncOperation) =>
        {
            Debug.Log(string.Format("GET请求响应\n\turl:{0}\n\tresult:{1}\n\tresponseCode:{2}\n\terror:{3}\n\ttext:{4}", req.url, req.result, req.responseCode, req.error, req.downloadHandler.text));
            callback(req.result, req.downloadHandler.text);
        };
    }
    #endregion

    #region Post请求
    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    public void Post(string url, string json, HttpCallback callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("", json);
        UnityWebRequest req = UnityWebRequest.Post(url, form);
        req.SendWebRequest().completed += (AsyncOperation asyncOperation) =>
        {
            Debug.Log(string.Format("POST请求响应\n\turl:{0}\n\tresult:{1}\n\tresponseCode:{2}\n\terror:{3}\n\ttext:{4}", req.url, req.result, req.responseCode, req.error, req.downloadHandler.text));
            callback(req.result, req.downloadHandler.text);
        };
    }
    #endregion
}