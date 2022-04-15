//===============================================
//作    者：
//创建时间：2022-04-13 00:52:42
//备    注：
//===============================================

/// <summary>
/// Http服务器返回的数据
/// </summary>
/// <typeparam name="DataType">数据的类型</typeparam>
public struct ResponseData<DataType> {
    /// <summary>
    /// 编号
    /// </summary>
    public int Code;
    /// <summary>
    /// 数据
    /// </summary>
    public DataType Data;
    /// <summary>
    /// 错误信息
    /// </summary>
    public string Error;
}