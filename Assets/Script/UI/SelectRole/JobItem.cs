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

    //点击职业项事件
    public event Action<int> ClickJobItem; 
    //职业ID
    private int m_JobId;
    //初始x坐标
    private float m_OriginPosX;
    //目标x坐标
    private float m_TargetPosX;
    //移动时间
    private float duration = 0.5f;

    private void Awake()
    {
        m_OriginPosX = transform.localPosition.x;
        m_TargetPosX = transform.localPosition.x + 100;
    }

    public void Init(int jobId, string jobName, string jobHeadIcon)
    {
        m_JobId = jobId;
        m_JobName.text = jobName;
        m_JobHeadIcon.overrideSprite = Resources.Load<Sprite>(jobHeadIcon);
    }

    public void MoveToRight()
    {
        transform.DOLocalMoveX(m_TargetPosX, duration).SetEase(Ease.OutExpo);
    }

    public void MoveToOrigin()
    {
        transform.DOLocalMoveX(m_OriginPosX, duration).SetEase(Ease.OutExpo);
    }

    public void OnClickBtn()
    {
        ClickJobItem?.Invoke(m_JobId);
    }
}
