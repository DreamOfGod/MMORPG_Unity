//===============================================
//作    者：
//创建时间：2022-03-07 14:35:49
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneCtrl : MonoBehaviour
{
    public static string NextScene;

    [SerializeField]
    private UISceneLoadingCtrl m_UISceneLoadingCtrl;

    private AsyncOperation m_Async;

    private int m_CurrentProgress;

    void Start()
    {
        m_CurrentProgress = 0;
        m_Async = SceneManager.LoadSceneAsync(NextScene);
        m_Async.allowSceneActivation = false;
    }

    void Update()
    {
        if (m_CurrentProgress < 100)
        {
            //异步加载的进度值最大为0.9
            //如果异步加载未完成
            if (m_Async.progress < 0.9f)//0.9必须是float类型
            {
                //如果显示的进度值小于实际进度，显示进度值每帧加一。否则显示的进度值不变
                if (m_CurrentProgress < (int)(m_Async.progress * 100))
                {
                    m_CurrentProgress++;
                }
            }
            else
            {
                //异步加载完成，显示的进度值每帧加一，直到100
                m_CurrentProgress++;
            }

            m_UISceneLoadingCtrl.SetProgressValue(m_CurrentProgress);

            if (m_CurrentProgress == 100)
            {
                m_Async.allowSceneActivation = true;
            }
        }
    }
}