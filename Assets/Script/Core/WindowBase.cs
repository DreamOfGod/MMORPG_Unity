//===============================================
//作    者：
//创建时间：2022-04-07 13:48:21
//备    注：
//===============================================
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 窗口基类
/// </summary>
public class WindowBase : MonoBehaviour
{
    #region 窗口动画相关的属性
    /// <summary>
    /// 窗口动画目标
    /// </summary>
    [SerializeField]
    public Transform WindowAnimationTarget;
    /// <summary>
    /// 窗口动画时长
    /// </summary>
    [SerializeField]
    public float WindowAnimationDuration = 0.2f;
    /// <summary>
    /// 窗口动画缓动类型
    /// </summary>
    [SerializeField]
    public Ease WindowAnimationEase = Ease.Linear;
    /// <summary>
    ///窗口显示动画结束后的回调
    /// </summary>
    [SerializeField]
    public TweenCallback OnWindowShowFinish;
    /// <summary>
    /// 窗口关闭动画结束后的回调
    /// </summary>
    [SerializeField]
    public TweenCallback OnWindowCloseFinish;
    #endregion

    //窗口排序层的全局的层内次序，新窗口的显示层级
    private static int GlobalOrderInWindowLayer = 1;

    #region 销毁窗口DestroyWindow
    private void DestroyWindow()
    {
        if (OnWindowCloseFinish != null)
        {
            OnWindowCloseFinish();
        }
        Destroy(gameObject);
    }
    #endregion

    #region 直接显示ShowImmediately、直接关闭CloseImmediately
    public void ShowImmediately()
    {
        WindowAnimationTarget.localPosition = Vector3.zero;
        if(OnWindowShowFinish != null)
        {
            OnWindowShowFinish();
        }
    }

    public void CloseImmediately()
    {
        DestroyWindow();
    }
    #endregion

    #region 放大显示ZoomInShow、缩小关闭ZoomOutClose
    public void ZoomInShow()
    {
        WindowAnimationTarget.localPosition = Vector3.zero;
        WindowAnimationTarget.localScale = Vector3.zero;
        Tweener tweener = WindowAnimationTarget.DOScale(Vector3.one, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(OnWindowShowFinish);
    }

    public void ZoomOutClose()
    {
        Tweener tweener = WindowAnimationTarget.DOScale(Vector3.zero, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(DestroyWindow);
    }
    #endregion

    #region 从左往右移动显示MoveFromLeftToRightShow、从右往左移动关闭MoveFromRightToLeftClose
    public void MoveFromLeftToRightShow()
    {
        WindowAnimationTarget.localPosition = new Vector3(-1000, 0, 0);
        Tweener tweener = WindowAnimationTarget.DOLocalMoveX(0, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(OnWindowShowFinish);
    }

    public void MoveFromRightToLeftClose()
    {
        Tweener tweener = WindowAnimationTarget.DOLocalMoveX(-1000, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(DestroyWindow);
    }
    #endregion

    #region 从右往左移动显示MoveFromRightToLeftShow、从左往右移动关闭MoveFromLeftToRightClose
    public void MoveFromRightToLeftShow()
    {
        WindowAnimationTarget.localPosition = new Vector3(1000, 0, 0);
        Tweener tweener = WindowAnimationTarget.DOLocalMoveX(0, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(OnWindowShowFinish);
    }

    public void MoveFromLeftToRightClose()
    {
        Tweener tweener = WindowAnimationTarget.DOLocalMoveX(1000, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(DestroyWindow);
    }
    #endregion

    #region 从上往下移动显示MoveFromTopToBottomShow、从下往上移动关闭MoveFromBottomToTopClose
    public void MoveFromTopToBottomShow()
    {
        WindowAnimationTarget.localPosition = new Vector3(0, 1000, 0);
        Tweener tweener = WindowAnimationTarget.DOLocalMoveY(0, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(OnWindowShowFinish);
    }

    public void MoveFromBottomToTopClose()
    {
        Tweener tweener = WindowAnimationTarget.DOLocalMoveY(1000, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(DestroyWindow);
    }
    #endregion

    #region 从下往上移动显示MoveFromBottomToTopShow、从上往下移动关闭MoveFromTopToBottomClose
    public void MoveFromBottomToTopShow()
    {
        WindowAnimationTarget.localPosition = new Vector3(0, -1000, 0);
        Tweener tweener = WindowAnimationTarget.DOLocalMoveY(0, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(OnWindowShowFinish);
    }

    public void MoveFromTopToBottomClose()
    {
        Tweener tweener = WindowAnimationTarget.DOLocalMoveY(-1000, WindowAnimationDuration);
        tweener.SetEase(WindowAnimationEase);
        tweener.OnComplete(DestroyWindow);
    }
    #endregion

    #region 克隆窗口InstantiateWindow
    public static GameObject InstantiateWindow(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(parent, false);
        obj.transform.localPosition = Vector3.zero;
        //设置子画布的排序层和层内层级
        Canvas canvas = obj.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = obj.AddComponent<Canvas>();
        }
        canvas.overrideSorting = true;
        canvas.sortingLayerName = SortingLayerName.Window;
        canvas.sortingOrder = GlobalOrderInWindowLayer++;
        //子画布需要GraphicRaycaster组件才能触发射线检测
        if (obj.GetComponent<GraphicRaycaster>() == null)
        {
            obj.AddComponent<GraphicRaycaster>();
        }
        return obj;
    }
    #endregion

    #region 用于打开视图的一些静态辅助方法
    public static WindowBase OpenWindowImmediately(string path, Transform parent)
    {
        GameObject obj = InstantiateWindow(path, parent);
        WindowBase window = obj.GetComponent<WindowBase>();
        window.ShowImmediately();
        return window;
    }

    public static WindowBase OpenWindowZoomInShow(string path, Transform parent)
    {
        GameObject obj = InstantiateWindow(path, parent);
        WindowBase window = obj.GetComponent<WindowBase>();
        window.ZoomInShow();
        return window;
    }

    public static WindowBase OpenWindowMoveFromLeftToRightShow(string path, Transform parent)
    {
        GameObject obj = InstantiateWindow(path, parent);
        WindowBase window = obj.GetComponent<WindowBase>();
        window.MoveFromLeftToRightShow();
        return window;
    }

    public static WindowBase OpenWindowMoveFromRightToLeftShow(string path, Transform parent)
    {
        GameObject obj = InstantiateWindow(path, parent);
        WindowBase window = obj.GetComponent<WindowBase>();
        window.MoveFromRightToLeftShow();
        return window;
    }

    public static WindowBase OpenWindowMoveFromTopToBottomShow(string path, Transform parent)
    {
        GameObject obj = InstantiateWindow(path, parent);
        WindowBase window = obj.GetComponent<WindowBase>();
        window.MoveFromTopToBottomShow();
        return window;
    }

    public static WindowBase OpenWindowMoveFromBottomToTopShow(string path, Transform parent)
    {
        GameObject obj = InstantiateWindow(path, parent);
        WindowBase window = obj.GetComponent<WindowBase>();
        window.MoveFromBottomToTopShow();
        return window;
    }
    #endregion
}