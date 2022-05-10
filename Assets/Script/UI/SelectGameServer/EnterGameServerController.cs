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
    public GameServerBean CurSelectGameServer { get => m_CurSelectGameServer; }
    private bool m_IsOpenSelectGameServerWindow = false;
    private bool m_IsReqEnterServer = false;
    private int m_TryConnectServerCount = 0;

    private void Start()
    {
        SocketHelper.Instance.ConnectSucceed += OnConnectSucceed;
        SocketHelper.Instance.ConnectFailed += OnConnectFailed;
    }

    private void OnDestroy()
    {
        SocketHelper.Instance.ConnectSucceed -= OnConnectSucceed;
        SocketHelper.Instance.ConnectFailed -= OnConnectFailed;
    }

    public void SetCurSelectGameServer(GameServerBean curSelectGameServer)
    {
        m_CurSelectGameServer = curSelectGameServer;
        m_EnterGameServerWindow.SetCurSelectGameServer(curSelectGameServer.Name);
    }
    public void OnClickSelectGameServer()
    {
        if(m_IsOpenSelectGameServerWindow)
        {
            return;
        }
        m_IsOpenSelectGameServerWindow = true;
        GameObject obj = WindowBase.InstantiateWindow(WindowPath.SelectGameServer, transform.parent);
        SelectGameServerWindow selectGameServerWindow = obj.GetComponent<SelectGameServerWindow>();
        selectGameServerWindow.OnWindowShowFinish = () =>
        {
            m_IsOpenSelectGameServerWindow = false;
        };
        selectGameServerWindow.ZoomInShow();
        SelectGameServerController selectGameServerController = obj.GetComponent<SelectGameServerController>();
        selectGameServerController.Init(this);
    }

    public async void OnCllickEnterGameServer()
    {
        if (m_IsReqEnterServer || m_TryConnectServerCount > 0)
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
        if (!requestResult.IsSuccess || requestResult.ResponseData.Code != 0)
        {
            return;
        }
        ConnectGameServer();
    }

    //连接区服成功回调
    private void OnConnectSucceed()
    {
        LoadingSceneController.LoadSceneFromAssetBundle(AssetBundlePath.SelectRoleScene, SceneName.SelectRole);
    }

    private void OnConnectFailed()
    {
        if(m_TryConnectServerCount < 5)
        {
            ConnectGameServer();
        }
        else
        {
            m_TryConnectServerCount = 0;
            MessageWindow.Show(transform, "提示", "连接游戏服务器失败，请重试", true, false);
        }
    }

    //连接区服失败回调
    private void ConnectGameServer()
    {
        ++m_TryConnectServerCount;
        SocketHelper.Instance.BeginConnect(m_CurSelectGameServer.Ip, m_CurSelectGameServer.Port);
    }
}