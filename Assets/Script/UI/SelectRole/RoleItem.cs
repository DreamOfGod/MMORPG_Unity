//===============================================
//作    者：
//创建时间：2022-05-25 20:31:59
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.UI;

public class RoleItem : MonoBehaviour
{
    //头像
    [SerializeField]
    private Image m_HeadIcon;
    //昵称
    [SerializeField]
    private Text m_NicknameText;
    //职业
    [SerializeField]
    private Text m_JobText;
    //等级
    [SerializeField]
    private Text m_LevelText;
    //光圈
    [SerializeField]
    private GameObject m_Light;

    //序号
    private int m_Idx;
    //点击执行的委托
    private Action<int> m_OnClick;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="nickname"></param>
    /// <param name="job"></param>
    /// <param name="level"></param>
    public void Init(int idx, string nickname, byte jobId, int level, Action<int> onClick)
    {
        m_Idx = idx;
        m_OnClick = onClick;
        m_NicknameText.text = nickname;
        var jobList = JobConfig.Instance.List;
        for(int i = 0; i < jobList.Count; ++i)
        {
            if(jobList[i].Id == jobId)
            {
                m_JobText.text = jobList[i].Name;
                m_HeadIcon.overrideSprite = Resources.Load<Sprite>($"UI/UISource/HeadImg/{ jobList[i].HeadPic }");
                break;
            }
        }
        m_LevelText.text = $"Lv { level }";
        m_Light.SetActive(false);
    }

    /// <summary>
    /// 设置是否显示光圈
    /// </summary>
    /// <param name="isShow"></param>
    public void SetLightActive(bool isShow)
    {
        m_Light.SetActive(isShow);
    }

    /// <summary>
    /// 点击回调
    /// </summary>
    public void OnClickButton()
    {
        m_OnClick?.Invoke(m_Idx);
    }
}
