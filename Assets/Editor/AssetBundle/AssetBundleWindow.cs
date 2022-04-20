//===============================================
//作    者：
//创建时间：2022-03-23 15:29:34
//备    注：
//===============================================

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// AssetBundle管理窗口
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;

    private string[] arrTag = { "All", "Scene", "Role", "Effect", "Audio", "None" };
    private int tagIndex = 0;

    private string[] arrBuildTarget = { "Windows", "Android", "IOS" };

#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0;
#elif UNITY_ANDROID
    private BuildTarget target = BuildTarget.Android;
    private int buildTargetIndex = 1;
#elif UNITY_IPHONE
    private BuildTarget target = BuildTarget.IOS;
    private int buildTargetIndex = 2;
#endif

    private Vector2 pos;

    private void OnEnable()
    {
        //Application.dataPath是游戏数据路径，取决于运行的平台。在编辑器环境中是项目的Assets目录。
        //在编辑器环境中可以读写该路径下的内容，在其它环境中不要读写
        string xmlPath = @$"{ Application.dataPath }\Editor\AssetBundle\AssetBundleConfig.xml";
        m_List = AssetBundleDAL.GetList(xmlPath);

        m_Dic = new Dictionary<string, bool>();

        for (int i = 0; i < m_List.Count; ++i)
        {
            m_Dic[m_List[i].Key] = true;
        }
    }

    /// <summary>
    /// 绘制窗口
    /// </summary>
    private void OnGUI()
    {
        if(m_List == null)
        {
            return;
        }

        #region 按钮行
        GUILayout.BeginHorizontal("box");

        int selectTag = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        if(selectTag != tagIndex)
        {
            tagIndex = selectTag;
            UpdateListSelectToggle();
        }

        int selectBuildTarget = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(100));
        if(selectBuildTarget != buildTargetIndex)
        {
            buildTargetIndex = selectBuildTarget;
            UpdateSelectBuildTarget();
        }

        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallback;
        }

        if (GUILayout.Button("清空AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallback;
        }

        EditorGUILayout.Space();

        GUILayout.EndHorizontal();
        #endregion

        #region 标题行
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("保存路径", GUILayout.Width(200));
        GUILayout.Label("版本", GUILayout.Width(100));
        GUILayout.Label("大小", GUILayout.Width(100));
        GUILayout.EndHorizontal();
        #endregion

        #region 配置内容
        pos = EditorGUILayout.BeginScrollView(pos);
        for (int i = 0; i < m_List.Count; ++i)
        {
            AssetBundleEntity entity = m_List[i];

            GUILayout.BeginHorizontal("box");
            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.ToPath, GUILayout.Width(200));
            GUILayout.Label(entity.Version.ToString(), GUILayout.Width(100));
            GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();

            foreach(string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        #endregion
    }

    /// <summary>
    /// 清空AssetBundle
    /// </summary>
    private void OnClearAssetBundleCallback()
    {
        string path = @$"{ Application.dataPath }/../AssetBundles/{ arrBuildTarget[buildTargetIndex] }";
        if(Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Debug.Log("清空完毕");
    }

    /// <summary>
    /// 打包回调
    /// </summary>
    private void OnAssetBundleCallback()
    {
        List<AssetBundleEntity> listNeedBuild = new List<AssetBundleEntity>();

        foreach(AssetBundleEntity entity in m_List)
        {
            if(m_Dic[entity.Key])
            {
                listNeedBuild.Add(entity);
            }
        }

        for(int i = 0; i < listNeedBuild.Count; ++i)
        {
            Debug.Log($"正在打包{ i + 1 }/{ listNeedBuild.Count }");
            BuildAssetBundle(listNeedBuild[i]);
        }

        Debug.Log("打包完毕");
    }

    private void BuildAssetBundle(AssetBundleEntity entity)
    {
        AssetBundleBuild[] arrBuild = new AssetBundleBuild[1];
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = entity.Name;

        build.assetBundleVariant = entity.Tag == "Scene" ? "unity3d" : "assetbundle";

        build.assetNames = entity.PathList.ToArray();

        arrBuild[0] = build;

        string toPath = $@"{ Application.dataPath }/../AssetBundles/{ arrBuildTarget[buildTargetIndex] }/{ entity.ToPath }";

        if(!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }

        BuildPipeline.BuildAssetBundles(toPath, arrBuild, BuildAssetBundleOptions.None, target);
    }

    /// <summary>
    /// 更新列表选择单选框
    /// </summary>
    private void UpdateListSelectToggle()
    {
        switch (tagIndex)
        {
            case 0: //全选
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;
            case 1://Scene
            case 2://Role
            case 3://Effect
            case 4://Audio
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag == arrTag[tagIndex];
                }
                break;
            case 5://None
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
        Debug.LogFormat($"当前选择的Tag：{ arrTag[tagIndex] }");
    }

    /// <summary>
    /// 更新选择的构建目标平台
    /// </summary>
    private void UpdateSelectBuildTarget()
    {
        switch (buildTargetIndex)
        {
            case 0: //Windows
                target = BuildTarget.StandaloneWindows;
                break;
            case 1: //Android
                target = BuildTarget.Android;
                break;
            case 2: //IOS
                target = BuildTarget.iOS;
                break;
        }
        Debug.LogFormat($"当前选择的BuildTarget：{ arrBuildTarget[buildTargetIndex] }");
    }
}