//===============================================
//作    者：
//创建时间：2022-03-14 14:09:45
//备    注：
//===============================================
using UnityEngine;

public partial class MainPlayerCtrl
{
    /// <summary>
    /// 主角攻击状态
    /// </summary>
    private class MainPlayerStateAttack : StateBase
    {
        private MainPlayerCtrl m_MainPlayerCtrl;

        public MainPlayerStateAttack(MainPlayerCtrl mainPlayerCtrl)
        {
            m_MainPlayerCtrl = mainPlayerCtrl;
        }

        public override void OnEnter()
        { 
            m_MainPlayerCtrl.m_Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 1);

            Vector3 targetMonsterPos = m_MainPlayerCtrl.m_TargetMonster.transform.position;
            targetMonsterPos.y = m_MainPlayerCtrl.transform.position.y;
            m_MainPlayerCtrl.transform.LookAt(targetMonsterPos);

            m_MainPlayerCtrl.m_TargetMonster.ChangeToHurtState(Random.Range(50, 150), 0.2f);
            m_MainPlayerCtrl.m_NextAttackTime = Time.time + 0.8f;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 0);
        }

        public override void OnUpdate()
        {
            AnimatorStateInfo info = m_MainPlayerCtrl.m_Animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime > 1)
            {
                m_MainPlayerCtrl.ChangeToIdleState();
            }
        }
    }
}
