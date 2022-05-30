//===============================================
//作    者：
//创建时间：2022-03-10 11:17:24
//备    注：
//===============================================
using UnityEngine;

public partial class RoleCtrl
{
    // 主角休闲状态
    private class RoleStateIdle : StateBase
    {
        private RoleCtrl m_RoleCtrl;

        public RoleStateIdle(RoleCtrl roleCtrl)
        {
            m_RoleCtrl = roleCtrl;
        }

        public override void OnEnter()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleNormal, true);
        }

        public override void OnLeave()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleNormal, false);
        }

        public override void OnUpdate()
        {
            //if (m_MainPlayerCtrl.m_TargetMonster != null && !m_MainPlayerCtrl.m_TargetMonster.isDieState())
            //{
            //    //有目标怪物且怪物未死亡
            //    float distance = Vector3.Distance(m_MainPlayerCtrl.transform.position, m_MainPlayerCtrl.m_TargetMonster.transform.position);
            //    if(distance <= m_MainPlayerCtrl.m_AttackDistance)
            //    {
            //        //目标怪物在攻击范围内
            //        if(Time.time >= m_MainPlayerCtrl.m_NextAttackTime)
            //        {
            //            //达到攻击时间，转为攻击状态
            //            m_MainPlayerCtrl.ChangeToAttackState();
            //        }
            //    }
            //    else
            //    {
            //        //不在攻击范围内，跑向目标怪物
            //        m_MainPlayerCtrl.ChangeToRunState(m_MainPlayerCtrl.m_TargetMonster.transform.position);
            //    }
            //    return;
            //}
        }
    }
}