//===============================================
//作    者：
//创建时间：2022-02-24 10:37:01
//备    注：
//===============================================
using System.Collections;
using UnityEngine;

public class UISceneLogOnCtrl : UISceneBase
{
    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(OpenLogonWindow());
    }

    private IEnumerator OpenLogonWindow()
    {
        yield return new WaitForSeconds(0.4f);

        GameObject prefab = Resources.Load("UIPrefab/UIWindows/PanLogOn") as GameObject;
        GameObject obj = Instantiate(prefab);

        Transform trans = obj.GetComponent<Transform>();
        trans.parent = Container_Center;
        trans.localPosition = Vector2.zero;

        TweenScale ts = obj.AddComponent<TweenScale>();
        ts.from = Vector2.zero;
        ts.to = Vector2.one;
        ts.duration = 0.2f;
        ts.PlayForward();
    }
}
