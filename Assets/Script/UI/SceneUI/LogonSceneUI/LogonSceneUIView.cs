//===============================================
//作    者：
//创建时间：2022-04-10 18:34:22
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.Networking;

public class LogonSceneUIView : MonoBehaviour
{
    private void Start()
    {
        CheckAccount();
    }

    private void CheckAccount()
    {
        if(PlayerPrefs.HasKey(PlayerPrefsKey.AccountID))
        {
            AccountModel.Instance.QuickLogon((UnityWebRequest.Result result, MFReturnValue<int> ret) => { 
                if(result == UnityWebRequest.Result.Success)
                {
                    if(ret.HasError)
                    {
                        WindowBase.OpenWindowZoomInShow(WindowPath.Logon, transform);
                    }
                    else
                    {
                        WindowBase.OpenWindowZoomInShow(WindowPath.GameServerEnter, transform);
                    }
                }
                else
                {
                    WindowBase.OpenWindowZoomInShow(WindowPath.Logon, transform);
                }
            });
        }
        else
        {
            WindowBase.OpenWindowZoomInShow(WindowPath.Register, transform);
        }
    }
}
