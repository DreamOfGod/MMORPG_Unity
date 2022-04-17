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
    public void OnClickSelectGameServer()
    {
        WindowBase window = WindowBase.OpenWindowZoomInShow(WindowPath.SelectGameServer, transform.parent);
        SelectGameServerController selectGameServerController = window.GetComponent<SelectGameServerController>();
        selectGameServerController.Init(m_EnterGameServerWindow);
    }

    public void OnCllickEnterGameServer()
    {

    }


}