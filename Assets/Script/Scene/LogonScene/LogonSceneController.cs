//===============================================
//作    者：
//创建时间：2022-02-23 16:44:48
//备    注：
//===============================================
using UnityEngine;

public class LogonSceneController : MonoBehaviour
{
    [SerializeField]
    private Transform m_WindowParent;

    private void Start()
    {
        CheckAccount();
    }

    private async void CheckAccount()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey.AccountID))
        {
            var requestResult = await AccountModel.Instance.QuickLogonAsync();
            if (this == null || gameObject == null)
            {
                return;
            }
            if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
            {
                ShowEnterGameServer();
            }
            else
            {
                var window = WindowBase.OpenWindowZoomInShow(WindowPath.Logon, m_WindowParent);
                var logonController = window.GetComponent<LogonController>();
                logonController.Init(this);
            }
        }
        else
        {
            WindowBase window = WindowBase.OpenWindowZoomInShow(WindowPath.Register, m_WindowParent);
            window.GetComponent<ReigsterController>().Init(this);
        }
    }

    public async void ShowEnterGameServer()
    {
        var isShowSuccess = await EnterGameServerWindow.Show(m_WindowParent);
        if(this == null || gameObject == null)
        {
            return;
        }
        if (!isShowSuccess)
        {
            MessageWindow.Show(m_WindowParent, "提示", "请求推荐服务器失败，点击重试", true, false, ShowEnterGameServer);
        }
    }
}