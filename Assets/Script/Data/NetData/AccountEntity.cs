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
    public int Id { get; set; }

    public string Username { get; set; }

    public string Pwd { get; set; }

    public int YuanBao { get; set; }

    public int LastServerId { get; set; }

    public string LastServerName { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}