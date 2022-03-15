//===============================================
//作    者：
//创建时间：2022-02-19 20:22:02
//备    注：
//===============================================

public static class StringUtils
{
    /// <summary>
    /// 字符串转换成int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)//扩展方法
    {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }

    /// <summary>
    /// 字符串转换成float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float temp = 0;
        float.TryParse(str, out temp);
        return temp;
    }
}
