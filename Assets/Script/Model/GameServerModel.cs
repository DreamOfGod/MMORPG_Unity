//===============================================
//作    者：
//创建时间：2022-04-11 14:56:09
//备    注：
//===============================================
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// 游戏服相关逻辑
/// </summary>
public class GameServerModel: IModel
{
    #region 单例
    private GameServerModel() { }
    private static GameServerModel instance = new GameServerModel();
    public static GameServerModel Instance { get => instance; }
    #endregion

    //服务器分组列表
    private List<GameServerGroupBean> m_GameServerGroupList;
    //已有角色列表
    private List<RoleOperation_LogOnGameServerReturnProto.RoleItem> m_RoleList;
    /// <summary>
    /// 已有角色列表
    /// </summary>
    public List<RoleOperation_LogOnGameServerReturnProto.RoleItem> RoleList { get => m_RoleList; }

    public event Action<List<RoleOperation_LogOnGameServerReturnProto.RoleItem>> LogonGameServerReturn;
    public event Action<RoleOperation_CreateRoleReturnProto> CreateRoleReturn;
    public event Action<bool, int> StartGameReturn;

    public void Init()
    {
        SocketHelper.Instance.AddListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogonGameServerReturn);
        SocketHelper.Instance.AddListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturn);
        SocketHelper.Instance.AddListener(ProtoCodeDef.RoleOperation_EnterGameReturn, OnStartGameReturn);
    }

    public void Reset()
    {
        SocketHelper.Instance.RemoveListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogonGameServerReturn);
        SocketHelper.Instance.RemoveListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturn);
        SocketHelper.Instance.RemoveListener(ProtoCodeDef.RoleOperation_EnterGameReturn, OnStartGameReturn);
    }

    #region 请求服务器组
    /// <summary>
    /// 请求服务器组
    /// </summary>
    /// <returns></returns>
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

    #region 请求游戏服信息
    /// <summary>
    /// 请求服务器列表
    /// </summary>
    /// <param name="firstId"></param>
    /// <param name="lastId"></param>
    /// <returns></returns>
    public async Task<RequestResult<List<GameServerBean>>> ReqGameServerListAsync(int firstId, int lastId)
    {
        string url = $"{ HttpHelper.AccountServerURL }game_server_list?firstId={ firstId }&lastId={ lastId }";
        return await HttpHelper.Instance.GetAsync<List<GameServerBean>>(url);
    }
    /// <summary>
    /// 请求推荐服务器列表
    /// </summary>
    /// <returns></returns>
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

    #region 登陆游戏服
    /// <summary>
    /// 登陆游戏服
    /// </summary>
    public void ReqLogonGameServer()
    {
        var protocol = new RoleOperation_LogOnGameServerProto();
        protocol.AccountId = AccountModel.Instance.AccountID;
        SocketHelper.Instance.BeginSend(protocol.ToArray());
    }
    //登陆游戏服消息回调
    private void OnLogonGameServerReturn(byte[] buffer)
    {
        var protocol = RoleOperation_LogOnGameServerReturnProto.GetProto(buffer);
        m_RoleList = protocol.RoleList;
        LogonGameServerReturn?.Invoke(protocol.RoleList);
    }
    #endregion

    #region 创建角色
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="nickname"></param>
    public void ReqCreateRole(byte jobId, string nickname)
    {
        var proto = new RoleOperation_CreateRoleProto();
        proto.JobId = jobId;
        proto.RoleNickName = nickname;
        SocketHelper.Instance.BeginSend(proto.ToArray());
    }
    //创建角色返回消息回调
    private void OnCreateRoleReturn(byte[] buffer)
    {
        var proto = RoleOperation_CreateRoleReturnProto.GetProto(buffer);
        DebugLogger.Log($"创建角色返回消息：isSuccess={ proto.IsSuccess }{ (proto.IsSuccess ? "" : $"， MsgCode={ proto.MsgCode }")  }");
        if (proto.IsSuccess)
        {
            m_RoleList.Add(new RoleOperation_LogOnGameServerReturnProto.RoleItem() { RoleId = proto.RoleId, RoleNickName = proto.RoleNickName, RoleJob = proto.RoleJob, RoleLevel = proto.RoleLevel });
        }
        CreateRoleReturn?.Invoke(proto);
    }
    #endregion

    #region 开始游戏
    /// <summary>
    /// 请求开始游戏
    /// </summary>
    /// <param name="roleId"></param>
    public void ReqStartGame(int roleId)
    {
        var proto = new RoleOperation_EnterGameProto();
        proto.RoleId = roleId;
        SocketHelper.Instance.BeginSend(proto.ToArray());
    }
    //开始游戏返回消息回调
    private void OnStartGameReturn(byte[] buffer)
    {
        var proto = RoleOperation_EnterGameReturnProto.GetProto(buffer);
        StartGameReturn?.Invoke(proto.IsSuccess, proto.MsgCode);
    }
    #endregion
}