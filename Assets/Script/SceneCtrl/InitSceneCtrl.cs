//===============================================
//作    者：
//创建时间：2022-03-07 17:11:26
//备    注：
//===============================================
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 初始场景控制器
/// </summary>
public class InitSceneCtrl : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadLogon());
    }

    private IEnumerator LoadLogon()
    {
        yield return new WaitForSeconds(3.5f);
        LoadingSceneCtrl.NextScene = SceneName.Logon;
        SceneManager.LoadScene(SceneName.Loading);
        
    }
}
