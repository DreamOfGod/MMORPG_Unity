//===============================================
//作    者：
//创建时间：2022-03-15 14:37:25
//备    注：
//===============================================
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 配置类抽象基类
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public abstract class AbstractConfig<TEntity>
    where TEntity: AbstractEntity
{
    /// <summary>
    /// 实体列表
    /// </summary>
    protected List<TEntity> m_List;

    /// <summary>
    /// (实体Id, 实体)的字典
    /// </summary>
    protected Dictionary<int, TEntity> m_Dic;

    public List<TEntity> List { get => m_List; }

    public Dictionary<int, TEntity> Dic { get => m_Dic; }

    public AbstractConfig()
    {
        m_List = new List<TEntity>();
        m_Dic = new Dictionary<int, TEntity>();
        LoadData();
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    protected void LoadData()
    {
        using(GameDataTableParser parser = new GameDataTableParser(string.Format(@$"{ Application.dataPath }\..\GameConfigData\{ FileName }")))
        {
            while(!parser.Eof)
            {
                TEntity e = MakeEntity(parser);
                m_List.Add(e);
                m_Dic[e.Id] = e;
                parser.Next();
            }
        }
    }

    /// <summary>
    /// 实体对应的数据文件名
    /// </summary>
    protected abstract string FileName { get; }

    /// <summary>
    /// 创建实体对象
    /// </summary>
    /// <param name="parser"></param>
    /// <returns></returns>
    protected abstract TEntity MakeEntity(GameDataTableParser parser);
}
