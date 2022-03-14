//===============================================
//作    者：
//创建时间：2022-02-24 10:46:29
//备    注：
//===============================================

//const常量变量会被编译器用常量直接替换，类似于宏

/// <summary>
/// 层的名字
/// </summary>
public class LayerName
{
    public const string Ground = "Ground";//地面
    public const string Monster = "Monster";//怪物
}

/// <summary>
/// 场景UI根节点名称
/// </summary>
public class SceneUIName
{
    public const string LogOn = "UI Root_LogOnScene";//登陆场景UI
}

/// <summary>
/// 窗口名称
/// </summary>
public class WindowName
{
    public const string LogOn = "PanLogOn";//登陆
    public const string Register = "PanReg";//注册
}

/// <summary>
/// UI容器类型
/// </summary>
public enum WindowUIContainerType
{
    //居中、左上、右上、左下、右下
    Center, TopLeft, TopRight, BottomLeft, BottomRight
}

/// <summary>
/// 窗口打开方式
/// </summary>
public enum WindowShowStyle
{
    //默认、缩放、从上往下、从下往上、从左往右、从右往左
    Normal, Scale, FromTop, FromDown, FromLeft, FromRight
}

public class SceneName
{
    public const string Loading = "Loading";
    public const string Logon = "LogOn";
    public const string City = "GameScene_HuPaoCun";
}

public class RegisterLogonKey
{
    public const string MMO_NICKNAME = "MMO_NICKNAME";
    public const string MMO_PWD = "MMO_PWD";
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
