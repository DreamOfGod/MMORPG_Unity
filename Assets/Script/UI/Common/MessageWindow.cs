//===============================================
//作    者：
//创建时间：2022-04-10 15:40:22
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow : WindowBase
{
    [SerializeField]
    private Text m_TextTitle;
    [SerializeField]
    private Text m_TextMessage;
    [SerializeField]
    private GameObject m_OKBtn;
    [SerializeField]
    private GameObject m_CancelBtn;

    public static void Show(Transform parent, string title, string message, bool okBtnActive, bool cancelBtnActive, Action onClickOK = null, Action onClickCancel = null, Action onCloseFinish = null)
    {
        MessageWindow window = (MessageWindow)OpenWindowZoomInShow(WindowPath.Message, parent);
        window.SetTitle(title);
        window.SetMessage(message);
        window.SetBtnActive(okBtnActive, cancelBtnActive);
        MessageController controller = window.GetComponent<MessageController>();
        controller.OnClickOK = onClickOK;
        controller.OnClickCancel = onClickCancel;
        controller.OnCloseFinish = onCloseFinish;
    }

    public void SetTitle(string title)
    {
        m_TextTitle.text = title;
    }

    public void SetMessage(string message)
    {
        m_TextMessage.text = message;
    }

    public void SetBtnActive(bool okBtnActive, bool cancelBtnActive)
    {
        m_OKBtn.SetActive(okBtnActive);
        m_CancelBtn.SetActive(cancelBtnActive);
        if(okBtnActive && !cancelBtnActive)
        {
            m_OKBtn.transform.localPosition = new Vector3(0, m_OKBtn.transform.localPosition.y, 0);
        }
        else if(!okBtnActive && cancelBtnActive)
        {
            m_CancelBtn.transform.localPosition = new Vector3(0, m_CancelBtn.transform.localPosition.y, 0);
        }
    }
}