//===============================================
//作    者：
//创建时间：2022-02-24 10:59:37
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登陆窗口UI控制器
/// </summary>
public class UILogOnCtrl : UIWindowBase
{
    protected override void OnBtnClick(GameObject go)
    {
        switch(go.name)
        {
            case "btnLogOn":
                btnLogOn();
                break;
            case "btnToReg":
                btnToReg();
                break;
        }
    }

    private void btnLogOn()
    {

    }

    private void btnToReg()
    {
        WindowUIMgr.Instance.CloseWindow(WindowName.LogOn);
        WindowUIMgr.Instance.OpenWindow(WindowName.Register);
    }
}
