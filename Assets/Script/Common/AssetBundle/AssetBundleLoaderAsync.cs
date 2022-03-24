//===============================================
//作    者：
//创建时间：2022-03-24 10:27:35
//备    注：
//===============================================

using System;
using System.IO;
using UnityEngine;

public class AssetBundleLoaderAsync : IDisposable
{
    private AssetBundle m_Bundle;

    private AssetBundleLoaderAsync(AssetBundle bundle)
    {
        m_Bundle = bundle;
    }

    public static void LoadAsync(string assetBundlePath, Action<AssetBundleLoaderAsync> callback)
    {
        string fullPath = LocalAssetBundlePath.Value + assetBundlePath;
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(fullPath);
        request.completed += (AsyncOperation ao) => {
            callback(new AssetBundleLoaderAsync(request.assetBundle));
        };
    }

    public void Dispose()
    {
        m_Bundle.Unload(false);
    }

    public void LoadAssetAsync<T>(string name, Action<UnityEngine.Object> callback) where T : UnityEngine.Object
    {
        AssetBundleRequest request = m_Bundle.LoadAssetAsync<T>(name);
        request.completed += (AsyncOperation ao) => {
            callback(request.asset);
        };
    }

    public void LoadAssetAsyncAndClone<T>(string name, Action<GameObject> callback) where T : UnityEngine.Object
    {
        LoadAssetAsync<T>(name, (UnityEngine.Object obj) => {
            callback(UnityEngine.Object.Instantiate(obj) as GameObject);
        });
    }
}