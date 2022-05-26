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
    /// 关闭动画执行完毕的回调
    /// </summary>
    public Action OnCloseFinish;

    /// <summary>
    /// 确定按钮回调
    /// </summary>
    public void OnBtnOK()
    {
        if(OnCloseFinish != null)
        {
            m_MessageView.OnWindowCloseFinish = () => {
                OnCloseFinish();
            };
        }
        m_MessageView.ZoomOutClose();
        OnClickOK?.Invoke();
    }

    /// <summary>
    /// 取消按钮回调
    /// </summary>
    public void OnBtnCancel()
    {
        if (OnCloseFinish != null)
        {
            m_MessageView.OnWindowCloseFinish = () => {
                OnCloseFinish();
            };
        }
        m_MessageView.ZoomOutClose();
        OnClickCancel?.Invoke();
    }
}