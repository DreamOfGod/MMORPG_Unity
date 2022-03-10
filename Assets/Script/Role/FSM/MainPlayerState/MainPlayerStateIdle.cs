//===============================================
//作    者：
//创建时间：2022-03-10 11:17:24
//备    注：
//===============================================
using UnityEngine;

public class MainPlayerStateIdle : StateBase
{
    public MainPlayerStateIdle(MainPlayerCtrl mainPlayerCtrl) : base(mainPlayerCtrl) { }

    public override void OnEnter()
    {
        m_RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, true);

        FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClickGround;
    }

    public override void OnLeave()
    {
        m_RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, false);

        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClickGround;
    }

    void OnPlayerClickGround(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //点击地面
            if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
            {
                if (Vector3.Distance(hitInfo.point, m_RoleCtrl.transform.position) > 0.1f)
                {
                    m_RoleCtrl.TargetPos = hitInfo.point;
                    ((MainPlayerCtrl)m_RoleCtrl).ChangeToRunState();
                }
            }
        }
    }
}
