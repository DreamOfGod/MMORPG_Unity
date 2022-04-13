//===============================================
//作    者：
//创建时间：2022-04-13 14:53:24
//备    注：
//===============================================
using UnityEngine.Networking;

/// <summary>
/// 请求结果
/// </summary>
/// <typeparam name="RespValType">响应值中的值类型</typeparam>
public class RequestResult<RespValType>
{
    /// <summary>
    /// 结果
    /// </summary>
    public UnityWebRequest.Result Result;
    /// <summary>
    /// 响应值
    /// </summary>
    public ResponseValue<RespValType> ResponseValue;

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess
    {
        get
        {
            return Result == UnityWebRequest.Result.Success;
        }
    }

    public RequestResult() { }

    public RequestResult(UnityWebRequest.Result result, ResponseValue<RespValType> responseValue = null)
    {
        Result = result;
        ResponseValue = responseValue;
    }
}
