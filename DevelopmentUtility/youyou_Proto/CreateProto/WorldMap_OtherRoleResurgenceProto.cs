//===================================================
//作    者：
//创建时间：2022-03-22 14:16:18
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器广播角色复活消息
/// </summary>
public struct WorldMap_OtherRoleResurgenceProto : IProtocol
{
    public ushort ProtoCode { get { return 13016; } }

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

    public static WorldMap_OtherRoleResurgenceProto GetProto(byte[] buffer)
    {
        WorldMap_OtherRoleResurgenceProto proto = new WorldMap_OtherRoleResurgenceProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.RoleId = ms.ReadInt();
        }
        return proto;
    }
}