//===============================================
//作    者：
//创建时间：2022-03-23 13:39:32
//备    注：
//===============================================
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingsWindow : EditorWindow
{
    private List<MacroItem> m_List = new List<MacroItem>();

    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();

    private string m_Macro = null;

    private void OnEnable()
    {
        m_List.Add(new MacroItem() { Name = "DEBUG_MODE", DisplayName = "调式模式", IsDebug = true, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "DEBUG_LOG", DisplayName = "打印日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "STAT_TD", DisplayName = "开启统计", IsDebug = false, IsRelease = true });

        m_Macro = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);

        for (int i = 0; i < m_List.Count; ++i)
        {
            m_Dic[m_List[i].Name] = !string.IsNullOrEmpty(m_Macro) && m_Macro.IndexOf(m_List[i].Name) != -1;
        }
    }

    /// <summary>
    /// 绘制界面
    /// </summary>
    private void OnGUI()
    {
        for (int i = 0; i < m_List.Count; ++i)
        {
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveMacro();
        }

        if (GUILayout.Button("调试模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; ++i)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            SaveMacro();
        }

        if (GUILayout.Button("发布模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; ++i)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            }
            SaveMacro();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SaveMacro()
    {
        m_Macro = string.Empty;
        foreach(var item in m_Dic)
        {
            if(item.Value)
            {
                m_Macro += string.Format("{0};", item.Key);
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macro);
    }

    public class MacroItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// 是否调试
        /// </summary>
        public bool IsDebug;

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsRelease;
    }
}
