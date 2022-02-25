//===============================================
//作    者：
//创建时间：2022-02-24 16:49:29
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtils
{
    /// <summary>
    /// 获取或创建组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T GetOrCreateComponent<T>(this GameObject obj) where T: MonoBehaviour
    {
        T t = obj.GetComponent<T>();
        if(t == null)
        {
            t = obj.AddComponent<T>();
        }
        return t;
    }
}
