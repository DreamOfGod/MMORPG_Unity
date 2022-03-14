//===============================================
//作    者：
//创建时间：2022-03-09 14:54:00
//备    注：
//===============================================
using UnityEngine;

public class UISceneCityCtrl : UISceneBase
{
    /// <summary>
    /// 角色头顶UI条容器
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarParent;

    /// <summary>
    /// 主角昵称
    /// </summary>
    [SerializeField]
    private UILabel m_LblNickname;

    /// <summary>
    /// 主角血条
    /// </summary>
    [SerializeField]
    private UISprite m_SprHP;

    protected override void Start()
    {
        m_LblNickname.text = UserInfo.nickname;
    }

    /// <summary>
    /// 添加角色头顶UI条
    /// </summary>
    /// <param name="followTarget">跟随目标</param>
    /// <param name="nickname">昵称</param>
    /// <param name="showHPBar">是否显示血条</param>
    /// <returns>头顶UI条控制器</returns>
    public RoleHeadBarCtrl AddHeadBar(Transform followTarget, string nickname, bool showHPBar)
    {
        GameObject headBarPrefab = Resources.Load<GameObject>("UIPrefab/UIOther/RoleHeadBar");
        GameObject headBar = Instantiate(headBarPrefab);
        headBar.transform.parent = m_HeadBarParent;
        headBar.transform.localScale = Vector3.one;
        RoleHeadBarCtrl headBarCtrl = headBar.GetComponent<RoleHeadBarCtrl>();
        headBarCtrl.SetFollowTarget(followTarget);
        headBarCtrl.SetNickname(nickname);
        headBarCtrl.SetHPBarVisible(showHPBar);
        return headBarCtrl;
    }

    /// <summary>
    /// 设置主角血条的百分比
    /// </summary>
    /// <param name="percent"></param>
    public void SetMainPlayerHPBar(float percent)
    {
        m_SprHP.fillAmount = percent;
    }
}
