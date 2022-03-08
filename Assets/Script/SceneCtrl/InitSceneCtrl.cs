//===============================================
//作    者：
//创建时间：2022-03-07 17:11:26
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
