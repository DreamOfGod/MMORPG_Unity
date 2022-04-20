//===============================================
//作    者：
//创建时间：2022-04-20 10:08:11
//备    注：
//===============================================
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 节点池
/// </summary>
public class GameObjectPool
{
    private GameObject m_OriginObject;
    private Queue<GameObject> m_ObjectQueue;

    #region 构造函数
    public GameObjectPool(string prefabPath, int count): this(Resources.Load<GameObject>(prefabPath), count) { }

    public GameObjectPool(GameObject originObject, int count)
    {
        if(count < 1)
        {
            throw new ArgumentException("count必须大于0");
        }
        m_OriginObject = originObject;
        m_ObjectQueue = new Queue<GameObject>(count);
        for (int i = 0; i < count; ++i)
        {
            GameObject clone = UnityEngine.Object.Instantiate(m_OriginObject, null);
            clone.SetActive(false);
            m_ObjectQueue.Enqueue(clone);
        }
    }
    #endregion

    #region Get
    public GameObject Get(Transform parent)
    {
        GameObject obj;
        if(m_ObjectQueue.Count > 0)
        {
            obj = m_ObjectQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(parent, false);
        }
        else
        {
            obj = UnityEngine.Object.Instantiate(m_OriginObject, parent);
        }
        return obj;
    }

    public GameObject Get()
    {
        GameObject obj;
        if (m_ObjectQueue.Count > 0)
        {
            obj = m_ObjectQueue.Dequeue();
        }
        else
        {
            obj = UnityEngine.Object.Instantiate(m_OriginObject);
        }
        return obj;
    }
    #endregion

    #region Put
    public void Put(GameObject obj)
    {
        obj.SetActive(false);
        m_ObjectQueue.Enqueue(obj);
    }
    #endregion

    #region Destroy
    public void Destroy()
    {
        while(m_ObjectQueue.Count > 0)
        {
            UnityEngine.Object.Destroy(m_ObjectQueue.Dequeue());
        }
    }
    #endregion
}