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

    public void Logon()
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
        AccountModel.Instance.Logon(username, pwd, (UnityWebRequest.Result result, MFReturnValue<int> ret) =>
        {
            if (this == null || gameObject == null)
            {
                return;
            }
            m_IsLogoning = false;
            if (result == UnityWebRequest.Result.Success)
            {
                if (ret.HasError)
                {
                    m_LogonView.ShowLogonTip("账号或密码错误");
                }
                else
                {
                    m_LogonView.CloseSelfAndOpenGameServerEnter();
                }
            }
            else
            {
                m_LogonView.ShowLogonTip(result.ToString());
            }
        });
    }
}