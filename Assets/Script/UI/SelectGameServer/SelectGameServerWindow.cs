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
    private RectTransform m_GameServerPageParent;
    [SerializeField]
    private RectTransform m_GameServerParent;

    private const int m_GameServerPageItemGap = 5;
    private const int m_GameServerPageListTopBottomPadding = 0;

    public void AddGameServerPageItem(List<RetGameServerPageEntity> list)
    {
        GameObject prefab = Resources.Load<GameObject>("UIPrefab/SelectGameServer/GameServerPageItem");
        float itemHeight = prefab.GetComponent<RectTransform>().rect.height;
        float parentHeight = itemHeight * list.Count + (list.Count - 1) * m_GameServerPageItemGap + m_GameServerPageListTopBottomPadding * 2;
        m_GameServerPageParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentHeight);
        for (int i = 0; i < list.Count; ++i)
        {
            GameObject item = Instantiate(prefab);
            item.transform.SetParent(m_GameServerPageParent, false);
            float y = -m_GameServerPageListTopBottomPadding - (itemHeight + m_GameServerPageItemGap) * i - itemHeight / 2;
            item.transform.localPosition = new Vector3(0, y, 0);
            GameServerPageItem itemScript = item.GetComponent<GameServerPageItem>();
            itemScript.OnClick = m_SelectGameServerController.OnClickGameServerPageItem;
            itemScript.Init(list[i]);
        }
    }

    public void UpdateGameServerList(List<RetGameServerEntity> list)
    {
        m_GameServerParent.DestroyChildren();

    }
}
