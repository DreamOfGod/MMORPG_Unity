//===============================================
//作    者：
//创建时间：2022-04-07 14:36:16
//备    注：
//===============================================

/// <summary>
/// 登录窗口
/// </summary>
public class LogonWindow : WindowBase
{
    public void CloseSelfAndOpenRegister()
    {
        OnWindowCloseFinish = () => { OpenWindowMoveFromLeftToRightShow(WindowPath.Register, transform.parent); };
        MoveFromRightToLeftClose();
    }

    public void CloseSelfAndOpenGameServerEnter()
    {
        OnWindowCloseFinish = () => { OpenWindowMoveFromLeftToRightShow(WindowPath.GameServerEnter, transform.parent); };
        MoveFromRightToLeftClose();
    }

    public void ShowLogonTip(string message)
    {
        MessageWindow.Show(transform.parent, "登录提示", message, true, false);
    }
}
