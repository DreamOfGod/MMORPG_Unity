//===============================================
//作    者：
//创建时间：2022-04-08 16:00:38
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;

public class LogonController : MonoBehaviour
{
    [SerializeField]
    private LogonView m_LogonView;
    [SerializeField]
    private InputField m_InputAccount;
    [SerializeField]
    private InputField m_InputPwd;

    public void ToRegister()
    {
        m_LogonView.OnViewCloseFinish = () => { ViewBase.MoveFromLeftToRightShowView(ViewPath.Register, transform.parent); };
        m_LogonView.MoveFromRightToLeftClose();
    }

    public void Logon()
    {
        string account = m_InputAccount.text;
        if(string.IsNullOrEmpty(account))
        {
            return;
        }
        string pwd = m_InputPwd.text;
        if(string.IsNullOrEmpty(pwd))
        {
            return;
        }

    }
}
