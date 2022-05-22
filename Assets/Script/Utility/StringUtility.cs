//===============================================
//作    者：
//创建时间：2022-02-19 20:22:02
//备    注：
//===============================================

using System;
using System.Text;

public static class StringUtility
{
    /// <summary>
    /// 字符串转换成int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        int n;
        int.TryParse(str, out n);
        return n;
    }

    /// <summary>
    /// 字符串转换成long
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long ToLong(this string str)
    {
        long n;
        long.TryParse(str, out n);
        return n;
    }

    /// <summary>
    /// 字符串转换成float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float n;
        float.TryParse(str, out n);
        return n;
    }

    /// <summary>
    /// 随机中文
    /// </summary>
    /// <returns></returns>
    public static string RandomChinese(uint length)
    {
        StringBuilder chinese = new StringBuilder();
        var random = new Random();
        while(length-- > 0)
        {
            chinese.Append(Convert.ToChar(random.Next(0x2e80, 0x2fd5)));
        }
        return chinese.ToString();
    }
}