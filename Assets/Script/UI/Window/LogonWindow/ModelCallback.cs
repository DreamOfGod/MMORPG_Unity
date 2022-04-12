//===============================================
//作    者：
//创建时间：2022-04-11 15:07:33
//备    注：
//===============================================
using UnityEngine.Networking;

public delegate void ModelCallback<RespValType>(UnityWebRequest.Result result, ResponseValue<RespValType> ret = null);