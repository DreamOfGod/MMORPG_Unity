require "Download/XLuaLogic/Data/Create/ProductEntity"

--数据访问
ProductConfig = { }

local this = ProductConfig;

local productTable = { }; --定义表格

function ProductConfig.New()
    return this;
end

function ProductConfig.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Product.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        productTable[#productTable+1] = ProductEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3], gameDataTable.Data[i][4] );
    end

end

function ProductConfig.GetList()
    return productTable;
end

function ProductConfig.GetEntity(id)
    local ret = nil;
    for i = 1, #productTable, 1 do
        if (productTable[i].Id == id) then
            ret = productTable[i];
            break;
        end
    end
    return ret;
end