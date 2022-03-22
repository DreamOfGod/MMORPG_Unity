//===================================================
//作    者：
//创建时间：2022-03-22 14:16:18
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送查询背包项消息
/// </summary>
public struct Backpack_SearchProto : IProtocol
{
    public ushort ProtoCode { get { return 16004; } }


    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            return ms.ToArray();
        }
    }

    public static Backpack_SearchProto GetProto(byte[] buffer)
    {
        Backpack_SearchProto proto = new Backpack_SearchProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
        }
        return proto;
    }
}