//===============================================
//作    者：
//创建时间：2022-04-21 16:30:11
//备    注：
//===============================================
using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 必须实现IDragHandler，否则开始拖拽和结束拖拽的回调不会触发
/// </summary>
public class SelectRoleDragAreaController : MonoBehaviour, IBeginDragHandler, IDragHandler ,IEndDragHandler
{
    private float m_BeginDragPosX;

    /// <summary>
    /// 水平拖拽结束事件
    /// </summary>
    public event Action<UIDirection> EndHorizontalDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_BeginDragPosX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        float delta = m_BeginDragPosX - eventData.position.x;
        if(delta > 20)
        {
            //向左
            EndHorizontalDrag?.Invoke(UIDirection.LEFT);
        }
        else if(delta < -20)
        {
            //向右
            EndHorizontalDrag?.Invoke(UIDirection.RIGHT);
        }
    }
}
