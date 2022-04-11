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
    private GameObject m_GameServerPageItemPrefab;
    [SerializeField]
    private RectTransform m_GameServerPageParent;
    [SerializeField]
    private GameObject m_GameServerItemPrefab;
    [SerializeField]
    private RectTransform m_GameServerParent;

    private const int m_GameServerPageItemGap = 4;
    private const int m_GameServerPageListTopBottomPadding = 0;

    private const int m_GameServerItemXGap = 5;
    private const int m_GameServerItemYGap = 5;
    private const int m_GameServerListLeftPadding = 12;
    private const int m_GameServerListTopBottomPadding = 0;

    public void AddGameServerPageItem(List<RetGameServerPageEntity> list)
    {
        float itemHeight = m_GameServerPageItemPrefab.GetComponent<RectTransform>().rect.height;
        float parentHeight = itemHeight * list.Count + (list.Count - 1) * m_GameServerPageItemGap + m_GameServerPageListTopBottomPadding * 2;
        m_GameServerPageParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentHeight);
        for (int i = 0; i < list.Count; ++i)
        {
            GameObject item = Instantiate(m_GameServerPageItemPrefab);
            item.transform.SetParent(m_GameServerPageParent, false);
            float y = -m_GameServerPageListTopBottomPadding - (itemHeight + m_GameServerPageItemGap) * i - itemHeight / 2;
            item.transform.localPosition = new Vector3(0, y, 0);
            GameServerPageItem itemScript = item.GetComponent<GameServerPageItem>();
            itemScript.Init(list[list.Count - 1 - i], m_SelectGameServerController.OnClickGameServerPageItem);
        }
    }

    public void ClearGameServerList()
    {
        m_GameServerParent.DestroyChildren();
    }

    public void UpdateGameServerList(List<RetGameServerEntity> list)
    {
        Rect itemRect = m_GameServerItemPrefab.GetComponent<RectTransform>().rect;
        float parentHeight = (itemRect.height + m_GameServerItemYGap) * (list.Count / 2 + list.Count % 2) + m_GameServerListTopBottomPadding * 2;
        m_GameServerParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentHeight);
        for (int i = 0; i < list.Count; ++i)
        {
            GameObject item = Instantiate(m_GameServerItemPrefab);
            item.transform.SetParent(m_GameServerParent, false);
            float x = m_GameServerListLeftPadding + (itemRect.width + m_GameServerItemXGap) * (i % 2) + (itemRect.width / 2);
            float y = -m_GameServerListTopBottomPadding - (itemRect.height + m_GameServerItemYGap) * (i / 2) - (itemRect.height / 2);
            item.transform.localPosition = new Vector3(x, y, 0);
            GameServerItem itemScript = item.GetComponent<GameServerItem>();
            itemScript.Init(list[i], m_SelectGameServerController.OnClickGameServerItem);
        }
    }
}
