//===============================================
//作    者：
//创建时间：2022-03-08 13:57:33
//备    注：
//===============================================

/// <summary>
/// 跑状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{
    public RoleStateRun(RoleFSM roleFSM) : base(roleFSM) { }

    public override void OnEnter()
    {
        base.OnEnter();
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToRun, true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToRun, false);
    }
}
