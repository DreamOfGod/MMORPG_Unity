//===============================================
//作    者：
//创建时间：2022-05-16 21:55:13
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JobItem : MonoBehaviour
{
    //职业名称
    [SerializeField]
    private Text m_JobName;
    //职业头像
    [SerializeField]
    private Image m_JobHeadIcon;
    //选中的颜色
    [SerializeField]
    private Color m_SelectColor;

    //点击执行的委托
    private Action<int> m_ClickFunc; 
    //序号
    private int m_Idx;
    //初始x坐标
    private float m_OriginPosX;
    //目标x坐标
    private float m_TargetPosX;
    //移动时间
    private const float duration = 0.5f;
    //tweener
    private Tweener m_Tweener;

    private void Start()
    {
        m_OriginPosX = transform.localPosition.x;
        m_TargetPosX = transform.localPosition.x - 100;
    }

    public void Init(int idx, string jobName, string headIcon, Action<int> clickFunc)
    {
        m_Idx = idx;
        m_JobName.text = jobName;
        m_JobName.color = Color.white;
        m_ClickFunc = clickFunc;
        m_JobHeadIcon.overrideSprite = Resources.Load<Sprite>(headIcon);
    }

    /// <summary>
    /// 放到左边
    /// </summary>
    public void PlaceToLeft() 
    {
        m_JobName.color = m_SelectColor;
        transform.localPosition = new Vector3(m_TargetPosX, transform.localPosition.y, 0);
    }

    /// <summary>
    /// 移动到左边
    /// </summary>
    public void MoveToLeft()
    {
        m_JobName.color = m_SelectColor;
        m_Tweener?.Kill();
        m_Tweener = transform.DOLocalMoveX(m_TargetPosX, duration).SetEase(Ease.OutExpo);
    }

    /// <summary>
    /// 移动回原位
    /// </summary>
    public void MoveToOrigin()
    {
        m_JobName.color = Color.white;
        m_Tweener?.Kill();
        m_Tweener = transform.DOLocalMoveX(m_OriginPosX, duration).SetEase(Ease.OutExpo);
    }

    //点击回调
    public void OnClickBtn()
    {
        m_ClickFunc?.Invoke(m_Idx);
    }
}
