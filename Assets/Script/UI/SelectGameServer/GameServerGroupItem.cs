//===============================================
//作    者：
//创建时间：2022-04-11 16:33:11
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameServerGroupItem : MonoBehaviour
{
    [SerializeField]
    private Text m_GameServerGroupName;

    private GameServerGroupBean m_Data;
    private SelectGameServerController m_SelectGameServerController;
    private bool m_IsReqList = false;

    public void Init(GameServerGroupBean data, SelectGameServerController selectGameServerController)
    {
        m_Data = data;
        m_GameServerGroupName.text = $"{ data.FirstId } - { data.LastId } 服";
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
        var requestResult = await GameServerModel.Instance.ReqGameServerListAsync(m_Data.FirstId, m_Data.LastId);
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
