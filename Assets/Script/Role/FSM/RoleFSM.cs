//===============================================
//作    者：
//创建时间：2022-03-08 13:56:41
//备    注：
//===============================================

/// <summary>
/// 角色有限状态机
/// </summary>
public class RoleFSM
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    public RoleCtrl RoleCtrl { get; private set; }

    /// <summary>
    /// 当前角色状态
    /// </summary>
    private RoleStateAbstract m_CurrRoleState;

    public RoleFSM(RoleCtrl roleCtrl)
    {
        RoleCtrl = roleCtrl;

        m_CurrRoleState = new RoleStateIdle(this);
        m_CurrRoleState.OnEnter();
    }

    #region OnUpdate 每帧执行
    public void OnUpdate()
    {
        m_CurrRoleState.OnUpdate();
    }
    #endregion

    #region 进入各种状态
    public void ChangeToRunState()
    {
        if(isRunState())
        {
            return;
        }

        m_CurrRoleState.OnLeave();
        m_CurrRoleState = new RoleStateRun(this);
        m_CurrRoleState.OnEnter();
    }

    public void ChangeToIdleState()
    {
        if (isIdleState())
        {
            return;
        }

        m_CurrRoleState.OnLeave();
        m_CurrRoleState = new RoleStateIdle(this);
        m_CurrRoleState.OnEnter();
    }

    public void ChangeToHurtState()
    {
        if (isHurtState())
        {
            return;
        }

        m_CurrRoleState.OnLeave();
        m_CurrRoleState = new RoleStateHurt(this);
        m_CurrRoleState.OnEnter();
    }

    public void ChangeToDieState()
    {
        if (isDieState())
        {
            return;
        }

        m_CurrRoleState.OnLeave();
        m_CurrRoleState = new RoleStateDie(this);
        m_CurrRoleState.OnEnter();
    }

    public void ChangeToAttackState()
    {
        if (isAttackState())
        {
            return;
        }

        m_CurrRoleState.OnLeave();
        m_CurrRoleState = new RoleStateAttack(this);
        m_CurrRoleState.OnEnter();
    }
    #endregion

    #region 判断状态
    public bool isIdleState()
    {
        return m_CurrRoleState is RoleStateIdle;
    }

    public bool isRunState()
    {
        return m_CurrRoleState is RoleStateRun;
    }

    public bool isHurtState()
    {
        return m_CurrRoleState is RoleStateHurt;
    }

    public bool isDieState()
    {
        return m_CurrRoleState is RoleStateDie;
    }

    public bool isAttackState()
    {
        return m_CurrRoleState is RoleStateAttack;
    }
    #endregion
}
