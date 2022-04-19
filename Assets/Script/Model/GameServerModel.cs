//===============================================
//作    者：
//创建时间：2022-04-11 14:56:09
//备    注：
//===============================================
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameServerModel
{
    #region 单例
    private GameServerModel() { }
    private static GameServerModel m_Instance;
    public static GameServerModel Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameServerModel();
            }
            return m_Instance;
        }
    }
    #endregion

    private List<GameServerGroupBean> m_GameServerGroupList;

    #region 请求区服页签
    public async Task<RequestResult<List<GameServerGroupBean>>> ReqGameServerGroupAsync()
    {
        var requestResult = await HttpHelper.Instance.GetAsync<List<GameServerGroupBean>>($"{ HttpHelper.AccountServerURL }game_server_group");
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            m_GameServerGroupList = requestResult.ResponseData.Data;
        }
        return requestResult;
    }
    #endregion

    #region 请求区服信息
    public async Task<RequestResult<List<GameServerBean>>> ReqGameServerListAsync(int firstId, int lastId)
    {
        string url = $"{ HttpHelper.AccountServerURL }game_server_list?firstId={ firstId }&lastId={ lastId }";
        return await HttpHelper.Instance.GetAsync<List<GameServerBean>>(url);
    }

    public async Task<RequestResult<List<GameServerBean>>> ReqRecommendGameServerListAsync()
    {
        string url = $"{ HttpHelper.AccountServerURL }recommend_game_server_list";
        return await HttpHelper.Instance.GetAsync<List<GameServerBean>>(url);
    }
    #endregion

    #region 请求进入区服
    public async Task<RequestResult<object>> ReqEnterGameServer(GameServerBean gameServer)
    {
        string url = $"{ HttpHelper.AccountServerURL }enter_game_server?gameServerId={ gameServer.Id }";
        var requestResult = await HttpHelper.Instance.GetAsync<object>(url);
        if(requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            AccountModel.Instance.LastLogonServer = gameServer;
        }
        return requestResult;
    }
    #endregion
}