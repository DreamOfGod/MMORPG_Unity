//===============================================
//作    者：
//创建时间：2022-02-24 10:41:01
//备    注：
//===============================================
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口管理器
/// </summary>
public class WindowUIMgr : Singleton<WindowUIMgr>
{
    /// <summary>
    /// 窗口缓存
    /// </summary>
    private Dictionary<string, GameObject> m_DicWindow = new Dictionary<string, GameObject>();

    /// <summary>
    /// 默认动画曲线
    /// </summary>
    private static AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.8f, 1.1f), new Keyframe(1, 1));

    /// <summary>
    /// 按照配置打开窗口
    /// </summary>
    /// <param name="name">窗口名称</param>
    /// <returns></returns>
    public GameObject OpenWindow(string name)
    {
        if (m_DicWindow.ContainsKey(name))
        {
            return null;
        }
        GameObject obj = ResourcesMgr.Instance.LoadUIWindows(name, true);
        m_DicWindow.Add(name, obj);

        Transform parent = null;
        UIWindowBase windowBase = obj.GetComponent<UIWindowBase>();
        switch(windowBase.containerType)
        {
            case WindowUIContainerType.Center:
                parent = UISceneBase.Instance.Container_Center;
                break;
            case WindowUIContainerType.TopLeft:
                parent = UISceneBase.Instance.Container_TL;
                break;
            case WindowUIContainerType.TopRight:
                parent = UISceneBase.Instance.Container_TR;
                break;
            case WindowUIContainerType.BottomLeft:
                parent = UISceneBase.Instance.Container_BL;
                break;
            case WindowUIContainerType.BottomRight:
                parent = UISceneBase.Instance.Container_BR;
                break;
        }
        obj.transform.parent = parent;

        switch(windowBase.showStyle)
        {
            case WindowShowStyle.Normal:
                ShowNormal(name);
                break;
            case WindowShowStyle.Scale:
                ShowScale(name);
                break;
            case WindowShowStyle.FromTop:
                ShowFromTop(name);
                break;
            case WindowShowStyle.FromDown:
                ShowFromDown(name);
                break;
            case WindowShowStyle.FromLeft:
                ShowFromLeft(name);
                break;
            case WindowShowStyle.FromRight:
                ShowFromRight(name);
                break;
        }

        return obj;
    }

    /// <summary>
    /// 按照配置关闭窗口
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void CloseWindow(string name) 
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        UIWindowBase windowBase = obj.GetComponent<UIWindowBase>();
        switch (windowBase.showStyle)
        {
            case WindowShowStyle.Normal:
                CloseNormal(name);
                break;
            case WindowShowStyle.Scale:
                CloseScale(name);
                break;
            case WindowShowStyle.FromTop:
                CloseToTop(name);
                break;
            case WindowShowStyle.FromDown:
                CloseToDown(name);
                break;
            case WindowShowStyle.FromLeft:
                CloseToLeft(name);
                break;
            case WindowShowStyle.FromRight:
                CloseToRight(name);
                break;
        }
    }

    /// <summary>
    /// 在中间打开窗口
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject OpenCenter(string name)
    {
        if(m_DicWindow.ContainsKey(name))
        {
            return null;
        }
        GameObject obj = ResourcesMgr.Instance.LoadUIWindows(name, true);
        m_DicWindow.Add(name, obj);
        obj.transform.parent = UISceneBase.Instance.Container_Center;
        return obj;
    }

    /// <summary>
    /// 在左上角打开窗口
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject OpenTopLeft(string name)
    {
        if (m_DicWindow.ContainsKey(name))
        {
            return null;
        }
        GameObject obj = ResourcesMgr.Instance.LoadUIWindows(name, true);
        m_DicWindow.Add(name, obj);
        obj.transform.parent = UISceneBase.Instance.Container_TL;
        return obj;
    }

    /// <summary>
    /// 在右上角打开窗口
    /// </summary>
    /// <param name="name">窗口名称</param>
    /// <returns></returns>
    public GameObject OpenTopRight(string name)
    {
        if (m_DicWindow.ContainsKey(name))
        {
            return null;
        }
        GameObject obj = ResourcesMgr.Instance.LoadUIWindows(name, true);
        m_DicWindow.Add(name, obj);
        obj.transform.parent = UISceneBase.Instance.Container_TR;
        return obj;
    }

    /// <summary>
    /// 在左下角打开窗口
    /// </summary>
    /// <param name="name">窗口名称</param>
    /// <returns></returns>
    public GameObject OpenBottomLeft(string name)
    {
        if (m_DicWindow.ContainsKey(name))
        {
            return null;
        }
        GameObject obj = ResourcesMgr.Instance.LoadUIWindows(name, true);
        m_DicWindow.Add(name, obj);
        obj.transform.parent = UISceneBase.Instance.Container_BL;
        return obj;
    }

    /// <summary>
    /// 在右下角打开窗口
    /// </summary>
    /// <param name="name">窗口名称</param>
    /// <returns></returns>
    public GameObject OpenBottomRight(string name)
    {
        if (m_DicWindow.ContainsKey(name))
        {
            return null;
        }
        GameObject obj = ResourcesMgr.Instance.LoadUIWindows(name, true);
        m_DicWindow.Add(name, obj);
        obj.transform.parent = UISceneBase.Instance.Container_BR;
        return obj;
    }

    /// <summary>
    /// 直接显示
    /// </summary>
    /// <param name="name"></param>
    public void ShowNormal(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        obj.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 直接关闭
    /// </summary>
    /// <param name="name"></param>
    public void CloseNormal(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        m_DicWindow.Remove(name);
        GameObject.Destroy(obj);
    }

    /// <summary>
    /// 放大显示
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void ShowScale(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        obj.transform.localScale = Vector3.zero;
        TweenScale ts = obj.GetOrCreateComponent<TweenScale>();
        ts.animationCurve = animationCurve;
        ts.from = obj.transform.localScale;
        ts.to = Vector3.one;
        ts.duration = obj.GetComponent<UIWindowBase>().duration;
        ts.ResetToBeginning();
        ts.PlayForward();
    }

    /// <summary>
    /// 缩小关闭
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void CloseScale(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        TweenScale ts = obj.GetOrCreateComponent<TweenScale>();
        ts.animationCurve = animationCurve;
        ts.from = obj.transform.localScale;
        ts.to = Vector3.zero;
        ts.duration = obj.GetComponent<UIWindowBase>().duration;
        ts.AddOnFinished(() => {
            m_DicWindow.Remove(name);
            Object.Destroy(obj);
        });
        ts.ResetToBeginning();
        ts.PlayForward();
    }

    /// <summary>
    /// 从左往右显示
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void ShowFromLeft(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3(-1400, 0, 0);
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        tp.from = obj.transform.localPosition;
        tp.to = Vector3.zero;
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    /// <summary>
    /// 往左移动后关闭
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void CloseToLeft(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        Vector3 pos = obj.transform.localPosition;
        tp.from = pos;
        tp.to = new Vector3(pos.x - 1400, pos.y, pos.z);
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.AddOnFinished(() => {
            m_DicWindow.Remove(name);
            GameObject.Destroy(obj);
        });
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    /// <summary>
    /// 从右往左显示
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void ShowFromRight(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3(1400, 0, 0);
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        tp.from = obj.transform.localPosition;
        tp.to = Vector3.zero;
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    /// <summary>
    /// 往右移动后关闭
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void CloseToRight(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        Vector3 pos = obj.transform.localPosition;
        tp.from = pos;
        tp.to = new Vector3(pos.x + 1400, pos.y, pos.z);
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.AddOnFinished(() => {
            m_DicWindow.Remove(name);
            GameObject.Destroy(obj);
        });
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    /// <summary>
    /// 从上往下显示
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void ShowFromTop(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3(0, 1000, 0);
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        tp.from = obj.transform.localPosition;
        tp.to = Vector3.zero;
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    /// <summary>
    /// 往上移动后关闭
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void CloseToTop(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        Vector3 pos = obj.transform.localPosition;
        tp.from = pos;
        tp.to = new Vector3(pos.x, pos.y + 1000, pos.z);
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.AddOnFinished(() => {
            m_DicWindow.Remove(name);
            GameObject.Destroy(obj);
        });
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    /// <summary>
    /// 从下往上显示
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void ShowFromDown(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3(0, -1000, 0);
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        tp.from = obj.transform.localPosition;
        tp.to = Vector3.zero;
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    /// <summary>
    /// 往下移动后关闭
    /// </summary>
    /// <param name="name">窗口名称</param>
    public void CloseToDown(string name)
    {
        if (!m_DicWindow.ContainsKey(name))
        {
            return;
        }
        GameObject obj = m_DicWindow[name];
        TweenPosition tp = obj.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = animationCurve;
        Vector3 pos = obj.transform.localPosition;
        tp.from = pos;
        tp.to = new Vector3(pos.x, pos.y - 1000, pos.z);
        tp.duration = obj.GetComponent<UIWindowBase>().duration;
        tp.AddOnFinished(() => {
            m_DicWindow.Remove(name);
            GameObject.Destroy(obj);
        });
        tp.ResetToBeginning();
        tp.PlayForward();
    }
}