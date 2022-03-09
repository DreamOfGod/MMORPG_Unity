//===============================================
//作    者：
//创建时间：2022-03-08 13:57:58
//备    注：
//===============================================

/// <summary>
/// 死亡状态
/// </summary>
public class RoleStateDie : RoleStateAbstract
{
    public RoleStateDie(RoleFSM roleFSM) : base(roleFSM) { }

    public override void OnEnter()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToDie, true);
    }

    public override void OnUpdate()
    {
    }

    public override void OnLeave()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToDie, false);
    }
}
