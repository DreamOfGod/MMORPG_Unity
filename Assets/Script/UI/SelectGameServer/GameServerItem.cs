//===============================================
//作    者：
//创建时间：2022-04-11 21:23:10
//备    注：
//===============================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameServerItem : MonoBehaviour
{
    [SerializeField]
    private Text m_GameServerName;
    [SerializeField]
    private Image m_Status;

    private int m_GameServerId;
    private Action<int> m_OnClick;

    public void Init(RetGameServerEntity entity, Action<int> onClick)
    {
        m_GameServerId = entity.Id;
        m_GameServerName.text = entity.Name;
        m_OnClick = onClick;
    }

    public void OnBtnClick()
    {
        if(m_OnClick != null)
        {
            m_OnClick(m_GameServerId);
        }
    }
}
