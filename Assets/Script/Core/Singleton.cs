//===============================================
//作    者：
//创建时间：2022-02-23 16:50:48
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T: new()//泛型约束：T必须具有无参构造
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
