using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ScriptCreateInit : UnityEditor.AssetModificationProcessor
{
    private static void OnWillCreateAsset(string path)
    {
        //创建c#文件后，在文件开头拼接上作者、创建时间、备注等信息
        path = path.Replace(".meta", "");
        if(path.EndsWith(".cs"))
        {
            string strContent = "//===============================================\n"
                              + "//作    者：\n"
                              + "//创建时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n"
                              + "//备    注：\n"
                              + "//===============================================\n"
                              + File.ReadAllText(path);
            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
        }
    }
}
