//===============================================
//作    者：
//创建时间：2022-03-07 17:11:26
//备    注：
//===============================================
using LitJson;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// 初始场景控制器
/// </summary>
public class InitSceneCtrl : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadLogon());

        //NetWorkHttp.Instance.Get("http://127.0.0.1:8080/api/account?id=1", (UnityWebRequest.Result result, string text) => {
        //    if(result == UnityWebRequest.Result.Success)
        //    {
        //        AccountEntity entity = LitJson.JsonMapper.ToObject<AccountEntity>(text);
        //        Debug.Log("account name:" + entity.Username);
        //    }
        //});


        //JsonData jsonData = new JsonData();
        //jsonData["Username"] = "test";
        //NetWorkHttp.Instance.Post("http://127.0.0.1:8080/api/account", jsonData.ToJson(), (UnityWebRequest.Result result, string text) => {
        //    if (result == UnityWebRequest.Result.Success)
        //    {

        //    }
        //});

        NetWorkSocket.Instance.Connect("127.0.0.1", 1011);
    }

    private IEnumerator LoadLogon()
    {
        yield return new WaitForSeconds(3.5f);
        LoadingSceneCtrl.NextScene = SceneName.Logon;
        SceneManager.LoadScene(SceneName.Loading);
    }

    private void Update()
    {
        //if(Input.GetKeyUp(KeyCode.A))
        //{
        //    string content = "A";
        //    MMO_MemoryStream ms = new MMO_MemoryStream();
        //    ms.WriteUTF8String(content);
        //    NetWorkSocket.Instance.SendMsg(ms.ToArray());
        //}
        //else if (Input.GetKeyUp(KeyCode.B))
        //{
        //    string content = "B";
        //    MMO_MemoryStream ms = new MMO_MemoryStream();
        //    ms.WriteUTF8String(content);
        //    NetWorkSocket.Instance.SendMsg(ms.ToArray());
        //}
        //else if (Input.GetKeyUp(KeyCode.C))
        //{
        //    for(int i = 0; i < 10; ++i)
        //    {
        //        MMO_MemoryStream ms = new MMO_MemoryStream();
        //        ms.WriteUTF8String(i.ToString());
        //        NetWorkSocket.Instance.SendMsg(ms.ToArray());
        //    }
        //}
        //else if(Input.GetKeyUp(KeyCode.D))
        //{
        //    NetWorkSocket.Instance.Close();
        //}
    }
}
