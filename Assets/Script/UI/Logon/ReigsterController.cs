//===============================================
//作    者：
//创建时间：2022-04-08 16:20:58
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 注册窗口控制器
/// </summary>
public class ReigsterController : MonoBehaviour
{
    [SerializeField]
    private ReigsterWindow m_ReigsterWindow;
    [SerializeField]
    private InputField m_InputUsername;
    [SerializeField]
    private InputField m_InputPwd;
    [SerializeField]
    private InputField m_InputYaoQingMa;

    private bool m_IsRegistering = false;
    private bool m_IsToLogon = false;
    private bool m_RegisterSuccess = false;

    public void OnClickToLogon()
    {
        if(m_IsRegistering || m_RegisterSuccess || m_IsToLogon)
        {
            return;
        }
        m_IsToLogon = true;
        OpenLogon();
    }

    private void OpenLogon()
    {
        m_ReigsterWindow.OnWindowCloseFinish = () => { WindowBase.OpenWindowMoveFromLeftToRightShow(WindowPath.Logon, transform.parent); };
        m_ReigsterWindow.MoveFromRightToLeftClose();
    }

    public async void OnClickRegister()
    {
        if(m_IsToLogon || m_RegisterSuccess)
        {
            return;
        }
        var username = m_InputUsername.text.Trim();
        if (string.IsNullOrEmpty(username))
        {
            MessageWindow.Show(transform.parent, "注册提示", "请输入账号", true, false);
            return;
        }
        var pwd = m_InputPwd.text.Trim();
        if (string.IsNullOrEmpty(pwd))
        {
            MessageWindow.Show(transform.parent, "注册提示", "请输入密码", true, false);
            return;
        }
        if(m_IsRegistering)
        {
            return;
        }
        m_IsRegistering = true;
        var requestResult = await AccountModel.Instance.RegisterAsync(username, pwd);
        if (this == null || gameObject == null)
        {
            return;
        }
        m_IsRegistering = false;
        if (requestResult.IsSuccess)
        {
            switch(requestResult.ResponseData.Code)
            {
                case 0:
                    m_RegisterSuccess = true;
                    MessageWindow.Show(transform.parent, "注册提示", "注册成功", true, false, onClickOK: OpenLogon); break;
                case 1:
                    MessageWindow.Show(transform.parent, "注册提示", "账号已存在", true, false); break;
                default:
                    MessageWindow.Show(transform.parent, "注册提示", requestResult.ResponseData.Error, true, false); break;
            }
        }
        else
        {
            MessageWindow.Show(transform.parent, "注册提示", requestResult.Result.ToString(), true, false);
        }
    }
}