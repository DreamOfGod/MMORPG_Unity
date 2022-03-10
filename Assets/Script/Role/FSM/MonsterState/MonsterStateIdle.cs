//===============================================
//作    者：
//创建时间：2022-03-10 14:56:48
//备    注：
//===============================================

public class MonsterStateIdle : StateBase
{
    public MonsterStateIdle(MonsterCtrl monsterCtrl): base(monsterCtrl) { }

    public override void OnEnter()
    {
        m_RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, true);
    }

    public override void OnLeave()
    {
        m_RoleCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, false);
    }
}
