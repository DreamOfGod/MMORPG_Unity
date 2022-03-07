//===============================================
//作    者：
//创建时间：2022-02-24 15:30:28
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口UI基类
/// </summary>
public class UIWindowBase : UIBase
{
    /// <summary>
    /// 容器类型
    /// </summary>
    [SerializeField]
    public WindowUIContainerType containerType = WindowUIContainerType.Center;

    /// <summary>
    /// 打开方式
    /// </summary>
    [SerializeField]
    public WindowShowStyle showStyle = WindowShowStyle.Normal;

    /// <summary>
    /// 打开或关闭动画效果持续事件
    /// </summary>
    [SerializeField]
    public float duration = 1f;
}