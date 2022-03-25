//===============================================
//作    者：
//创建时间：2022-03-23 13:37:46
//备    注：
//===============================================

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Menu
{
    /// <summary>
    /// MyTools/Settings菜单：显示全局设置窗口
    /// </summary>
    [MenuItem("MyTools/Settings")]
    public static void Settings()
    {
        SettingsWindow win = EditorWindow.GetWindow<SettingsWindow>();
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }

    /// <summary>
    /// MyTools/AssetBundleCreate菜单：显示创建AssetBundle资源的窗口
    /// </summary>
    [MenuItem("MyTools/AssetBundleCreate")]
    public static void AssetBundleCreate()
    {
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("资源打包");
        win.Show();
    }

    [MenuItem("MyTools/CreateAllAssetBundle")]
    public static void CreateAllAssetBundle()
    {
        string path = $"{ Application.dataPath }/../AllAssetBundls";
        if(Directory.Exists(path))
        {
            Directory.Delete(path);
        }
        Directory.CreateDirectory(path);
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}