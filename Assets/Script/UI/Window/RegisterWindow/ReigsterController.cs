//===============================================
//作    者：
//创建时间：2022-04-08 16:20:58
//备    注：
//===============================================
using LitJson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 注册视图控制器
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
        m_ReigsterView.OnWindowCloseFinish = () => { WindowBase.OpenWindowMoveFromLeftToRightShow(WindowPath.Logon, transform.parent); };
        m_ReigsterView.MoveFromRightToLeftClose();
    }

    public void Register()
    {
        string username = m_InputUsername.text;
        if (string.IsNullOrEmpty(username))
        {
            MessageWindow.Show(transform.parent, "注册提示", "请输入账号", true, false);
            return;
        }
        string pwd = m_InputPwd.text;
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
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Username"] = username;
        dic["Pwd"] = pwd;
        dic["ChannelId"] = 0;
        dic["DeviceModel"] = DeviceUtil.DeviceModel;

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "api/Register", dic, (UnityWebRequest.Result result, string text) => {
            if(this == null || gameObject == null)
            {
                return;
            }
            m_IsRegistering = false;
            if (result == UnityWebRequest.Result.Success)
            {
                MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
                if(ret.HasError)
                {
                    Debug.LogError(ret.Message);
                    MessageWindow.Show(transform.parent, "注册提示", ret.Message, true, false);
                }
                else
                {
                    DebugLogger.LogFormat("注册成功，账号ID：{0}", ret.Value);
                    Statistics.Register(ret.Value, username);
                    MessageWindow.Show(transform.parent, "注册提示", "注册成功", true, false);
                }
            }
            else
            {
                MessageWindow.Show(transform.parent, "注册提示", result.ToString(), true, false);
            }
        });
    }
}