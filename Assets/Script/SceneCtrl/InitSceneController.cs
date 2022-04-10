//===============================================
//作    者：
//创建时间：2022-03-07 17:11:26
//备    注：
//===============================================
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
        ReqServerTime();
    }

    void ReqServerTime()
    {
        ++m_ReqServerTimeCnt;
        ServerTimeUtil.Instance.ReqServerTime(ReqServerTimeCallback);
    }

    void ReqServerTimeCallback(UnityWebRequest.Result result)
    {
        if (result == UnityWebRequest.Result.Success)
        {
            StartCoroutine(LoadLogonScene());
        }
        else if (m_ReqServerTimeCnt < 5)
        {
            ReqServerTime();
        }
        else
        {
            Debug.LogErrorFormat("请求服务器时间失败{0}次", m_ReqServerTimeCnt);
            m_ReqServerTimeCnt = 0;
            m_InitSceneUIView.ShowNetErrorMsg(ReqServerTime);
        }
    }

    private IEnumerator LoadLogonScene()
    {
        yield return new WaitForSeconds(2f);
        SceneLoadingCtrl.LoadScene(SceneName.Logon);

    }
}
