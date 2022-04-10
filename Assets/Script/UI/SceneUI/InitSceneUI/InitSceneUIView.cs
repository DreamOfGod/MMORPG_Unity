//===============================================
//作    者：
//创建时间：2022-04-10 18:03:51
//备    注：
//===============================================
using System;
using UnityEngine;

public class InitSceneUIView : MonoBehaviour
{
    public void ShowNetErrorMsg(Action OnClickOK)
    {
        MessageWindow.Show(transform, "网络提示", "网络错误，点击重试", true, false, OnClickOK);
    }
}
