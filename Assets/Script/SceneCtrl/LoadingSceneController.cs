//===============================================
//作    者：
//创建时间：2022-03-07 14:35:49
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    /// <summary>
    /// 是否从AssetBundle加载场景
    /// </summary>
    private static bool m_IsLoadSceneFromAssetBundle;
    /// <summary>
    /// AssetBundle路径
    /// </summary>
    private static string m_AssetBundlePath;
    /// <summary>
    /// 加载的场景
    /// </summary>
    private static string m_NextScene;

    [SerializeField]
    private UISceneLoadingCtrl m_UISceneLoadingCtrl;

    private AsyncOperation m_AsyncOperation;

    private int m_CurrentProgress;

    /// <summary>
    /// 从Resources中加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        m_IsLoadSceneFromAssetBundle = false;
        m_NextScene = sceneName;
        SceneManager.LoadScene(SceneName.Loading);
    }

    /// <summary>
    /// 从AssetBundle从加载场景
    /// </summary>
    /// <param name="assetBundlePath"></param>
    /// <param name="sceneName"></param>
    public static void LoadSceneFromAssetBundle(string assetBundlePath, string sceneName)
    {
        m_IsLoadSceneFromAssetBundle = true;
        m_AssetBundlePath = assetBundlePath;
        m_NextScene = sceneName;
        SceneManager.LoadScene(SceneName.Loading);
    }

    async void Start()
    {
        m_CurrentProgress = 0;
        if (m_IsLoadSceneFromAssetBundle)
        {
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(m_AssetBundlePath);
            await request;
            m_AsyncOperation = SceneManager.LoadSceneAsync(m_NextScene);
            m_AsyncOperation.allowSceneActivation = false;
            await m_AsyncOperation;
            request.assetBundle.Unload(false);
        }
        else
        {
            m_AsyncOperation = SceneManager.LoadSceneAsync(m_NextScene);
            m_AsyncOperation.allowSceneActivation = false;
        }
    }

    void Update()
    {
        if(m_AsyncOperation == null)
        {
            return;
        }
        if (m_CurrentProgress < 100)
        {
            //异步加载的进度值最大为0.9
            //如果异步加载未完成
            if (m_AsyncOperation.progress < 0.9f)//0.9必须是float类型
            {
                //如果显示的进度值小于实际进度，显示进度值每帧加一。否则显示的进度值不变
                if (m_CurrentProgress < (int)(m_AsyncOperation.progress * 100))
                {
                    ++m_CurrentProgress;
                    m_UISceneLoadingCtrl.SetProgressValue(m_CurrentProgress);
                    if (m_CurrentProgress == 100)
                    {
                        m_AsyncOperation.allowSceneActivation = true;
                    }
                }
            }
            else
            {
                //异步加载完成，显示的进度值每帧加一，直到100
                ++m_CurrentProgress;
                m_UISceneLoadingCtrl.SetProgressValue(m_CurrentProgress);
                if (m_CurrentProgress == 100)
                {
                    m_AsyncOperation.allowSceneActivation = true;
                }
            }
        }
    }
}