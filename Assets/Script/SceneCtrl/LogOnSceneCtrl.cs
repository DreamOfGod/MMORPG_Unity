//===============================================
//作    者：
//创建时间：2022-02-23 16:44:48
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnSceneCtrl : MonoBehaviour
{
    void Awake()
    {
        GameObject obj = Resources.Load<GameObject>("UIPrefab/UIScene/UI Root_LogOnScene");
        Instantiate(obj);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}