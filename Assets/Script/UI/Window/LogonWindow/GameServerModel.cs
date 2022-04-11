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
    private List<RetGameServerPageEntity> m_RetGameServerPageList;
    private List<RetGameServerEntity> m_RetGameServerList;

    public void ReqGameServerPage(ModelCallback<List<RetGameServerPageEntity>> callback = null)
    {
        NetWorkHttp.Instance.Get(NetWorkHttp.AccountServerURL + "api/GameServer", (UnityWebRequest.Result result, string text) => {
            if(result == UnityWebRequest.Result.Success)
            {
                MFReturnValue<List<RetGameServerPageEntity>> ret = JsonMapper.ToObject<MFReturnValue<List<RetGameServerPageEntity>>>(text);
                if(!ret.HasError)
                {
                    m_RetGameServerPageList = ret.Value;
                }
                if(callback != null)
                {
                    callback(result, ret);
                }
            }
            else if(callback != null)
            {
                callback(result);
            }
        });
    }

    public void ReqGameServer(int pageIndex, ModelCallback<List<RetGameServerEntity>> callback = null)
    {
        NetWorkHttp.Instance.Get(NetWorkHttp.AccountServerURL + "api/GameServer?pageIndex=" + pageIndex, (UnityWebRequest.Result result, string text) => {
            if (result == UnityWebRequest.Result.Success)
            {
                MFReturnValue<List<RetGameServerEntity>> ret = JsonMapper.ToObject<MFReturnValue<List<RetGameServerEntity>>>(text);
                if (!ret.HasError)
                {
                    m_RetGameServerList = ret.Value;
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
        });
    }
}