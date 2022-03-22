//===================================================
//作    者：
//创建时间：2022-03-22 14:16:18
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 测试协议
/// </summary>
public struct TestProto : IProtocol
{
    public ushort ProtoCode { get { return 5001; } }

    public bool IsSuccess; //是否成功
    public int ErrorCode; //错误编号
    public string Name; //名称
    public int Count; //数量
    public List<int> ItemIdList; //物品ID
    public List<Role> RoleList; //角色

    /// <summary>
    /// 角色
    /// </summary>
    public struct Role
    {
        public int RoleId; //角色ID
        public string RoleName; //角色名称
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(IsSuccess)
            {
                ms.WriteUTF8String(Name);
            }
            else
            {
                ms.WriteInt(ErrorCode);
            }
            ms.WriteInt(Count);
            for (int i = 0; i < Count; i++)
            {
                ms.WriteInt(ItemIdList[i]);
                ms.WriteInt(RoleList[i].RoleId);
                ms.WriteUTF8String(RoleList[i].RoleName);
            }
            return ms.ToArray();
        }
    }

    public static TestProto GetProto(byte[] buffer)
    {
        TestProto proto = new TestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(proto.IsSuccess)
            {
                proto.Name = ms.ReadUTF8String();
            }
            else
            {
                proto.ErrorCode = ms.ReadInt();
            }
            proto.Count = ms.ReadInt();
            proto.ItemIdList = new List<int>();
            proto.RoleList = new List<Role>();
            for (int i = 0; i < proto.Count; i++)
            {
                int _ItemId = ms.ReadInt();  //物品ID
                proto.ItemIdList.Add(_ItemId);
                Role _Role = new Role();
                _Role.RoleId = ms.ReadInt();
                _Role.RoleName = ms.ReadUTF8String();
                proto.RoleList.Add(_Role);
            }
        }
        return proto;
    }
}