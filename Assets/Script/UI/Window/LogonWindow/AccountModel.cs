//===============================================
//作    者：
//创建时间：2022-04-10 23:39:46
//备    注：
//===============================================
using LitJson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AccountModel: Singleton<AccountModel>
{
    public delegate void Callback(UnityWebRequest.Result result, MFReturnValue<int> ret = null);

    /// <summary>
    /// 注册
    /// </summary>
    public void Register(string username, string pwd, Callback callback = null)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Username"] = username;
        dic["Pwd"] = pwd;
        dic["ChannelId"] = 0;
        dic["DeviceModel"] = DeviceUtil.DeviceModel;

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "api/Register", dic, (UnityWebRequest.Result result, string text) => {
            if (result == UnityWebRequest.Result.Success)
            {
                MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
                if (!ret.HasError)
                {
                    Statistics.Register(ret.Value, username);
                }
                if(callback != null)
                {
                    callback(result, ret);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback(result);
                }
            }
        });
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="username"></param>
    /// <param name="pwd"></param>
    /// <param name="callback"></param>
    public void Logon(string username, string pwd, Callback callback = null)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Username"] = username;
        dic["Pwd"] = pwd;
        dic["ChannelId"] = 0;
        dic["DeviceModel"] = DeviceUtil.DeviceModel;

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "api/Logon", dic, (UnityWebRequest.Result result, string text) => {
            if (result == UnityWebRequest.Result.Success)
            {
                MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
                if (!ret.HasError)
                {
                    PlayerPrefs.SetInt(PlayerPrefsKey.AccountID, ret.Value);
                    PlayerPrefs.SetString(PlayerPrefsKey.Username, username);
                    PlayerPrefs.SetString(PlayerPrefsKey.Password, pwd);
                    Statistics.Logon(ret.Value, username);
                }
                if(callback != null)
                {
                    callback(result, ret);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback(result);
                }
            }
        });
    }

    /// <summary>
    /// 快速登录
    /// </summary>
    public void QuickLogon(Callback callback = null)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        string username = PlayerPrefs.GetString(PlayerPrefsKey.Username);
        dic["Username"] = username;
        dic["Pwd"] = PlayerPrefs.GetString(PlayerPrefsKey.Password);
        dic["ChannelId"] = 0;
        dic["DeviceModel"] = DeviceUtil.DeviceModel;

        NetWorkHttp.Instance.Post(NetWorkHttp.AccountServerURL + "api/Logon", dic, (UnityWebRequest.Result result, string text) => {
            if (result == UnityWebRequest.Result.Success)
            {
                MFReturnValue<int> ret = JsonMapper.ToObject<MFReturnValue<int>>(text);
                if (!ret.HasError)
                {
                    Statistics.Logon(ret.Value, username);
                }
                if (callback != null)
                {
                    callback(result, ret);
                }
            }
            else if (callback != null)
            {
                callback(result);
            }
        });
    }
}
