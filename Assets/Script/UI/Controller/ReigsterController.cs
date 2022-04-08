//===============================================
//作    者：
//创建时间：2022-04-08 16:20:58
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;

public class ReigsterController : MonoBehaviour
{
    [SerializeField]
    private ReigsterView m_ReigsterView;
    [SerializeField]
    private InputField m_InputAccount;
    [SerializeField]
    private InputField m_InputPwd;
    [SerializeField]
    private InputField m_InputYaoQingMa;

    public void ToLogon()
    {
        m_ReigsterView.OnViewCloseFinish = () => { ViewBase.MoveFromLeftToRightShowView(ViewPath.Logon, transform.parent); };
        m_ReigsterView.MoveFromRightToLeftClose();
    }

    public void Register()
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
        string yaoQingMa = m_InputYaoQingMa.text;
        if(string.IsNullOrEmpty(yaoQingMa))
        {
            return;
        }
    }
}