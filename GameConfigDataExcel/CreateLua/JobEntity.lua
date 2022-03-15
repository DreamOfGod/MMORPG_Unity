JobEntity = { Id = 0, Name = "", HeadPic = "", JobPic = "", PrefabName = "", Desc = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
JobEntity.__index = JobEntity;

function JobEntity.New(Id, Name, HeadPic, JobPic, PrefabName, Desc)
    local self = { }; --初始化self
    setmetatable(self, JobEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.HeadPic = HeadPic;
    self.JobPic = JobPic;
    self.PrefabName = PrefabName;
    self.Desc = Desc;

    return self;
end