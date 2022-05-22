//===============================================
//作    者：
//创建时间：2022-04-10 23:39:46
//备    注：
//===============================================
using System.Threading.Tasks;
using UnityEngine;

public class AccountModel: IModel
{
    #region 单例
    private AccountModel() { }
    private static AccountModel m_Instance;
    public static AccountModel Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new AccountModel();
            }
            return m_Instance;
        }
    }
    #endregion

    private AccountBean m_CurAccount;

    public GameServerBean LastLogonServer
    {
        get => m_CurAccount.LastLogonGameServer;
        set { m_CurAccount.LastLogonGameServer = value; }
    }

    public int AccountID { get => m_CurAccount.Id; }

    public void Init() { }

    public void Reset() { }

    #region 注册
    public async Task<RequestResult<int>> RegisterAsync(string username, string pwd)
    {
        var form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtility.DeviceModel);

        var requestResult = await HttpHelper.Instance.PostAsync<int>($"{ HttpHelper.AccountServerURL }register", form);
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            var id = requestResult.ResponseData.Data;
            Statistics.Register(id, username);
        }
        return requestResult;
    }
    #endregion

    #region 登录
    public async Task<RequestResult<AccountBean>> LogonAsync(string username, string pwd)
    {
        var form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", pwd);
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtility.DeviceModel);

        var requestResult = await HttpHelper.Instance.PostAsync<AccountBean>($"{ HttpHelper.AccountServerURL }logon", form);
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            m_CurAccount = requestResult.ResponseData.Data;
            PlayerPrefs.SetInt(PlayerPrefsKey.AccountID, m_CurAccount.Id);
            PlayerPrefs.SetString(PlayerPrefsKey.Username, username);
            PlayerPrefs.SetString(PlayerPrefsKey.Password, pwd);
            Statistics.Logon(m_CurAccount.Id, username);
        }
        return requestResult;
    }

    /// <summary>
    /// 快速登录
    /// </summary>
    public async Task<RequestResult<AccountBean>> QuickLogonAsync()
    {
        var username = PlayerPrefs.GetString(PlayerPrefsKey.Username);
        var form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("Pwd", PlayerPrefs.GetString(PlayerPrefsKey.Password));
        form.AddField("ChannelId", "0");
        form.AddField("DeviceModel", DeviceUtility.DeviceModel);

        var requestResult = await HttpHelper.Instance.PostAsync<AccountBean>($"{ HttpHelper.AccountServerURL }logon", form);
        if (requestResult.IsSuccess && requestResult.ResponseData.Code == 0)
        {
            m_CurAccount = requestResult.ResponseData.Data;
            Statistics.Logon(m_CurAccount.Id, username);
        }
        return requestResult;
    }
    #endregion
}