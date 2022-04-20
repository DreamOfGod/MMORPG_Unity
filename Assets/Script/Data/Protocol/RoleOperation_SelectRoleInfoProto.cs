//===============================================
//作    者：
//创建时间：2022-04-20 15:54:30
//备    注：
//===============================================
//===================================================
//作    者：
//创建时间：2022-04-20 15:53:36
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端查询角色信息
/// </summary>
public struct RoleOperation_SelectRoleInfoProto : IProtocol
{
    public ushort ProtoCode { get { return 10009; } }

    public int RoldId; //角色编号

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoldId);
            return ms.ToArray();
        }
    }

    public static RoleOperation_SelectRoleInfoProto GetProto(byte[] buffer)
    {
        RoleOperation_SelectRoleInfoProto proto = new RoleOperation_SelectRoleInfoProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.RoldId = ms.ReadInt();
        }
        return proto;
    }
}