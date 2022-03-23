//===============================================
//作    者：
//创建时间：2022-03-23 15:10:11
//备    注：
//===============================================

using System.Collections.Generic;
/// <summary>
/// AssetBundle实体
/// </summary>
public class AssetBundleEntity
{
    /// <summary>
    /// 用于打包时候选定 唯一Key
    /// </summary>
    public string Key;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 标记
    /// </summary>
    public string Tag;

    /// <summary>
    /// 版本号
    /// </summary>
    public int Version;

    /// <summary>
    /// 大小（K）
    /// </summary>
    public long Size;

    /// <summary>
    /// 打包保存的路径
    /// </summary>
    public string ToPath;

    private List<string> m_PathList = new List<string>();

    /// <summary>
    /// 路径集合
    /// </summary>
    public List<string> PathList { get { return m_PathList; } }
}
