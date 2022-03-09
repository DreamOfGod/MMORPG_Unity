//===============================================
//作    者：
//创建时间：2022-03-08 13:57:46
//备    注：
//===============================================

using UnityEngine;
/// <summary>
/// 受伤状态
/// </summary>
public class RoleStateHurt : RoleStateAbstract
{
    public RoleStateHurt(RoleFSM roleFSM) : base(roleFSM) { }

    public override void OnEnter()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToHurt, true);
    }

    public override void OnUpdate()
    {
        AnimatorStateInfo info = m_RoleFSM.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > 1)
        {
            m_RoleFSM.ChangeToIdleState();
        }
    }

    public override void OnLeave()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToHurt, false);
    }
}
