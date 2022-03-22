//===================================================
//作    者：
//创建时间：2022-03-22 14:16:18
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送查询任务消息
/// </summary>
public struct Task_SearchTaskProto : IProtocol
{
    public ushort ProtoCode { get { return 15001; } }


    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            return ms.ToArray();
        }
    }

    public static Task_SearchTaskProto GetProto(byte[] buffer)
    {
        Task_SearchTaskProto proto = new Task_SearchTaskProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
        }
        return proto;
    }
}