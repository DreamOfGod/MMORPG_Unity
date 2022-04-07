//===============================================
//作    者：
//创建时间：2022-04-07 13:48:21
//备    注：
//===============================================
using DG.Tweening;
using UnityEngine;

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
        tweener.OnComplete(OnViewCloseFinish);
    }
}