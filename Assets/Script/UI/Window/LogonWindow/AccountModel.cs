//===============================================
//作    者：
//创建时间：2022-04-10 23:39:46
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.Networking;

public class AccountModel: Singleton<AccountModel>
{
    #region 注册
    private struct RegisterCallbackData
    {
        public string Username;
        public ModelCallback<AccountEntity> Callback;
    }

    public void Register(string username, string pwd, ModelCallback<AccountEntity> callback = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        NetWorkHttp.Instance.Post<AccountEntity>(NetWorkHttp.AccountServerURL + "register", form, RegisterCallback, new RegisterCallbackData() { Username = username, Callback = callback});
    }

    private void RegisterCallback(UnityWebRequest.Result result, object callbackData, ResponseValue<AccountEntity> responseValue)
    {
        RegisterCallbackData registerCallbackData = (RegisterCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            if (responseValue.Code == 0)
            {
                Statistics.Register(responseValue.Value.Id, registerCallbackData.Username);
            }
            if (registerCallbackData.Callback != null)
            {
                registerCallbackData.Callback(result, responseValue);
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
        public ModelCallback<AccountEntity> Callback;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="username"></param>
    /// <param name="pwd"></param>
    /// <param name="callback"></param>
    public void Logon(string username, string pwd, ModelCallback<AccountEntity> callback = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        NetWorkHttp.Instance.Post<AccountEntity>(NetWorkHttp.AccountServerURL + "logon", form, LogonCallback, new LogonCallbackData() { Username = username, Pwd = pwd, Callback = callback });
    }

    private void LogonCallback(UnityWebRequest.Result result, object callbackData, ResponseValue<AccountEntity> responseValue)
    {
        LogonCallbackData logonCallbackData = (LogonCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            if (responseValue.Code == 0)
            {
                PlayerPrefs.SetInt(PlayerPrefsKey.AccountID, responseValue.Value.Id);
                PlayerPrefs.SetString(PlayerPrefsKey.Username, logonCallbackData.Username);
                PlayerPrefs.SetString(PlayerPrefsKey.Password, logonCallbackData.Pwd);
                Statistics.Logon(responseValue.Value.Id, logonCallbackData.Username);
            }
            if (logonCallbackData.Callback != null)
            {
                logonCallbackData.Callback(result, responseValue);
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
    public void QuickLogon(ModelCallback<AccountEntity> callback = null)
    {
        string username = PlayerPrefs.GetString(PlayerPrefsKey.Username);
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", PlayerPrefs.GetString(PlayerPrefsKey.Password));
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        NetWorkHttp.Instance.Post<AccountEntity>(NetWorkHttp.AccountServerURL + "logon", form, QuickLogonCallback, new LogonCallbackData() { Username = username, Callback = callback });
    }

    private void QuickLogonCallback(UnityWebRequest.Result result, object callbackData, ResponseValue<AccountEntity> responseValue)
    {
        LogonCallbackData logonCallbackData = (LogonCallbackData)callbackData;
        if (result == UnityWebRequest.Result.Success)
        {
            if (responseValue.Code == 0)
            {
                Statistics.Logon(responseValue.Value.Id, logonCallbackData.Username);
            }
            if (logonCallbackData.Callback != null)
            {
                logonCallbackData.Callback(result, responseValue);
            }
        }
        else if (logonCallbackData.Callback != null)
        {
            logonCallbackData.Callback(result);
        }
    }
    #endregion
}