//===================================================
//作    者：
//创建时间：2022-03-22 14:16:18
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器广播其他角色离开场景消息
/// </summary>
public struct WorldMap_OtherRoleLeaveProto : IProtocol
{
    public ushort ProtoCode { get { return 13006; } }

    public int RoleId; //角色编号

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            return ms.ToArray();
        }
    }

    public static WorldMap_OtherRoleLeaveProto GetProto(byte[] buffer)
    {
        WorldMap_OtherRoleLeaveProto proto = new WorldMap_OtherRoleLeaveProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.RoleId = ms.ReadInt();
        }
        return proto;
    }
}