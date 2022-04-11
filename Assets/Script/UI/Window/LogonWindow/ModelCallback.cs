//===============================================
//作    者：
//创建时间：2022-04-11 15:07:33
//备    注：
//===============================================
using UnityEngine.Networking;

public delegate void ModelCallback<RetValType>(UnityWebRequest.Result result, MFReturnValue<RetValType> ret = null);