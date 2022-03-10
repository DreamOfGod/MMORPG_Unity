//===============================================
//作    者：
//创建时间：2022-03-10 13:34:12
//备    注：
//===============================================

public abstract class StateBase
{
    protected RoleCtrlBase m_RoleCtrl;

    public StateBase(RoleCtrlBase roleCtrl)
    {
        m_RoleCtrl = roleCtrl;
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
