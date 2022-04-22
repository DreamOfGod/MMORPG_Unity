//===============================================
//作    者：
//创建时间：2022-04-11 14:56:09
//备    注：
//===============================================
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameServerModel: IModel
{
    #region 单例
    private GameServerModel() { }
    private static GameServerModel instance = new GameServerModel();
    public static GameServerModel Instance { get => instance; }
    #endregion

    private List<GameServerGroupBean> m_GameServerGroupList;

    public event Action<List<RoleOperation_LogOnGameServerReturnProto.RoleItem>> LogonGameServerReturn;

    public void Init()
    {
        SocketHelper.Instance.AddListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogonGameServerReturn);
    }

    public void Reset()
    {
        SocketHelper.Instance.RemoveListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogonGameServerReturn);
    }

    private void OnLogonGameServerReturn(byte[] buffer)
    {
        var protocol = RoleOperation_LogOnGameServerReturnProto.GetProto(buffer);
        LogonGameServerReturn?.Invoke(protocol.RoleList);
    }

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

    #region 请求登陆区服
    public void ReqLogonGameServer()
    {
        var protocol = new RoleOperation_LogOnGameServerProto();
        protocol.AccountId = AccountModel.Instance.AccountID;
        SocketHelper.Instance.SendMsg(protocol.ToArray());
    }
    #endregion
}