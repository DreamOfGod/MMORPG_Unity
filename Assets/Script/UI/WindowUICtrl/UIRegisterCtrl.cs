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
        TweenScale ts = gameObject.GetComponent<TweenScale>();
        ts.from = Vector2.one;
        ts.to = Vector2.zero;
        ts.duration = 0.2f;
        ts.AddOnFinished(() => {
            Destroy(gameObject);
            openLogon();
        });
        ts.PlayForward();
    }

    private void openLogon()
    {
        GameObject prefab = Resources.Load("UIPrefab/UIWindows/PanLogOn") as GameObject;
        GameObject obj = Instantiate(prefab);

        Transform trans = obj.GetComponent<Transform>();
        trans.parent = gameObject.GetComponent<Transform>().parent;
        trans.localPosition = Vector2.zero;

        TweenScale ts = obj.AddComponent<TweenScale>();
        ts.from = Vector2.zero;
        ts.to = Vector2.one;
        ts.duration = 0.2f;
        ts.PlayForward();
    }
}
