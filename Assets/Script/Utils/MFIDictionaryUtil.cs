//===============================================
//作    者：
//创建时间：2022-04-09 14:28:09
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MFIDictionaryUtil
{
    public static bool IsNullOrEmpty(this IDictionary<string, object> collection)
    {
        return (collection == null) || (collection.Count == 0);
    }
}
