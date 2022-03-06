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
        TweenScale ts = gameObject.GetComponent<TweenScale>();
        ts.from = Vector2.one;
        ts.to = Vector2.zero;
        ts.duration = 0.2f;
        ts.AddOnFinished(() => {
            Destroy(gameObject);
            openRegister();
        });
        ts.PlayForward();
    }

    private void openRegister()
    {
        GameObject prefab = Resources.Load("UIPrefab/UIWindows/PanReg") as GameObject;
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
