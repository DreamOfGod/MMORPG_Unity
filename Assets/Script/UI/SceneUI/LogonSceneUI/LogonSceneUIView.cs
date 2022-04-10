//===============================================
//作    者：
//创建时间：2022-04-10 18:34:22
//备    注：
//===============================================
using System.Collections;
using UnityEngine;

public class LogonSceneUIView : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(OpenLogonView());
    }

    private IEnumerator OpenLogonView()
    {
        yield return new WaitForSeconds(0.5f);
        WindowBase.OpenWindowZoomInShow(WindowPath.Logon, transform);
    }
}
