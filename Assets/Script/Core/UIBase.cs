//===============================================
//作    者：
//创建时间：2022-03-06 22:21:49
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// UI基类
/// </summary>
public class UIBase : MonoBehaviour
{
    protected virtual void Start()
    {
        UIButton[] btnArr = GetComponentsInChildren<UIButton>();
        for (int i = 0; i < btnArr.Length; i++)
        {
            UIEventListener.Get(btnArr[i].gameObject).onClick = OnBtnClick;
        }
    }

    protected virtual void OnBtnClick(GameObject go) { }
}
