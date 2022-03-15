ProductEntity = { Id = 0, Name = "", Price = 0, PicName = "", Desc = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
ProductEntity.__index = ProductEntity;

function ProductEntity.New(Id, Name, Price, PicName, Desc)
    local self = { }; --初始化self
    setmetatable(self, ProductEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Price = Price;
    self.PicName = PicName;
    self.Desc = Desc;

    return self;
end