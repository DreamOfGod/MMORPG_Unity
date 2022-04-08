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
        StartCoroutine(OpenLogonView());
    }

    private IEnumerator OpenLogonView()
    {
        yield return new WaitForSeconds(1);
        ViewBase.ZoomInShowView(ViewPath.Logon, m_WindowParent);
    }
}