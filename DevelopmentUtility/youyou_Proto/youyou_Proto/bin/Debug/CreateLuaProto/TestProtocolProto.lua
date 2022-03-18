--测试协议
TestProtocolProto = { ProtoCode = 1004, Id = 0, Name = "", Type = 0, Price = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
TestProtocolProto.__index = TestProtocolProto;

function TestProtocolProto.New()
    local self = { }; --初始化self
    setmetatable(self, TestProtocolProto); --将self的元表设定为Class
    return self;
end


--发送协议
function TestProtocolProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.Id);
    ms:WriteUTF8String(proto.Name);
    ms:WriteInt(proto.Type);
    ms:WriteFloat(proto.Price);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function TestProtocolProto.GetProto(buffer)

    local proto = TestProtocolProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.Id = ms:ReadInt();
    proto.Name = ms:ReadUTF8String();
    proto.Type = ms:ReadInt();
    proto.Price = ms:ReadFloat();

    ms:Dispose();
    return proto;
end