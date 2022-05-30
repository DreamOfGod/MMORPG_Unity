//===============================================
//作    者：
//创建时间：2022-03-13 01:19:03
//备    注：
//===============================================
using UnityEngine;

public partial class RoleCtrl
{
    // 主角受伤状态
    private class RoleStateHurt : StateBase
    {
        private RoleCtrl m_RoleCtrl;

        public RoleStateHurt(RoleCtrl roleCtrl)
        {
            m_RoleCtrl = roleCtrl;
        }

        public override void OnEnter()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, true);
        }

        public override void OnLeave()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, false);
        }

        public override void OnUpdate()
        {
            if(m_RoleCtrl.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if(m_RoleCtrl.HP > 0)
                {
                    m_RoleCtrl.ChangeToIdleState();
                }
                else
                {
                    m_RoleCtrl.ChangeToDieState();
                }
            }
        }
    }
}
