//===============================================
//作    者：
//创建时间：2022-04-10 22:49:51
//备    注：
//===============================================
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnterGameServerWindow : WindowBase
{
    [SerializeField]
    private Text m_TextCurSelectGameServer;

    /// <summary>
    /// 显示进入区服窗口
    /// </summary>
    /// <param name="parent"></param>
    public static async Task<bool> Show(Transform parent)
    {
        GameServerBean lastLogonServer = AccountModel.Instance.LastLogonServer;
        if (lastLogonServer == null)
        {
            var recommendGameServerListResult = await GameServerModel.Instance.ReqRecommendGameServerListAsync();
            if (!recommendGameServerListResult.IsSuccess || recommendGameServerListResult.ResponseData.Code != 0)
            {
                return false;
            }
            lastLogonServer = recommendGameServerListResult.ResponseData.Data[0];
        }
        WindowBase window = WindowBase.OpenWindowZoomInShow(WindowPath.EnterGameServer, parent);
        var enterGameServerController = window.GetComponent<EnterGameServerController>();
        enterGameServerController.SetCurSelectGameServer(lastLogonServer);
        return true;
    }

    public void SetCurSelectGameServer(string name)
    {
        m_TextCurSelectGameServer.text = name;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
