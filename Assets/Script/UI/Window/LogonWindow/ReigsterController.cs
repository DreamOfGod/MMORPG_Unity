//===============================================
//作    者：
//创建时间：2022-04-08 16:20:58
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 注册窗口控制器
/// </summary>
public class ReigsterController : MonoBehaviour
{
    [SerializeField]
    private ReigsterWindow m_ReigsterView;
    [SerializeField]
    private InputField m_InputUsername;
    [SerializeField]
    private InputField m_InputPwd;
    [SerializeField]
    private InputField m_InputYaoQingMa;

    private bool m_IsRegistering = false;

    public void ToLogon()
    {
        if(m_IsRegistering)
        {
            return;
        }
        m_ReigsterView.CloseSelfAndOpenLogon();
    }

    public void Register()
    {
        string username = m_InputUsername.text;
        if (string.IsNullOrEmpty(username))
        {
            m_ReigsterView.ShowRegisterTip("请输入账号");
            return;
        }
        string pwd = m_InputPwd.text;
        if (string.IsNullOrEmpty(pwd))
        {
            m_ReigsterView.ShowRegisterTip("请输入密码");
            return;
        }
        if(m_IsRegistering)
        {
            return;
        }
        m_IsRegistering = true;
        AccountModel.Instance.Register(username, pwd, (UnityWebRequest.Result result, MFReturnValue<int> ret) => {
            if (this == null || gameObject == null)
            {
                return;
            }
            m_IsRegistering = false;
            if (result == UnityWebRequest.Result.Success)
            {
                if (ret.HasError)
                {
                    m_ReigsterView.ShowRegisterTip("账号已存在");
                }
                else
                {
                    m_ReigsterView.ShowRegisterTip("注册成功", m_ReigsterView.CloseSelfAndOpenLogon);
                }
            }
            else
            {
                m_ReigsterView.ShowRegisterTip(result.ToString());
            }
        });
    }
}