//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-10-23 16:07:22
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送物品加入背包消息
/// </summary>
public struct Backpack_GoodsAddProto : IProto
{
    public ushort ProtoCode { get { return 16004; } }

    public byte GoodsInType; //物品入库类型
    public int GoodsKindCount; //物品种类数量
    public List<GoodsKindItem> ItemList; //物品种类

    /// <summary>
    /// 物品种类
    /// </summary>
    public struct GoodsKindItem
    {
        public byte GoodsType; //物品类型
        public int GoodsId; //物品编号
        public int GoodsCount; //物品数量
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteByte(GoodsInType);
            ms.WriteInt(GoodsKindCount);
            for (int i = 0; i < GoodsKindCount; i++)
            {
                ms.WriteByte(ItemList[i].GoodsType);
                ms.WriteInt(ItemList[i].GoodsId);
                ms.WriteInt(ItemList[i].GoodsCount);
            }
            return ms.ToArray();
        }
    }

    public static Backpack_GoodsAddProto GetProto(byte[] buffer)
    {
        Backpack_GoodsAddProto proto = new Backpack_GoodsAddProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.GoodsInType = (byte)ms.ReadByte();
            proto.GoodsKindCount = ms.ReadInt();
            proto.ItemList = new List<GoodsKindItem>();
            for (int i = 0; i < proto.GoodsKindCount; i++)
            {
                GoodsKindItem _Item = new GoodsKindItem();
                _Item.GoodsType = (byte)ms.ReadByte();
                _Item.GoodsId = ms.ReadInt();
                _Item.GoodsCount = ms.ReadInt();
                proto.ItemList.Add(_Item);
            }
        }
        return proto;
    }
}