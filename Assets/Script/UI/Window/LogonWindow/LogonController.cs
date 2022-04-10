//===============================================
//作    者：
//创建时间：2022-04-08 16:00:38
//备    注：
//===============================================
using LitJson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 登录视图控制器
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
        m_LogonView.OnWindowCloseFinish = () => { WindowBase.OpenWindowMoveFromLeftToRightShow(WindowPath.Register, transform.parent); };
        m_LogonView.MoveFromRightToLeftClose();
    }

    public void Logon()
    {
        string username = m_InputUsername.text;
        if(string.IsNullOrEmpty(username))
        {
            MessageWindow.Show(transform.parent, "登录提示", "请输入账号", true, false);
            return;
        }
        string pwd = m_InputPwd.text;
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
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Username"] = username;
        dic["Pwd"] = pwd;
        dic["ChannelId"] = 0;
        dic["DeviceModel"] = DeviceUtil.DeviceModel;

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "api/Logon", dic, (UnityWebRequest.Result result, string text) => {
            if (this == null || gameObject == null)
            {
                return;
            }
            m_IsLogoning = false;
            if (result == UnityWebRequest.Result.Success)
            {
                MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
                if (ret.HasError)
                {
                    DebugLogger.LogError(ret.Message);
                    MessageWindow.Show(transform.parent, "登录提示", "账号或密码错误", true, false);
                }
                else
                {
                    DebugLogger.LogFormat("登录成功，账号ID：{0}", ret.Value);
                    Statistics.Logon(ret.Value, username);
                    MessageWindow.Show(transform.parent, "登录提示", "登录成功", true, false);
                }
            }
            else
            {
                MessageWindow.Show(transform.parent, "登录提示", result.ToString(), true, false);
            }
        });
    }
}