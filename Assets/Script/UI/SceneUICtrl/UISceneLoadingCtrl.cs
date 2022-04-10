//===============================================
//作    者：
//创建时间：2022-03-07 14:16:56
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneLoadingCtrl : MonoBehaviour
{
    [SerializeField]
    private UIProgressBar m_Progress;

    [SerializeField]
    private UILabel m_LblProgress;

    [SerializeField]
    private Transform m_TransProgressLight;

    public void SetProgressValue(int percent)
    {
        float progress = percent * 0.01f;
        m_Progress.value = progress;
        m_LblProgress.text = string.Format("{0}%", percent);
        m_TransProgressLight.localPosition = new Vector3(-390 + 890 * progress, 0, 0);
    }
}