//===============================================
//作    者：
//创建时间：2022-03-18 12:16:08
//备    注：
//===============================================

/// <summary>
/// 测试协议
/// </summary>
public struct TestProtocol : IProtocol
{
    public ushort ProtocolID { get { return 1001; } }

    public int Id;
    public string Name;
    public int Type;
    public float Price;

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.WriteUShort(ProtocolID);
        ms.WriteInt(Id);
        ms.WriteUTF8String(Name);
        ms.WriteInt(Type);
        ms.WriteFloat(Price);
        return ms.ToArray();
    }

    public static TestProtocol GetProtocol(byte[] buffer)
    {
        TestProtocol protocol = new TestProtocol();

        MMO_MemoryStream ms = new MMO_MemoryStream(buffer);
        ms.ReadUShort();
        protocol.Id = ms.ReadInt();
        protocol.Name = ms.ReadUTF8String();
        protocol.Type = ms.ReadInt();
        protocol.Price = ms.ReadFloat();

        return protocol;
    }

}
