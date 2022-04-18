//===============================================
//作    者：
//创建时间：2022-04-08 16:00:38
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 登录窗口控制器
/// </summary>
public class LogonController : MonoBehaviour
{
    [SerializeField]
    private LogonWindow m_LogonWindow;
    [SerializeField]
    private InputField m_InputUsername;
    [SerializeField]
    private InputField m_InputPwd;

    private bool m_IsLogoning = false;
    private bool m_IsToRegister = false;
    public void OnClickToRegister()
    {
        if(m_IsLogoning || m_IsToRegister)
        {
            return;
        }
        m_IsToRegister = true;
        m_LogonWindow.OnWindowCloseFinish = () => { WindowBase.OpenWindowMoveFromLeftToRightShow(WindowPath.Register, transform.parent); };
        m_LogonWindow.MoveFromRightToLeftClose();
    }

    public async void OnClickLogon()
    {
        if(m_IsToRegister)
        {
            return;
        }
        var username = m_InputUsername.text.Trim();
        if(string.IsNullOrEmpty(username))
        {
            MessageWindow.Show(transform.parent, "登录提示", "请输入账号", true, false);
            return;
        }
        var pwd = m_InputPwd.text.Trim();
        if(string.IsNullOrEmpty(pwd))
        {
            MessageWindow.Show(transform.parent, "登录提示", "请输入密码", true, false);
            return;
        }
        if(m_IsLogoning)
        {
            return;
        }
        m_IsLogoning = true;
        var logonResult = await AccountModel.Instance.LogonAsync(username, pwd);
        if (this == null || gameObject == null)
        {
            return;
        }
        m_IsLogoning = false;
        if (logonResult.IsSuccess)
        {
            switch (logonResult.ResponseData.Code)
            {
                case 0:
                    GameServerBean lastLogonServer = AccountModel.Instance.LastLogonServer;
                    if(lastLogonServer == null)
                    {
                        var recommendGameServerListResult = await GameServerModel.Instance.ReqRecommendGameServerListAsync();
                        if(!recommendGameServerListResult.IsSuccess || recommendGameServerListResult.ResponseData.Code != 0)
                        {
                            return;
                        }
                        lastLogonServer = recommendGameServerListResult.ResponseData.Data[0];
                    }
                    m_LogonWindow.OnWindowCloseFinish = () => {
                        WindowBase window = WindowBase.OpenWindowMoveFromLeftToRightShow(WindowPath.EnterGameServer, transform.parent);
                        EnterGameServerController enterGameServerController = window.GetComponent<EnterGameServerController>();
                        enterGameServerController.SetCurSelectGameServer(lastLogonServer);
                    };
                    m_LogonWindow.MoveFromRightToLeftClose();
                    break;
                case 1: MessageWindow.Show(transform.parent, "登录提示", "账号或密码错误", true, false); break;
            }
        }
    }
}