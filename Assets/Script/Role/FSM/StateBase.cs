//===============================================
//作    者：
//创建时间：2022-03-10 13:34:12
//备    注：
//===============================================

public abstract class StateBase
{
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
