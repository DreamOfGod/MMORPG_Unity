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
/// 服务器返回出售物品给系统消息
/// </summary>
public struct Goods_SellToSysReturnProto : IProtocol
{
    public ushort ProtoCode { get { return 16009; } }

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
            }
            ms.WriteInt(MsgCode);
            return ms.ToArray();
        }
    }

    public static Goods_SellToSysReturnProto GetProto(byte[] buffer)
    {
        Goods_SellToSysReturnProto proto = new Goods_SellToSysReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
            }
            proto.MsgCode = ms.ReadInt();
        }
        return proto;
    }
}