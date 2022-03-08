//===============================================
//作    者：
//创建时间：2022-03-08 13:57:01
//备    注：
//===============================================

/// <summary>
/// 角色状态抽象基类
/// </summary>
public abstract class RoleStateAbstract
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    protected RoleFSM m_RoleFSM;

    public RoleStateAbstract(RoleFSM roleFSM)
    {
        m_RoleFSM = roleFSM;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 执行状态
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 离开状态
    /// </summary>
    public virtual void OnLeave() { }
}
