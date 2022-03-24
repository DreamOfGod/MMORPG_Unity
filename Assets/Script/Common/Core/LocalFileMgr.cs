//===============================================
//作    者：
//创建时间：2022-03-24 09:53:00
//备    注：
//===============================================

using System.IO;
using UnityEngine;
/// <summary>
/// 本地文件管理
/// </summary>
public class LocalFileMgr : Singleton<LocalFileMgr>
{
#if UNITY_EDITOR
    #if UNITY_STANDALONE_WIN
        public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Windows/";
    #elif UNITY_ANDROID
        public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Android/";
    #elif UNITY_IPHONE
        public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/IOS/";
    #endif
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_WIN
        public readonly string LocalFilePath = Application.persistentDataPath + "/";
#endif

    /// <summary>
    /// 读取本地文件到数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;
        using(FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
