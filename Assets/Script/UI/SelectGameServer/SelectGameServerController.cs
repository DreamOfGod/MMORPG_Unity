//===============================================
//作    者：
//创建时间：2022-04-11 16:24:11
//备    注：
//===============================================
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SelectGameServerController : MonoBehaviour
{
    [SerializeField]
    private SelectGameServerWindow m_SelectGameServerWindow;

    private int m_CurGameServerPageIndex = -1;

    void Start()
    {
        ReqGameServerPage();
    }

    private void ReqGameServerPage()
    {
        GameServerModel.Instance.ReqGameServerPage((UnityWebRequest.Result result, ResponseValue<List<RetGameServerPageEntity>> responseValue) => {
            if (result == UnityWebRequest.Result.Success && responseValue.Code == 0)
            {
                m_SelectGameServerWindow.AddGameServerPageItem(responseValue.Value);
            }
        });
    }

    public void OnClickGameServerPageItem(int pageIndex)
    {
        m_CurGameServerPageIndex = pageIndex;
        m_SelectGameServerWindow.ClearGameServerList();
        GameServerModel.Instance.ReqGameServer(pageIndex, ReqGameServerCallback);
    }

    private void ReqGameServerCallback(UnityWebRequest.Result result, int pageIndex, ResponseValue<List<RetGameServerEntity>> responseValue)
    {
        if (result == UnityWebRequest.Result.Success && responseValue.Code == 0 && pageIndex == m_CurGameServerPageIndex)
        {
            m_SelectGameServerWindow.UpdateGameServerList(responseValue.Value);
        }
    }

    public void OnClickGameServerItem(int gameServerId)
    {
        DebugLogger.Log("点击了" + gameServerId + "游戏服");
    }
}
