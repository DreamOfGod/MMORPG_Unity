//===============================================
//作    者：
//创建时间：2022-03-09 14:54:00
//备    注：
//===============================================
using UnityEngine;

public class UISceneCityCtrl : UISceneBase
{
    [SerializeField]
    private Transform m_HeadBarParent;

    private RoleHeadBarCtrl m_HeadBarCtrl;

    protected override void Awake()
    {
        base.Awake();
    }

    public void InitHeadBar(Transform nicknameTarget, string nickname, bool showHPBar)
    {
        GameObject headBarPrefab = Resources.Load<GameObject>("UIPrefab/UIOther/RoleHeadBar");
        GameObject headBar = Instantiate(headBarPrefab);
        headBar.transform.parent = m_HeadBarParent;
        headBar.transform.localScale = Vector3.one;
        m_HeadBarCtrl = headBar.GetComponent<RoleHeadBarCtrl>();
        m_HeadBarCtrl.SetFollowTarget(nicknameTarget);
        m_HeadBarCtrl.SetNickname(nickname);
        m_HeadBarCtrl.SetHPBarVisible(showHPBar);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            m_HeadBarCtrl.SetHUDText(5);
        }
    }
}
