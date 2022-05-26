//===============================================
//作    者：
//创建时间：2022-03-07 17:11:26
//备    注：
//===============================================
using System.Collections;
using UnityEngine;

/// <summary>
/// 初始场景控制器
/// </summary>
public class InitSceneController : MonoBehaviour
{
    [SerializeField]
    private InitSceneUIView m_InitSceneUIView;

    private int m_ReqServerTimeCnt = 0;

    void Start()
    {
        InitAllModel();
        ReqServerTimeTaskAsync();
        //SocketHelper.Instance.ConnectSucceed += () => 
        //{
        //    DebugLogger.Log("连接成功111111111111");
        //};
        //SocketHelper.Instance.ConnectFailed += () =>
        //{
        //    DebugLogger.Log("连接失败111111111111");
        //};
        //SocketHelper.Instance.ConnectionExceptionOccured += () =>
        //{
        //    DebugLogger.Log("连接发生异常111111111111");
        //};
        //SocketHelper.Instance.BeginConnect("192.168.1.4", 1011);
    }

    private void InitAllModel()
    {
        AccountModel.Instance.Init();
        GameServerModel.Instance.Init();
    }

    async void ReqServerTimeTaskAsync()
    {
        ++m_ReqServerTimeCnt;
        var requestResult = await TimeModel.Instance.ReqServerTime();
        if(requestResult.IsSuccess)
        {
            StartCoroutine(LoadLogonScene());
        }
        else if (m_ReqServerTimeCnt < 5)
        {
            ReqServerTimeTaskAsync();
        }
        else
        {
            Debug.LogErrorFormat("请求服务器时间失败{0}次", m_ReqServerTimeCnt);
            m_ReqServerTimeCnt = 0;
            m_InitSceneUIView.ShowNetErrorMsg(ReqServerTimeTaskAsync);
        }
    }

    private IEnumerator LoadLogonScene()
    {
        yield return new WaitForSeconds(2f);
        LoadingSceneController.LoadSceneFromAssetBundle(AssetBundlePath.LogonScene, SceneName.Logon);
    }
}
