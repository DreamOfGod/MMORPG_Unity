//===============================================
//作    者：
//创建时间：2022-03-13 01:27:39
//备    注：
//===============================================

public partial class RoleCtrl
{
    // 主角死亡状态
    private class RoleStateDie : StateBase
    {
        private RoleCtrl m_RoleCtrl;

        public RoleStateDie(RoleCtrl roleCtrl)
        {
            m_RoleCtrl = roleCtrl;
        }

        public override void OnEnter()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToDie, true);
            DestroyImmediate(m_RoleCtrl.m_HeadBarCtrl.gameObject);
        }

        public override void OnLeave()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToDie, false);
        }
    }
}