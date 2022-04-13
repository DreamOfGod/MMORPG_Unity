//===============================================
//作    者：
//创建时间：2022-04-08 16:00:38
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 登录窗口控制器
/// </summary>
public class LogonController : MonoBehaviour
{
    [SerializeField]
    private LogonWindow m_LogonView;
    [SerializeField]
    private InputField m_InputUsername;
    [SerializeField]
    private InputField m_InputPwd;

    private bool m_IsLogoning = false;

    public void ToRegister()
    {
        m_LogonView.CloseSelfAndOpenRegister();
    }

    public async void LogonTaskAsync()
    {
        string username = m_InputUsername.text;
        if(string.IsNullOrEmpty(username))
        {
            m_LogonView.ShowLogonTip("请输入账号");
            return;
        }
        string pwd = m_InputPwd.text;
        if(string.IsNullOrEmpty(pwd))
        {
            m_LogonView.ShowLogonTip("请输入密码");
            return;
        }
        if(m_IsLogoning)
        {
            return;
        }
        m_IsLogoning = true;
        var requestResult = await AccountModel.Instance.LogonTaskAsync(username, pwd);
        if (this == null || gameObject == null)
        {
            return;
        }
        m_IsLogoning = false;
        if (requestResult.IsSuccess)
        {
            switch (requestResult.ResponseValue.Code)
            {
                case 0:
                    m_LogonView.CloseSelfAndOpenGameServerEnter(); break;
                case 1:
                    m_LogonView.ShowLogonTip("账号或密码错误"); break;
                default:
                    m_LogonView.ShowLogonTip(requestResult.ResponseValue.Error); break;
            }
        }
        else
        {
            m_LogonView.ShowLogonTip(requestResult.Result.ToString());
        }
    }
}