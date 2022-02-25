//===============================================
//作    者：
//创建时间：2022-02-23 16:47:56
//备    注：
//===============================================
using System;
using System.Collections;
using UnityEngine;

public class ResourcesMgr: Singleton<ResourcesMgr>, IDisposable
{
    /// <summary>
    /// 预设缓存
    /// </summary>
    private Hashtable m_PrefabTable;

    public ResourcesMgr()
    {
        m_PrefabTable = new Hashtable();
    }

    #region 加载资源
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="cache">是否缓存</param>
    /// <returns>预设体克隆</returns>
    private GameObject Load(string path, bool cache = false)
    {
        GameObject obj = null;
        if (m_PrefabTable.ContainsKey(path))
        {
            obj = m_PrefabTable[path] as GameObject;
        }
        else
        {
            obj = Resources.Load(path) as GameObject;
            if(cache)
            {
                m_PrefabTable.Add(path, obj);
            }
        }
        return GameObject.Instantiate(obj);
    }

    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="name">场景UI名称</param>
    /// <returns></returns>
    public GameObject LoadUIScene(string name, bool cache = false)
    {
        string path = "UIPrefab/UIScene/" + name;
        return Load(path, cache);
    }

    /// <summary>
    /// 加载窗口
    /// </summary>
    /// <param name="name">窗口名</param>
    /// <returns></returns>
    public GameObject LoadUIWindows(string name, bool cache = false)
    {
        string path = "UIPrefab/UIWindows/" + name;
        return Load(path, cache);
    }

    /// <summary>
    /// 加载角色
    /// </summary>
    /// <param name="name">角色名</param>
    /// <returns></returns>
    public GameObject LoadRole(string name, bool cache = false)
    {
        string path = "UIPrefab/RolePrefab/" + name;
        return Load(path, cache);
    }

    /// <summary>
    /// 加载特效
    /// </summary>
    /// <param name="name">特效名</param>
    /// <returns></returns>
    public GameObject LoadEffect(string name, bool cache = false)
    {
        string path = "UIPrefab/EffectPrefab/" + name;
        return Load(path, cache);
    }
    #endregion

    #region 释放资源
    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        m_PrefabTable.Clear();
        Resources.UnloadUnusedAssets();
    }
    #endregion
}
