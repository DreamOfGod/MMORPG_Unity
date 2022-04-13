//===============================================
//作    者：
//创建时间：2022-04-10 23:39:46
//备    注：
//===============================================
using System.Threading.Tasks;
using UnityEngine;

public class AccountModel: Singleton<AccountModel>
{
    #region 注册
    public async Task<RequestResult<AccountEntity>> RegisterTaskAsync(string username, string pwd)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        var requestResult = await NetWorkHttp.Instance.PostAsync<AccountEntity>(NetWorkHttp.AccountServerURL + "register", form);
        if (requestResult.IsSuccess && requestResult.ResponseValue.Code == 0)
        {
            Statistics.Register(requestResult.ResponseValue.Value.Id, username);
        }
        return requestResult;
    }
    #endregion

    #region 登录
    public async Task<RequestResult<AccountEntity>> LogonTaskAsync(string username, string pwd)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        var requestResult = await NetWorkHttp.Instance.PostAsync<AccountEntity>(NetWorkHttp.AccountServerURL + "logon", form);
        if (requestResult.IsSuccess)
        {
            var responseValue = requestResult.ResponseValue;
            var entity = responseValue.Value;
            if (responseValue.Code == 0)
            {
                PlayerPrefs.SetInt(PlayerPrefsKey.AccountID, entity.Id);
                PlayerPrefs.SetString(PlayerPrefsKey.Username, entity.Username);
                PlayerPrefs.SetString(PlayerPrefsKey.Password, entity.Pwd);
                Statistics.Logon(entity.Id, entity.Username);
            }
        }
        return requestResult;
    }

    /// <summary>
    /// 快速登录
    /// </summary>
    public async Task<RequestResult<AccountEntity>> QuickLogonTaskAsync()
    {
        string username = PlayerPrefs.GetString(PlayerPrefsKey.Username);
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", PlayerPrefs.GetString(PlayerPrefsKey.Password));
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtil.DeviceModel);

        var requestResult = await NetWorkHttp.Instance.PostAsync<AccountEntity>(NetWorkHttp.AccountServerURL + "register", form);
        if (requestResult.IsSuccess && requestResult.ResponseValue.Code == 0)
        {
            Statistics.Logon(requestResult.ResponseValue.Value.Id, requestResult.ResponseValue.Value.Username);
        }
        return requestResult;
    }
    #endregion
}