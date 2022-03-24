//===============================================
//作    者：
//创建时间：2022-03-24 10:27:35
//备    注：
//===============================================

using System;
using UnityEngine;

public class AssetBundleLoader: IDisposable
{
    private AssetBundle bundle;

    public AssetBundleLoader(string assetBundlePath)
    {
        string fullPath = LocalFileMgr.Instance.LocalFilePath + assetBundlePath;
        bundle = AssetBundle.LoadFromMemory(LocalFileMgr.Instance.GetBuffer(fullPath));
    }

    public void Dispose()
    {
        bundle.Unload(false);
    }

    public T LoadAsset<T>(string name) where T: UnityEngine.Object
    {
        return bundle.LoadAsset<T>(name);
    }

    public T LoadAssetAndClone<T>(string name) where T : UnityEngine.Object
    {
        return UnityEngine.Object.Instantiate(LoadAsset<T>(name));
    }
}