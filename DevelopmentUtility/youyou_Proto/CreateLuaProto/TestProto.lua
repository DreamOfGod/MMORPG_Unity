--测试协议
TestProto = { ProtoCode = 5001, IsSuccess = false, ErrorCode = 0, Name = "", Count = 0, ItemIdTable = { }, RoleTable = { } }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
TestProto.__index = TestProto;

function TestProto.New()
    local self = { }; --初始化self
    setmetatable(self, TestProto); --将self的元表设定为Class
    return self;
end


--定义角色
Role = { RoleId = 0, RoleName = "" }
Role.__index = Role;
function Role.New()
    local self = { };
    setmetatable(self, Role);
    return self;
end


--发送协议
function TestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteBool(proto.IsSuccess);
    if(proto.IsSuccess) then
        ms:WriteUTF8String(Name);
        else
        ms:WriteInt(ErrorCode);
    end
    ms:WriteInt(proto.Count);
    for i = 1, proto.Count, 1 do
        ms:WriteInt(ItemIdList[i]);
        ms:WriteInt(RoleList[i].RoleId);
        ms:WriteUTF8String(RoleList[i].RoleName);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function TestProto.GetProto(buffer)

    local proto = TestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.IsSuccess = ms:ReadBool();
    if(proto.IsSuccess) then
        proto.Name = ms:ReadUTF8String();
        else
        proto.ErrorCode = ms:ReadInt();
    end
    proto.Count = ms:ReadInt();
	proto.ItemIdTable = {};
	proto.RoleTable = {};
    for i = 1, proto.Count, 1 do
        local _ItemId = ms:ReadInt();  --物品ID
        proto.ItemIdTable[#proto.ItemIdTable+1] = _ItemId;
        local _Role = Role.New();
        _Role.RoleId = ms:ReadInt();
        _Role.RoleName = ms:ReadUTF8String();
        proto.RoleTable[#proto.RoleTable+1] = _Role;
    end

    ms:Dispose();
    return proto;
end