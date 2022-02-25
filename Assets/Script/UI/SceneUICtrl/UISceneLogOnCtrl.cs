//===============================================
//作    者：
//创建时间：2022-02-24 10:37:01
//备    注：
//===============================================
using UnityEngine;

public class UISceneLogOnCtrl : UISceneBase
{
    protected override void Awake()
    {
        base.Awake();
        WindowUIMgr.Instance.OpenWindow(WindowName.LogOn);
    }
}
