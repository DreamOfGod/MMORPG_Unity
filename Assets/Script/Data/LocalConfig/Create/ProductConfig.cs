//===============================================
//作    者：
//创建时间：2022-04-21 11:10:32
//备    注：
//===============================================

//===================================================
//作    者：
//创建时间：2022-04-21 11:10:14
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Product数据管理
/// </summary>
public partial class ProductConfig : AbstractConfig<ProductEntity>
{
   #region 单例
   private ProductConfig() { }
   private static ProductConfig m_Instance;
   public static ProductConfig Instance
   {
       get
       {
           if(m_Instance == null)
           {
                m_Instance = new ProductConfig();
           }
           return m_Instance;
        }
    }
   #endregion

    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Product.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ProductEntity MakeEntity(GameDataTableParser parse)
    {
        ProductEntity entity = new ProductEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Price = parse.GetFieldValue("Price").ToFloat();
        entity.PicName = parse.GetFieldValue("PicName");
        entity.Desc = parse.GetFieldValue("Desc");
        return entity;
    }
}
