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
    public Action<int> OnClick;

    public void Init(RetGameServerPageEntity entity)
    {
        m_PageIndex = entity.PageIndex;
        m_GameServerPageName.text = string.Format("{0} -- {1} 服", entity.BeginId, entity.EndId);
    }

    public void OnBtnClick()
    {
        if(OnClick != null)
        {
            OnClick(m_PageIndex);
        }
    }
}