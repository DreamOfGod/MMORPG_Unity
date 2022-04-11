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
        GameServerModel.Instance.ReqGameServerPage((UnityWebRequest.Result result, MFReturnValue<List<RetGameServerPageEntity>> ret) => {
            if (result == UnityWebRequest.Result.Success && !ret.HasError)
            {
                m_SelectGameServerWindow.AddGameServerPageItem(ret.Value);
            }
        });
    }

    public void OnClickGameServerPageItem(int pageIndex)
    {
        m_CurGameServerPageIndex = pageIndex;
        m_SelectGameServerWindow.ClearGameServerList();
        GameServerModel.Instance.ReqGameServer(pageIndex, ReqGameServerCallback);
    }

    private void ReqGameServerCallback(UnityWebRequest.Result result, int pageIndex, MFReturnValue<List<RetGameServerEntity>> ret)
    {
        if (result == UnityWebRequest.Result.Success && !ret.HasError && pageIndex == m_CurGameServerPageIndex)
        {
            m_SelectGameServerWindow.UpdateGameServerList(ret.Value);
        }
    }

    public void OnClickGameServerItem(int gameServerId)
    {
        DebugLogger.Log("点击了" + gameServerId + "游戏服");
    }
}
