//===================================================
//作    者：
//创建时间：2022-05-23 20:58:50
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回创建角色消息
/// </summary>
public struct RoleOperation_CreateRoleReturnProto : IProtocol
{
    public ushort ProtoCode { get { return 10004; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码
    public int RoleId; //角色编号
    public byte RoleJob; //角色职业
    public string RoleNickName; //角色昵称
    public int RoleLevel; //角色等级

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(IsSuccess)
            {
                ms.WriteInt(RoleId);
                ms.WriteByte(RoleJob);
                ms.WriteUTF8String(RoleNickName);
                ms.WriteInt(RoleLevel);
            }
            else
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static RoleOperation_CreateRoleReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_CreateRoleReturnProto proto = new RoleOperation_CreateRoleReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(proto.IsSuccess)
            {
                proto.RoleId = ms.ReadInt();
                proto.RoleJob = (byte)ms.ReadByte();
                proto.RoleNickName = ms.ReadUTF8String();
                proto.RoleLevel = ms.ReadInt();
            }
            else
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}