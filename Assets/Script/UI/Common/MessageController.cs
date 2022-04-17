//===============================================
//作    者：
//创建时间：2022-04-10 15:40:41
//备    注：
//===============================================
using System;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    private MessageWindow m_MessageView;

    /// <summary>
    /// 确定回调
    /// </summary>
    public Action OnClickOK;

    /// <summary>
    /// 取消回调
    /// </summary>
    public Action OnClickCancel;

    /// <summary>
    /// 确定按钮回调
    /// </summary>
    public void OnBtnOK()
    {
        if(OnClickOK != null)
        {
            OnClickOK();
        }
        m_MessageView.ZoomOutClose();
    }

    /// <summary>
    /// 取消按钮回调
    /// </summary>
    public void OnBtnCancel()
    {
        if (OnClickCancel != null)
        {
            OnClickCancel();
        }
        m_MessageView.ZoomOutClose();
    }
}