//===============================================
//作    者：
//创建时间：2022-02-19 20:22:02
//备    注：
//===============================================
//===============================================
//作    者：
//创建时间：2022-02-19 20:22:02
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtils
{
    public static int ToInt(this string str)//扩展方法
    {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }
}
