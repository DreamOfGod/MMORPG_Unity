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
    public delegate void WithPageIndexCallback(UnityWebRequest.Result result, int pageIndex, MFReturnValue<List<RetGameServerEntity>> ret = null);

    private List<RetGameServerPageEntity> m_RetGameServerPageList;
    private List<RetGameServerEntity> m_RetGameServerList;

    #region 请求游戏服页签
    public void ReqGameServerPage(ModelCallback<List<RetGameServerPageEntity>> callback = null)
    {
        NetWorkHttp.Instance.Get(NetWorkHttp.AccountServerURL + "api/GameServer", ReqGameServerPageCallback, callback);
    }

    private void ReqGameServerPageCallback(UnityWebRequest.Result result, object callbackData, string text)
    {
        ModelCallback<List<RetGameServerPageEntity>> callback = (ModelCallback<List<RetGameServerPageEntity>>)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            MFReturnValue<List<RetGameServerPageEntity>> ret = JsonMapper.ToObject<MFReturnValue<List<RetGameServerPageEntity>>>(text);
            if (!ret.HasError)
            {
                m_RetGameServerPageList = ret.Value;
            }
            if (callback != null)
            {
                callback(result, ret);
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
        NetWorkHttp.Instance.Get(NetWorkHttp.AccountServerURL + "api/GameServer?pageIndex=" + pageIndex, ReqGameServerCallback, new ReqGameServerCallbackData() { PageIndex = pageIndex, Callback = callback});
    }

    private void ReqGameServerCallback(UnityWebRequest.Result result, object callbackData, string text)
    {
        ReqGameServerCallbackData reqGameServerCallbackData = (ReqGameServerCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            MFReturnValue<List<RetGameServerEntity>> ret = JsonMapper.ToObject<MFReturnValue<List<RetGameServerEntity>>>(text);
            if (!ret.HasError)
            {
                m_RetGameServerList = ret.Value;
            }
            if (reqGameServerCallbackData.Callback != null)
            {
                reqGameServerCallbackData.Callback(result, reqGameServerCallbackData.PageIndex, ret);
            }
        }
        else if (reqGameServerCallbackData.Callback != null)
        {
            reqGameServerCallbackData.Callback(result, reqGameServerCallbackData.PageIndex);
        }
    }
    #endregion
}