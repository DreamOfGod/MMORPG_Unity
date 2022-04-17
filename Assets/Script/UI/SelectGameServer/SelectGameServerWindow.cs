//===============================================
//作    者：
//创建时间：2022-04-11 16:23:56
//备    注：
//===============================================
using System.Collections.Generic;
using UnityEngine;

public class SelectGameServerWindow : WindowBase
{
    [SerializeField]
    private SelectGameServerController m_SelectGameServerController;
    [SerializeField]
    private GameObject m_GameServerGroupItemPrefab;
    [SerializeField]
    private GameObject m_RecommendGameServerGroupItemPrefab;
    [SerializeField]
    private RectTransform m_GameServerGroupParent;
    [SerializeField]
    private GameObject m_GameServerItemPrefab;
    [SerializeField]
    private RectTransform m_GameServerParent;
    [SerializeField]
    private GameServerItem m_SelectedGameServer;

    private const int m_GameServerGroupListYGap = 4;
    private const int m_GameServerGroupListTopBottomPadding = 0;

    private const int m_GameServerListXGap = 5;
    private const int m_GameServerListYGap = 5;
    private const int m_GameServerListLeftPadding = 12;
    private const int m_GameServerListTopBottomPadding = 0;

    public void AddGameServerGroupItem(List<GameServerGroupBean> list)
    {
        float itemHeight = m_GameServerGroupItemPrefab.GetComponent<RectTransform>().rect.height;
        float parentHeight = itemHeight * (list.Count + 1) + list.Count * m_GameServerGroupListYGap + m_GameServerGroupListTopBottomPadding * 2;
        m_GameServerGroupParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentHeight);

        //推荐区服组
        GameObject item = Instantiate(m_RecommendGameServerGroupItemPrefab);
        item.transform.SetParent(m_GameServerGroupParent, false);
        float y = -m_GameServerGroupListTopBottomPadding - itemHeight / 2;
        item.transform.localPosition = new Vector3(0, y, 0);
        RecommendGameServerGroupItem recommendGameServerGroupItemScript = item.GetComponent<RecommendGameServerGroupItem>();
        recommendGameServerGroupItemScript.Init(m_SelectGameServerController);

        //其它区服组
        for (int i = 1; i <= list.Count; ++i)
        {
            item = Instantiate(m_GameServerGroupItemPrefab);
            item.transform.SetParent(m_GameServerGroupParent, false);
            y = -m_GameServerGroupListTopBottomPadding - (itemHeight + m_GameServerGroupListYGap) * i - itemHeight / 2;
            item.transform.localPosition = new Vector3(0, y, 0);
            GameServerGroupItem gameServerGroupItemScript = item.GetComponent<GameServerGroupItem>();
            gameServerGroupItemScript.Init(list[list.Count - i], m_SelectGameServerController);
        }
    }

    public void ClearGameServerList()
    {
        m_GameServerParent.DestroyChildren();
    }

    public void UpdateGameServerList(List<GameServerBean> list)
    {
        Rect itemRect = m_GameServerItemPrefab.GetComponent<RectTransform>().rect;
        float parentHeight = (itemRect.height + m_GameServerListYGap) * (list.Count / 2 + list.Count % 2) + m_GameServerListTopBottomPadding * 2;
        m_GameServerParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentHeight);
        for (int i = 0; i < list.Count; ++i)
        {
            GameObject item = Instantiate(m_GameServerItemPrefab);
            item.transform.SetParent(m_GameServerParent, false);
            float x = m_GameServerListLeftPadding + (itemRect.width + m_GameServerListXGap) * (i % 2) + (itemRect.width / 2);
            float y = -m_GameServerListTopBottomPadding - (itemRect.height + m_GameServerListYGap) * (i / 2) - (itemRect.height / 2);
            item.transform.localPosition = new Vector3(x, y, 0);
            GameServerItem itemScript = item.GetComponent<GameServerItem>();
            itemScript.Init(list[i], m_SelectGameServerController);
        }
    }

    public void InitSelectedGameServer(GameServerBean gameServer)
    {
        m_SelectedGameServer.Init(gameServer, m_SelectGameServerController);
    }
}
