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
/// 服务器返回进入世界地图场景消息
/// </summary>
public struct WorldMap_RoleEnterReturnProto : IProtocol
{
    public ushort ProtoCode { get { return 13002; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static WorldMap_RoleEnterReturnProto GetProto(byte[] buffer)
    {
        WorldMap_RoleEnterReturnProto proto = new WorldMap_RoleEnterReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}