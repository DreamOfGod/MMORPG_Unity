//===============================================
//作    者：
//创建时间：2022-05-16 21:52:03
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//选择角色场景的UI视图
public class SelectRoleSceneUIView : MonoBehaviour
{
    //职业列表
    [SerializeField]
    private JobItem[] m_JobItemList;
    //选择的职业名称
    private Text m_SelectJobName;
    //选择职业描述
    private Text m_SelectJobDesc;

    public void InitJobItem(int jobId, string jobName, string jobHeadIcon)
    {
        var jobItem = m_JobItemList[jobId - 1];
        jobItem.Init(jobId, jobName, jobHeadIcon);
        jobItem.ClickJobItem += OnClickJobItem;
    }

    private void OnClickJobItem(int jobId)
    {

    }

    public void MoveToRight(int jobId)
    {
        m_JobItemList[jobId - 1].MoveToRight();
    }

    public void MoveToOrigin(int jobId)
    {
        m_JobItemList[jobId - 1].MoveToOrigin();
    }

    public void OnClickReturnBtn()
    {

    }

    public void OnClickStartBtn()
    {

    }
}
