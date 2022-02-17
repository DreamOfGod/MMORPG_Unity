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
        //����c#�ļ������ļ���ͷƴ�������ߡ�����ʱ�䡢��ע����Ϣ
        path = path.Replace(".meta", "");
        if(path.EndsWith(".cs"))
        {
            string strContent = "//===============================================\n"
                              + "//��    �ߣ�\n"
                              + "//����ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n"
                              + "//��    ע��\n"
                              + "//===============================================\n"
                              + File.ReadAllText(path);
            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
        }
    }
}
