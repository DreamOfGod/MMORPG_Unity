//===============================================
//作    者：
//创建时间：2022-04-17 20:49:48
//备    注：
//===============================================
using UnityEngine;

public class RecommendGameServerGroupItem : MonoBehaviour
{
    private SelectGameServerController m_SelectGameServerController;
    private bool m_IsReqList = false;

    public void Init(SelectGameServerController selectGameServerController)
    {
        m_SelectGameServerController = selectGameServerController;
    }

    public async void OnBtnClick()
    {
        if(m_IsReqList)
        {
            return;
        }
        m_IsReqList = true;
        int seqNo = m_SelectGameServerController.GetServerListReqSeqNum();
        m_SelectGameServerController.SelectGameServerWindow.ClearGameServerList();
        var requestResult = await GameServerModel.Instance.ReqRecommendGameServerListAsync();
        if (this == null || gameObject == null)
        {
            return;
        }
        m_IsReqList = false;
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0 && m_SelectGameServerController.LatestServerListReqSeqNum == seqNo)
        {
            //只更新最新的请求的响应
            m_SelectGameServerController.SelectGameServerWindow.UpdateGameServerList(requestResult.ResponseData.Data, m_SelectGameServerController);
        }
    }
}
