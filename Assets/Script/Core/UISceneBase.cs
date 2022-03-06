//===============================================
//作    者：
//创建时间：2022-02-24 11:55:06
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 场景UI基类
/// </summary>
public class UISceneBase: UIBase
{
    /// <summary>
    /// 场景UI根节点组件，用于加载窗口时获取容器
    /// </summary>
    public static UISceneBase Instance;

    protected virtual void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 容器_居中
    /// </summary>
    [SerializeField]
    public Transform Container_Center;

    /// <summary>
    /// 容器_左上
    /// </summary>
    [SerializeField]
    public Transform Container_TL;

    /// <summary>
    /// 容器_右上
    /// </summary>
    [SerializeField]
    public Transform Container_TR;

    /// <summary>
    /// 容器_左下
    /// </summary>
    [SerializeField]
    public Transform Container_BL;

    /// <summary>
    /// 容器_右下
    /// </summary>
    [SerializeField]
    public Transform Container_BR;
}
