//===============================================
//作    者：
//创建时间：2022-02-23 16:44:48
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnSceneCtrl : MonoBehaviour
{
    [SerializeField]
    private Transform m_WindowParent;

    private void Start()
    {
        GameObject prefab = Resources.Load<GameObject>("UIPrefab/UIView/LogonView");
        GameObject win = Instantiate(prefab);
        win.transform.SetParent(m_WindowParent, false);
        win.transform.localPosition = Vector3.zero;
        LogonView view = win.GetComponent<LogonView>();
        view.ViewDuration = 1;
        view.ViewEase = DG.Tweening.Ease.InCubic;
        view.ZoomInShow();
    }
}