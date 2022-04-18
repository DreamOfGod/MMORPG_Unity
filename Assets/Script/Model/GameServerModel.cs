//===============================================
//作    者：
//创建时间：2022-04-11 14:56:09
//备    注：
//===============================================
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameServerModel: Singleton<GameServerModel>
{
    private List<GameServerGroupBean> m_GameServerGroupList;

    #region 请求游戏服页签
    public async Task<RequestResult<List<GameServerGroupBean>>> ReqGameServerGroupAsync()
    {
        var requestResult = await NetWorkHttp.Instance.GetAsync<List<GameServerGroupBean>>($"{ NetWorkHttp.AccountServerURL }game_server_group");
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            m_GameServerGroupList = requestResult.ResponseData.Data;
        }
        return requestResult;
    }
    #endregion

    #region 请求游戏服信息
    public async Task<RequestResult<List<GameServerBean>>> ReqGameServerListAsync(int firstId, int lastId)
    {
        string url = $"{ NetWorkHttp.AccountServerURL }game_server_list?firstId={ firstId }&lastId={ lastId }";
        return await NetWorkHttp.Instance.GetAsync<List<GameServerBean>>(url);
    }

    public async Task<RequestResult<List<GameServerBean>>> ReqRecommendGameServerListAsync()
    {
        string url = $"{ NetWorkHttp.AccountServerURL }recommend_game_server_list";
        return await NetWorkHttp.Instance.GetAsync<List<GameServerBean>>(url);
    }
    #endregion

    public async Task<RequestResult<object>> ReqEnterGameServer(GameServerBean gameServer)
    {
        string url = $"{ NetWorkHttp.AccountServerURL }enter_game_server?gameServerId={ gameServer.Id }";
        var requestResult = await NetWorkHttp.Instance.GetAsync<object>(url);
        if(requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            AccountModel.Instance.LastLogonServer = gameServer;
        }
        return requestResult;
    }
}