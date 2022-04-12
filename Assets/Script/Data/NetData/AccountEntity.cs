//===============================================
//作    者：
//创建时间：2022-03-16 16:53:24
//备    注：
//===============================================

using System;

/// <summary>
/// 账户实体
/// </summary>
public class AccountEntity
{
    public int PKValue { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Pwd { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Mail { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Money { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short ChannelId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int LastLogonServerId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string LastLogonServerName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime LastLogonServerTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int LastLogonRoleId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string LastLogonRoleNickname { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int LastLogonRoleJobId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string DeviceIdentifier { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string DeviceModel { get; set; }
}