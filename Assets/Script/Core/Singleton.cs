//===============================================
//作    者：
//创建时间：2022-02-23 16:50:48
//备    注：
//===============================================

/// <summary>
/// 单例基类
/// </summary>
/// <typeparam name="Subclass">子类类型</typeparam>
public abstract class Singleton<Subclass> where Subclass : new()
{
    private static Subclass instance;

    public static Subclass Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Subclass();
            }
            return instance;
        }
    }
}
