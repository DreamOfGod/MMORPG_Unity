//===============================================
//作    者：
//创建时间：2022-04-08 14:21:11
//备    注：
//===============================================

using System;
/// <summary>
/// 注册窗口
/// </summary>
public class ReigsterWindow : WindowBase
{
    public void ShowRegisterTip(string message, Action onClickOK = null)
    {
        MessageWindow.Show(transform.parent, "注册提示", message, true, false, onClickOK: onClickOK);
    }

    public void CloseSelfAndOpenLogon()
    {
        OnWindowCloseFinish = () => { OpenWindowMoveFromLeftToRightShow(WindowPath.Logon, transform.parent); };
        MoveFromRightToLeftClose();
    }
}