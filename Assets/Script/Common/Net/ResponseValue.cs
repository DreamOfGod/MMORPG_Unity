//===============================================
//作    者：
//创建时间：2022-04-13 00:52:42
//备    注：
//===============================================

/// <summary>
/// Http返回值
/// </summary>
/// <typeparam name="ValueType">值类型</typeparam>
public class ResponseValue<ValueType> {
    /// <summary>
    /// 编号
    /// </summary>
    public int Code;
    /// <summary>
    /// 值
    /// </summary>
    public ValueType Value;
    /// <summary>
    /// 错误信息
    /// </summary>
    public string Error;
}