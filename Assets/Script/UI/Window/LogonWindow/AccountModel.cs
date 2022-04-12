//===============================================
//作    者：
//创建时间：2022-04-10 23:39:46
//备    注：
//===============================================
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

public class AccountModel: Singleton<AccountModel>
{
    #region 注册
    private struct RegisterCallbackData
    {
        public string Username;
        public ModelCallback<int> Callback;
    }

    public void Register(string username, string pwd, ModelCallback<int> callback = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "register", form, RegisterCallback, new RegisterCallbackData() { Username = username, Callback = callback});
    }

    private void RegisterCallback(UnityWebRequest.Result result, object callbackData, string text)
    {
        RegisterCallbackData registerCallbackData = (RegisterCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
            if (!ret.HasError)
            {
                Statistics.Register(ret.Value, registerCallbackData.Username);
            }
            if (registerCallbackData.Callback != null)
            {
                registerCallbackData.Callback(result, ret);
            }
        }
        else
        {
            if (registerCallbackData.Callback != null)
            {
                registerCallbackData.Callback(result);
            }
        }
    }
    #endregion

    #region 登录
    private struct LogonCallbackData
    {
        public string Username;
        public string Pwd;
        public ModelCallback<int> Callback;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="username"></param>
    /// <param name="pwd"></param>
    /// <param name="callback"></param>
    public void Logon(string username, string pwd, ModelCallback<int> callback = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "logon", form, LogonCallback, new LogonCallbackData() { Username = username, Pwd = pwd, Callback = callback });
    }

    private void LogonCallback(UnityWebRequest.Result result, object callbackData, string text)
    {
        LogonCallbackData logonCallbackData = (LogonCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
            if (!ret.HasError)
            {
                PlayerPrefs.SetInt(PlayerPrefsKey.AccountID, ret.Value);
                PlayerPrefs.SetString(PlayerPrefsKey.Username, logonCallbackData.Username);
                PlayerPrefs.SetString(PlayerPrefsKey.Password, logonCallbackData.Pwd);
                Statistics.Logon(ret.Value, logonCallbackData.Username);
            }
            if (logonCallbackData.Callback != null)
            {
                logonCallbackData.Callback(result, ret);
            }
        }
        else
        {
            if (logonCallbackData.Callback != null)
            {
                logonCallbackData.Callback(result);
            }
        }
    }

    /// <summary>
    /// 快速登录
    /// </summary>
    public void QuickLogon(ModelCallback<int> callback = null)
    {
        string username = PlayerPrefs.GetString(PlayerPrefsKey.Username);
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", PlayerPrefs.GetString(PlayerPrefsKey.Password));
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "logon", form, QuickLogonCallback, new LogonCallbackData() { Username = username, Callback = callback });
    }

    private void QuickLogonCallback(UnityWebRequest.Result result, object callbackData, string text)
    {
        LogonCallbackData logonCallbackData = (LogonCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
            if (!ret.HasError)
            {
                Statistics.Logon(ret.Value, logonCallbackData.Username);
            }
            if (logonCallbackData.Callback != null)
            {
                logonCallbackData.Callback(result, ret);
            }
        }
        else if (logonCallbackData.Callback != null)
        {
            logonCallbackData.Callback(result);
        }
    }
    #endregion
}