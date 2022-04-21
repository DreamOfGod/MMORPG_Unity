//===============================================
//作    者：
//创建时间：2022-04-21 10:28:16
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 资源包路径
/// </summary>
public static class AssetBundlePath
{
    #region 资源包的根目录
#if UNITY_EDITOR
#if UNITY_STANDALONE_WIN
        public static readonly string RootPath = $"{ Application.dataPath }/../AssetBundles/Windows/";
#elif UNITY_ANDROID
    public static readonly string RootPath = $"{ Application.dataPath }/../AssetBundles/Android/";
#elif UNITY_IPHONE
        public static readonly string RootPath = $"{ Application.dataPath }/../AssetBundles/IOS/";
#endif
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_WIN
        public static readonly string RootPath = $"{ Application.persistentDataPath }/";
#endif
    #endregion

    #region 扩展名
    public const string SceneExpandedName = ".unity3d";
    public const string OtherExpandedName = ".assetbundle";
    #endregion

    #region 场景
    public static readonly string SelectRoleScene = $"{ RootPath }/Scene/SelectRoleScene{ SceneExpandedName }";
    #endregion

    #region 角色
    public static readonly string RoleRootPath = $"{ RootPath }/Role/";
    #endregion
}