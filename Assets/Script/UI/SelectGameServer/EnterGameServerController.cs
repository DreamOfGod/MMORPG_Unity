//===============================================
//作    者：
//创建时间：2022-04-11 15:14:26
//备    注：
//===============================================
using UnityEngine;

public class EnterGameServerController : MonoBehaviour
{
    public void SelectGameServer()
    {
        WindowBase.OpenWindowZoomInShow(WindowPath.SelectGameServer, transform.parent);
    }

    public void EnterGameServer()
    {

    }
}
