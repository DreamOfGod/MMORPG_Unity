//===============================================
//作    者：
//创建时间：2022-03-08 13:57:22
//备    注：
//===============================================

using UnityEngine;
/// <summary>
/// 待机状态
/// </summary>
public class RoleStateIdle : RoleStateAbstract
{
    public RoleStateIdle(RoleFSM roleFSM) : base(roleFSM) { }

    public override void OnEnter()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleNormal, true);

        FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClickGround;

        Debug.Log("enter idle state");
    }

    public override void OnUpdate()
    {
    }

    public override void OnLeave()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleNormal, false);

        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClickGround;

        Debug.Log("leave idle state");
    }

    #region OnPlayerClickGround 玩家点击地面
    /// <summary>
    /// 玩家点击地面
    /// </summary>
    /// <param name="screenPos"></param>
    void OnPlayerClickGround(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //点击地面
            if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
            {
                if (Vector3.Distance(hitInfo.point, m_RoleFSM.RoleCtrl.transform.position) > 0.1f)
                {
                    m_RoleFSM.RoleCtrl.TargetPos = hitInfo.point;
                    m_RoleFSM.ChangeToRunState();
                }
            }
        }
    }
    #endregion
}
