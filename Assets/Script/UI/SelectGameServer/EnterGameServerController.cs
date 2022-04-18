//===============================================
//作    者：
//创建时间：2022-04-11 15:14:26
//备    注：
//===============================================
using UnityEngine;

public class EnterGameServerController : MonoBehaviour
{
    [SerializeField]
    private EnterGameServerWindow m_EnterGameServerWindow;

    private GameServerBean m_CurSelectGameServer;
    private bool m_IsReqEnterServer = false;

    public void SetCurSelectGameServer(GameServerBean curSelectGameServer)
    {
        m_CurSelectGameServer = curSelectGameServer;
        m_EnterGameServerWindow.SetCurSelectGameServer(curSelectGameServer.Name);
    }
    public void OnClickSelectGameServer()
    {
        WindowBase window = WindowBase.OpenWindowZoomInShow(WindowPath.SelectGameServer, transform.parent);
        SelectGameServerController selectGameServerController = window.GetComponent<SelectGameServerController>();
        selectGameServerController.Init(this);
    }

    public async void OnCllickEnterGameServer()
    {
        if (m_IsReqEnterServer)
        {
            return;
        }
        m_IsReqEnterServer = true;
        var requestResult  = await GameServerModel.Instance.ReqEnterGameServer(m_CurSelectGameServer);
        if(this == null || gameObject == null)
        {
            return;
        }
        m_IsReqEnterServer = false;
        if(!requestResult.IsSuccess)
        {
            return;
        }
        switch(requestResult.ResponseData.Code)
        {
            case 0:
                MessageWindow.Show(transform.parent, "提示", "Success", true, false);
                break;
            case 1: MessageWindow.Show(transform.parent, "提示", requestResult.ResponseData.Error, true, false); break;
        }
    }
}