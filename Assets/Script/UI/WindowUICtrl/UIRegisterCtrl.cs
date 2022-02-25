//===============================================
//作    者：
//创建时间：2022-02-24 11:00:18
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRegisterCtrl : UIWindowBase
{
    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnReg":
                btnReg();
                break;
            case "btnToLogOn":
                btnToLogOn();
                break;
        }
    }

    private void btnReg()
    {
    
    }

    private void btnToLogOn()
    {
        WindowUIMgr.Instance.CloseWindow(WindowName.Register);
        WindowUIMgr.Instance.OpenWindow(WindowName.LogOn);
    }
}
