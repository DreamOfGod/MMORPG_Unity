//===============================================
//作    者：
//创建时间：2022-03-08 13:58:09
//备    注：
//===============================================

using UnityEngine;
/// <summary>
/// 攻击状态
/// </summary>
public class RoleStateAttack : RoleStateAbstract
{
    public RoleStateAttack(RoleFSM roleFSM): base(roleFSM) { }

    public override void OnEnter()
    {
        m_RoleFSM.RoleCtrl.Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 1);
    }

    public override void OnUpdate()
    {
        AnimatorStateInfo info = m_RoleFSM.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime > 1)
        {
            m_RoleFSM.ChangeToIdleState();
        }
    }

    public override void OnLeave()
    {
        m_RoleFSM.RoleCtrl.Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 0);
    }
}
