//===============================================
//作    者：
//创建时间：2022-03-13 01:06:37
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 怪物控制器
/// </summary>
public partial class MonsterCtrl
{
    /// <summary>
    /// 怪物攻击状态
    /// </summary>
    private class MonsterStateAttack : StateBase
    {
        private MonsterCtrl m_MonsterCtrl;

        public MonsterStateAttack(MonsterCtrl monsterCtrl)
        {
            m_MonsterCtrl = monsterCtrl;
        }

        public override void OnEnter()
        {
            m_MonsterCtrl.m_Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 1);
            //朝向主角
            Vector3 mainPlayerPos = m_MonsterCtrl.m_MainPlayerCtrl.transform.position;
            mainPlayerPos.y = m_MonsterCtrl.transform.position.y;
            m_MonsterCtrl.transform.LookAt(mainPlayerPos);
            //攻击
            m_MonsterCtrl.m_MainPlayerCtrl.ChangeToHurtState(Random.Range(5, 15), 0.2f);
            m_MonsterCtrl.m_NextAttackTime = Time.time + 2f;
        }

        public override void OnLeave()
        {
            m_MonsterCtrl.m_Animator.SetInteger(AnimStateConditionName.ToPhyAttack, 0);
        }

        public override void OnUpdate()
        {
            AnimatorStateInfo info = m_MonsterCtrl.m_Animator.GetCurrentAnimatorStateInfo(0);
            if(info.normalizedTime > 1)
            {
                m_MonsterCtrl.ChangeToIdleState();
            }
        }
    }
}