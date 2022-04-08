//===============================================
//作    者：
//创建时间：2022-04-07 13:48:21
//备    注：
//===============================================
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 视图基类
/// </summary>
public class ViewBase : MonoBehaviour
{
    [SerializeField]
    public float ViewDuration = 0.2f;
    [SerializeField]
    public Ease ViewEase = Ease.Linear;
    [SerializeField]
    public TweenCallback OnViewShowFinish;
    [SerializeField]
    public TweenCallback OnViewCloseFinish;

    private static int GlobalOrderInWindowLayer = 1;

    private void OnCloseComplete()
    {
        Destroy(gameObject);
        OnViewCloseFinish();
    }

    #region 放大显示、缩小关闭
    public void ZoomInShow()
    {
        transform.localScale = Vector3.zero;
        Tweener tweener = transform.DOScale(Vector3.one, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnViewShowFinish);
    }

    public void ZoomOutClose()
    {
        Tweener tweener = transform.DOScale(Vector3.zero, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnCloseComplete);
    }
    #endregion

    #region 从左往右移动显示、从右往左移动关闭
    public void MoveFromLeftToRightShow()
    {
        transform.localPosition = new Vector3(-1000, 0, 0);
        Tweener tweener = transform.DOLocalMoveX(0, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnViewShowFinish);
    }

    public void MoveFromRightToLeftClose()
    {
        Tweener tweener = transform.DOLocalMoveX(-1000, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnCloseComplete);
    }
    #endregion

    #region 从右往左移动显示、从左往右移动关闭
    public void MoveFromRightToLeftShow()
    {
        transform.localPosition = new Vector3(1000, 0, 0);
        Tweener tweener = transform.DOLocalMoveX(0, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnViewShowFinish);
    }

    public void MoveFromLeftToRightClose()
    {
        Tweener tweener = transform.DOLocalMoveX(1000, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnCloseComplete);
    }
    #endregion

    #region 从上往下移动显示、从下往上移动关闭
    public void MoveFromTopToBottomShow()
    {
        transform.localPosition = new Vector3(0, 1000, 0);
        Tweener tweener = transform.DOLocalMoveY(0, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnViewShowFinish);
    }

    public void MoveFromBottomToTopClose()
    {
        Tweener tweener = transform.DOLocalMoveY(1000, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnCloseComplete);
    }
    #endregion

    #region 从下往上移动显示、从上往下移动关闭
    public void MoveFromBottomToTopShow()
    {
        transform.localPosition = new Vector3(0, -1000, 0);
        Tweener tweener = transform.DOLocalMoveY(0, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnViewShowFinish);
    }

    public void MoveFromTopToBottomClose()
    {
        Tweener tweener = transform.DOLocalMoveY(-1000, ViewDuration);
        tweener.SetEase(ViewEase);
        tweener.OnComplete(OnCloseComplete);
    }
    #endregion

    public static GameObject InstantiateView(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(parent, false);
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

    public static ViewBase ZoomInShowView(string path, Transform parent)
    {
        GameObject obj = InstantiateView(path, parent);
        ViewBase view = obj.GetComponent<ViewBase>();
        view.ZoomInShow();
        return view;
    }

    public static ViewBase MoveFromLeftToRightShowView(string path, Transform parent)
    {
        GameObject obj = InstantiateView(path, parent);
        ViewBase view = obj.GetComponent<ViewBase>();
        view.MoveFromLeftToRightShow();
        return view;
    }

    public static ViewBase MoveFromRightToLeftShowView(string path, Transform parent)
    {
        GameObject obj = InstantiateView(path, parent);
        ViewBase view = obj.GetComponent<ViewBase>();
        view.MoveFromRightToLeftShow();
        return view;
    }

    public static ViewBase MoveFromTopToBottomShowView(string path, Transform parent)
    {
        GameObject obj = InstantiateView(path, parent);
        ViewBase view = obj.GetComponent<ViewBase>();
        view.MoveFromTopToBottomShow();
        return view;
    }

    public static ViewBase MoveFromBottomToTopShowView(string path, Transform parent)
    {
        GameObject obj = InstantiateView(path, parent);
        ViewBase view = obj.GetComponent<ViewBase>();
        view.MoveFromBottomToTopShow();
        return view;
    }
}