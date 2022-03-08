//===============================================
//作    者：
//创建时间：2022-03-08 13:57:22
//备    注：
//===============================================

/// <summary>
/// 待机状态
/// </summary>
public class RoleStateIdle : RoleStateAbstract
{
    public RoleStateIdle(RoleFSM roleFSM) : base(roleFSM) { }

    public override void OnEnter()
    {
        base.OnEnter();
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleNormal, true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleNormal, false);
    }
}
