//===============================================
//作    者：
//创建时间：2022-04-11 16:24:11
//备    注：
//===============================================
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SelectGameServerController : MonoBehaviour
{
    [SerializeField]
    private SelectGameServerWindow m_SelectGameServerWindow;

    void Start()
    {
        ReqGameServerPage();
    }

    private void ReqGameServerPage()
    {
        GameServerModel.Instance.ReqGameServerPage((UnityWebRequest.Result result, MFReturnValue<List<RetGameServerPageEntity>> ret) => {
            if (result == UnityWebRequest.Result.Success && !ret.HasError)
            {
                m_SelectGameServerWindow.AddGameServerPageItem(ret.Value);
            }
        });
    }

    public void OnClickGameServerPageItem(int pageIndex)
    {
        GameServerModel.Instance.ReqGameServer(pageIndex, (UnityWebRequest.Result result, MFReturnValue<List<RetGameServerEntity>> ret) => {
            if (result == UnityWebRequest.Result.Success && !ret.HasError)
            {
                m_SelectGameServerWindow.UpdateGameServerList(ret.Value);
            }
        });
    }

    void Update()
    {
        
    }
}
