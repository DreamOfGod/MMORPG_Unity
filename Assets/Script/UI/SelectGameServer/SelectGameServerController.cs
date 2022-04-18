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

    public SelectGameServerWindow SelectGameServerWindow { get => m_SelectGameServerWindow; }

    [HideInInspector]
    public int CurGameServerGroupFirstId = -1;
    private EnterGameServerController m_EnterGameServerController;
    public EnterGameServerController EnterGameServerController { get => m_EnterGameServerController; }

    public void Init(EnterGameServerController enterGameServerController)
    {
        m_EnterGameServerController = enterGameServerController;
    }

    void Start()
    {
        //m_SelectGameServerWindow.InitSelectedGameServer();
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
        m_SelectGameServerWindow.AddGameServerGroupItem(gameServerGroupResult.ResponseData.Data);
        m_SelectGameServerWindow.UpdateGameServerList(recommendGameServerResult.ResponseData.Data);
    }
}
