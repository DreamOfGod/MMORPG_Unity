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

    public void Init(GameServerGroupBean data, SelectGameServerController selectGameServerController)
    {
        m_Data = data;
        m_GameServerGroupName.text = $"{ data.FirstId } - { data.LastId } 服";
        m_SelectGameServerController = selectGameServerController;
    }

    public async void OnBtnClick()
    {
        if (m_SelectGameServerController.CurGameServerGroupFirstId == m_Data.FirstId)
        {
            return;
        }
        m_SelectGameServerController.CurGameServerGroupFirstId = m_Data.FirstId;
        m_SelectGameServerController.SelectGameServerWindow.ClearGameServerList();
        var requestResult = await GameServerModel.Instance.ReqGameServerListAsync(m_Data.FirstId, m_Data.LastId);
        if (this == null || gameObject == null)
        {
            return;
        }
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0 && m_SelectGameServerController.CurGameServerGroupFirstId == m_Data.FirstId)
        {
            m_SelectGameServerController.SelectGameServerWindow.UpdateGameServerList(requestResult.ResponseData.Data);
        }
    }
}
