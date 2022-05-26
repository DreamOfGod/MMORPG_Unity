//===============================================
//作    者：
//创建时间：2022-05-26 10:12:58
//备    注：
//===============================================

//===================================================
//作    者：
//创建时间：2022-05-26 10:11:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Job数据管理
/// </summary>
public partial class JobConfig : AbstractConfig<JobEntity>
{
   #region 单例
   private JobConfig() { }
   private static JobConfig m_Instance;
   public static JobConfig Instance
   {
       get
       {
           if(m_Instance == null)
           {
                m_Instance = new JobConfig();
           }
           return m_Instance;
        }
    }
   #endregion

    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Job.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override JobEntity MakeEntity(GameDataTableParser parse)
    {
        JobEntity entity = new JobEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.RolePic = parse.GetFieldValue("RolePic");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.Desc = parse.GetFieldValue("Desc");
        return entity;
    }
}
