//===============================================
//作    者：
//创建时间：2022-04-11 14:56:09
//备    注：
//===============================================
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameServerModel: Singleton<GameServerModel>
{
    private List<RetGameServerPageEntity> m_RetGameServerPageList;
    private List<RetGameServerEntity> m_RetGameServerList;

    #region 请求游戏服页签
    public async Task<RequestResult<List<RetGameServerPageEntity>>> ReqGameServerPageTaskAsync()
    {
        var requestResult = await NetWorkHttp.Instance.GetTaskAsync<List<RetGameServerPageEntity>>(NetWorkHttp.AccountServerURL + "game_server");
        if (requestResult.IsSuccess && requestResult.ResponseValue.Code == 0)
        {
            m_RetGameServerPageList = requestResult.ResponseValue.Value;
        }
        return requestResult;
    }
    #endregion

    #region 请求游戏服信息
    public async Task<RequestResult<List<RetGameServerEntity>>> ReqGameServerTaskAsync(int pageIndex)
    {
        var requestResult = await NetWorkHttp.Instance.GetTaskAsync<List<RetGameServerEntity>>(NetWorkHttp.AccountServerURL + "game_server?pageIndex=" + pageIndex);
        if (requestResult.IsSuccess && requestResult.ResponseValue.Code == 0)
        {
            m_RetGameServerList = requestResult.ResponseValue.Value;
        }
        return requestResult;
    }
    #endregion
}