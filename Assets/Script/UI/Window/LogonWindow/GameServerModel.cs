//===============================================
//作    者：
//创建时间：2022-04-11 14:56:09
//备    注：
//===============================================
using LitJson;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameServerModel: Singleton<GameServerModel>
{
    public delegate void WithPageIndexCallback(UnityWebRequest.Result result, int pageIndex, ResponseValue<List<RetGameServerEntity>> ret = null);

    private List<RetGameServerPageEntity> m_RetGameServerPageList;
    private List<RetGameServerEntity> m_RetGameServerList;

    #region 请求游戏服页签
    public void ReqGameServerPage(ModelCallback<List<RetGameServerPageEntity>> callback = null)
    {
        NetWorkHttp.Instance.Get<List<RetGameServerPageEntity>>(NetWorkHttp.AccountServerURL + "game_server", ReqGameServerPageCallback, callback);
    }

    private void ReqGameServerPageCallback(UnityWebRequest.Result result, object callbackData, ResponseValue<List<RetGameServerPageEntity>> responseValue)
    {
        ModelCallback<List<RetGameServerPageEntity>> callback = (ModelCallback<List<RetGameServerPageEntity>>)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            if (responseValue.Code == 0)
            {
                m_RetGameServerPageList = responseValue.Value;
            }
            if (callback != null)
            {
                callback(result, responseValue);
            }
        }
        else if (callback != null)
        {
            callback(result);
        }
    }
    #endregion

    #region 请求游戏服信息
    private struct ReqGameServerCallbackData
    {
        public int PageIndex;
        public WithPageIndexCallback Callback;
    }

    public void ReqGameServer(int pageIndex, WithPageIndexCallback callback = null)
    {
        NetWorkHttp.Instance.Get<List<RetGameServerEntity>>(NetWorkHttp.AccountServerURL + "game_server?pageIndex=" + pageIndex, ReqGameServerCallback, new ReqGameServerCallbackData() { PageIndex = pageIndex, Callback = callback});
    }

    private void ReqGameServerCallback(UnityWebRequest.Result result, object callbackData, ResponseValue<List<RetGameServerEntity>> responseValue)
    {
        ReqGameServerCallbackData reqGameServerCallbackData = (ReqGameServerCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            if (responseValue.Code == 0)
            {
                m_RetGameServerList = responseValue.Value;
            }
            if (reqGameServerCallbackData.Callback != null)
            {
                reqGameServerCallbackData.Callback(result, reqGameServerCallbackData.PageIndex, responseValue);
            }
        }
        else if (reqGameServerCallbackData.Callback != null)
        {
            reqGameServerCallbackData.Callback(result, reqGameServerCallbackData.PageIndex);
        }
    }
    #endregion
}