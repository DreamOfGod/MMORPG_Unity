//===============================================
//作    者：
//创建时间：2022-02-24 10:46:29
//备    注：
//===============================================

//const常量变量会被编译器用常量直接替换，类似于宏

using UnityEngine;
/// <summary>
/// 层的名字
/// </summary>
public class LayerName
{
    public const string Ground = "Ground";//地面
    public const string Monster = "Monster";//怪物
}

/// <summary>
/// 排序层名字
/// </summary>
public class SortingLayerName
{
    public const string Window = "Window";//窗口
}

public class SceneName
{
    public const string Loading = "Loading";
    public const string Logon = "LogOn";
    public const string SelectRole = "SelectRole";
    public const string HuPaoCun = "HuPaoCun";
}

public class AnimStateConditionName
{
    public const string ToIdleNormal = "ToIdleNormal";
    public const string ToIdleFight = "ToIdleFight"; 
    public const string ToRun = "ToRun"; 
    public const string ToHurt = "ToHurt";
    public const string ToDie = "ToDie";
    public const string ToPhyAttack = "ToPhyAttack";
}