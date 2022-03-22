//===============================================
//作    者：
//创建时间：2022-03-07 17:11:26
//备    注：
//===============================================
using LitJson;
using System.Collections;
using System.Collections.Generic;
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
        //StartCoroutine(LoadLogon());

        //NetWorkHttp.Instance.Get("http://127.0.0.1:8080/api/account?id=1", (UnityWebRequest.Result result, string text) => {
        //    if(result == UnityWebRequest.Result.Success)
        //    {
        //        AccountEntity entity = LitJson.JsonMapper.ToObject<AccountEntity>(text);
        //        Debug.Log("account name:" + entity.Username);
        //    }
        //});


        //JsonData jsonData = new JsonData();
        //jsonData["Username"] = "test";
        //jsonData["Pwd"] = "123456";
        //NetWorkHttp.Instance.Post("http://127.0.0.1:8080/api/register", jsonData.ToJson(), (UnityWebRequest.Result result, string text) =>
        //{
        //    if (result == UnityWebRequest.Result.Success)
        //    {
        //        Debug.Log("用户id：" + text);
        //    }
        //});

        NetWorkSocket.Instance.Connect("127.0.0.1", 1011);

        EventDispatcher.Instance.AddListener(ProtoCodeDef.Test, TestListener);
    }

    private void TestListener(byte[] buffer)
    {
        TestProto proto = TestProto.GetProto(buffer);
        Debug.Log(string.Format("IsSuccess：{0}", proto.IsSuccess));
        Debug.Log(string.Format("ErrorCode：{0}", proto.ErrorCode));
        for (int i = 0; i < proto.RoleList.Count; ++i)
        {
            Debug.Log(string.Format("Role{0}: {1} {2}", i, proto.RoleList[i].RoleId, proto.RoleList[i].RoleName));
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(ProtoCodeDef.Test, TestListener);
    }

    private IEnumerator LoadLogon()
    {
        yield return new WaitForSeconds(3.5f);
        LoadingSceneCtrl.NextScene = SceneName.Logon;
        SceneManager.LoadScene(SceneName.Loading);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            TestProto proto = new TestProto();
            proto.IsSuccess = true;
            proto.Name = "测试";
            proto.Count = 4;
            proto.RoleList = new List<TestProto.Role>();
            proto.ItemIdList = new List<int>();
            for (int i = 0; i < proto.Count; ++i)
            {
                proto.ItemIdList.Add(i);
                TestProto.Role role = new TestProto.Role();
                role.RoleId = i;
                role.RoleName = "Role " + i;
                proto.RoleList.Add(role);
            }

            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }
}
