//===============================================
//作    者：
//创建时间：2022-03-14 14:09:45
//备    注：
//===============================================
using UnityEngine;

public partial class RoleCtrl
{
    // 主角攻击状态
    private class RoleStateAttack : StateBase
    {
        private RoleCtrl m_RoleCtrl;

        public RoleStateAttack(RoleCtrl roleCtrl)
        {
            m_RoleCtrl = roleCtrl;
        }

        public override void OnEnter()
        {
            m_RoleCtrl.m_Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 2);
            //朝向目标怪物
            Vector3 targetPos = m_RoleCtrl.m_TargetMonster.transform.position;
            targetPos.y = m_RoleCtrl.transform.position.y;
            m_RoleCtrl.transform.LookAt(targetPos);
            //攻击目标
            m_RoleCtrl.m_TargetMonster.ChangeToHurtState(Random.Range(50, 150), 0.2f);
            m_RoleCtrl.m_NextAttackTime = Time.time + 0.8f;
        }

        public override void OnLeave()
        {
            m_RoleCtrl.m_Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 0);
        }

        public override void OnUpdate()
        {
            if (m_RoleCtrl.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                m_RoleCtrl.ChangeToIdleState();
            }
        }
    }
}
