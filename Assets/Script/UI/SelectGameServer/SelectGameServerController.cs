//===============================================
//作    者：
//创建时间：2022-04-11 16:24:11
//备    注：
//===============================================
using UnityEngine;

public class SelectGameServerController : MonoBehaviour
{
    [SerializeField]
    private SelectGameServerWindow m_SelectGameServerWindow;

    private int m_CurGameServerPageIndex = -1;

    void Start()
    {
        ReqGameServerPageTaskAsync();
    }

    private async void ReqGameServerPageTaskAsync()
    {
        var requestResult = await GameServerModel.Instance.ReqGameServerPageTaskAsync();
        if (requestResult.IsSuccess && requestResult.ResponseValue.Code == 0)
        {
            m_SelectGameServerWindow.AddGameServerPageItem(requestResult.ResponseValue.Value);
        }
    }

    public async void OnClickGameServerPageItemTaskAsync(int pageIndex)
    {
        m_CurGameServerPageIndex = pageIndex;
        m_SelectGameServerWindow.ClearGameServerList();
        var requestResult = await GameServerModel.Instance.ReqGameServerTaskAsync(pageIndex);
        if (requestResult.IsSuccess && requestResult.ResponseValue.Code == 0 && pageIndex == m_CurGameServerPageIndex)
        {
            m_SelectGameServerWindow.UpdateGameServerList(requestResult.ResponseValue.Value);
        }
    }

    public void OnClickGameServerItem(int gameServerId)
    {
        DebugLogger.Log("点击了" + gameServerId + "游戏服");
    }
}
