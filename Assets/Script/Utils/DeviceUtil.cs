//===============================================
//作    者：
//创建时间：2022-04-10 14:42:29
//备    注：
//===============================================
using UnityEngine;

public class DeviceUtil
{
    /// <summary>
    /// 设备型号
    /// </summary>
    public static string DeviceModel
    {
        get
        {
#if !UNITY_EDITOR && UNITY_IPHONE
            return Device.generation.ToString();
#else
            return SystemInfo.deviceModel;
#endif
        }
    }
}
