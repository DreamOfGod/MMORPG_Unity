
//===================================================
//作    者：
//创建时间：2022-03-15 16:30:23
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Job数据管理
/// </summary>
public partial class JobDBModel : AbstractDBModel<JobDBModel, JobEntity>
{
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
        entity.JobPic = parse.GetFieldValue("JobPic");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.Desc = parse.GetFieldValue("Desc");
        return entity;
    }
}
