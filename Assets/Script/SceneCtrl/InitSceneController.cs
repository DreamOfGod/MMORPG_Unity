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
        ReqServerTimeTaskAsync();
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
        SceneLoadingCtrl.LoadScene(SceneName.Logon);

    }
}
