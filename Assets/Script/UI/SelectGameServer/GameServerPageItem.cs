//===============================================
//作    者：
//创建时间：2022-04-11 16:33:11
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameServerPageItem : MonoBehaviour
{
    [SerializeField]
    private Text m_GameServerPageName;

    private int m_PageIndex;
    private Action<int> m_OnClick;

    public void Init(RetGameServerPageEntity entity, Action<int> onClick)
    {
        m_PageIndex = entity.PageIndex;
        m_OnClick = onClick;
        m_GameServerPageName.text = $"{ entity.BeginId } -- { entity.EndId } 服";
    }

    public void OnBtnClick()
    {
        if(m_OnClick != null)
        {
            m_OnClick(m_PageIndex);
        }
    }
}
