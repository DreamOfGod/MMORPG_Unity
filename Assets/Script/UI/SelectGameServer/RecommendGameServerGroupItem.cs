//===============================================
//作    者：
//创建时间：2022-04-17 20:49:48
//备    注：
//===============================================
using UnityEngine;

public class RecommendGameServerGroupItem : MonoBehaviour
{
    private SelectGameServerController m_SelectGameServerController;

    public void Init(SelectGameServerController selectGameServerController)
    {
        m_SelectGameServerController = selectGameServerController;
    }

    public async void OnBtnClick()
    {
        if (m_SelectGameServerController.CurGameServerGroupFirstId == -1)
        {
            return;
        }
        m_SelectGameServerController.CurGameServerGroupFirstId = -1;
        m_SelectGameServerController.SelectGameServerWindow.ClearGameServerList();
        var requestResult = await GameServerModel.Instance.ReqRecommendGameServerListAsync();
        if (this == null || gameObject == null)
        {
            return;
        }
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            if (m_SelectGameServerController.CurGameServerGroupFirstId == -1)
            {
                m_SelectGameServerController.SelectGameServerWindow.UpdateGameServerList(requestResult.ResponseData.Data);
            }
        }
    }
}
