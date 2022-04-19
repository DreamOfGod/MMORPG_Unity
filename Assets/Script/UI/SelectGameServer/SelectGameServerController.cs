//===============================================
//作    者：
//创建时间：2022-04-11 16:24:11
//备    注：
//===============================================
using System.Threading.Tasks;
using UnityEngine;

public class SelectGameServerController : MonoBehaviour
{
    [SerializeField]
    private SelectGameServerWindow m_SelectGameServerWindow;

    private int m_ServerListReqSeqNum = 0;
    private EnterGameServerController m_EnterGameServerController;
    private bool m_IsClose = false;

    public SelectGameServerWindow SelectGameServerWindow { get => m_SelectGameServerWindow; }

    public int LatestServerListReqSeqNum { get => m_ServerListReqSeqNum; }
    public int GetServerListReqSeqNum()
    {
        return ++m_ServerListReqSeqNum;
    }

    public EnterGameServerController EnterGameServerController { get => m_EnterGameServerController; }

    public bool IsClose { get => m_IsClose; }

    public void SetCloseStatus()
    {
        m_IsClose = true;
    }

    public void Init(EnterGameServerController enterGameServerController)
    {
        m_EnterGameServerController = enterGameServerController;
        m_SelectGameServerWindow.SetSelectedGameServer(m_EnterGameServerController.CurSelectGameServer);
        ReqGameServerGroupAndRecommendGameServerAsync();
    }

    private async void ReqGameServerGroupAndRecommendGameServerAsync()
    {
        var gameServerGroupTask = GameServerModel.Instance.ReqGameServerGroupAsync();
        var recommendGameServerTask = GameServerModel.Instance.ReqRecommendGameServerListAsync();
        await Task.WhenAll(gameServerGroupTask, recommendGameServerTask);
        if(this == null || gameObject == null)
        {
            return;
        }
        var gameServerGroupResult = gameServerGroupTask.Result;
        var recommendGameServerResult = recommendGameServerTask.Result;
        if(!gameServerGroupResult.IsSuccess || gameServerGroupResult.ResponseData.Code != 0
            || !recommendGameServerResult.IsSuccess || recommendGameServerResult.ResponseData.Code != 0)
        {
            return;
        }
        m_SelectGameServerWindow.AddGameServerGroupItem(gameServerGroupResult.ResponseData.Data, this);
        m_SelectGameServerWindow.UpdateGameServerList(recommendGameServerResult.ResponseData.Data, this);
    }

    public void OnClickClose()
    {
        if(m_IsClose)
        {
            return;
        }
        m_IsClose = true;
        m_SelectGameServerWindow.ZoomOutClose();
    }
}
