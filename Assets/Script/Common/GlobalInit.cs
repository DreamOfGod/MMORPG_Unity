//===============================================
//作    者：
//创建时间：2022-03-07 14:13:17
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInit : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
