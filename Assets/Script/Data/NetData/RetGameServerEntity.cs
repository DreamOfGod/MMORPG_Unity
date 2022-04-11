//===============================================
//作    者：
//创建时间：2022-04-11 11:08:48
//备    注：
//===============================================

/// <summary>
/// 区服实体
/// </summary>
public class RetGameServerEntity
{
    /// <summary>
    /// 区服编号
    /// </summary>
    public int Id;

    /// <summary>
    /// 区服状态
    /// </summary>
    public int RunStatus;

    /// <summary>
    /// 是否推荐
    /// </summary>
    public bool IsCommand;

    /// <summary>
    /// 是否新服
    /// </summary>
    public bool IsNew;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// IP
    /// </summary>
    public string Ip;

    /// <summary>
    /// 端口号
    /// </summary>
    public int Port;
}