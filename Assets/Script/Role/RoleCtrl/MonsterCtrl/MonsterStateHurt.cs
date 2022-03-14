//===============================================
//作    者：
//创建时间：2022-03-14 11:20:28
//备    注：
//===============================================

public partial class MonsterCtrl
{
    /// <summary>
    /// 怪物受伤状态
    /// </summary>
    private class MonsterStateHurt : StateBase
    {
        private MonsterCtrl m_MonsterCtrl;

        public MonsterStateHurt(MonsterCtrl monsterCtrl)
        {
            m_MonsterCtrl = monsterCtrl;
        }

        public override void OnEnter()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, true);
        }

        public override void OnLeave()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, false);
        }

        public override void OnUpdate()
        {
            if (m_MonsterCtrl.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if (m_MonsterCtrl.HP > 0)
                {
                    m_MonsterCtrl.ChangeToIdleState();
                }
                else
                {
                    m_MonsterCtrl.ChangeToDieState();
                }
            }
        }
    }
}
