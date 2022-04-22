//===============================================
//作    者：
//创建时间：2022-04-22 14:56:16
//备    注：
//===============================================
using System.Collections.Generic;

/// <summary>
/// 事件派发者
/// </summary>
public class EventDispatcher<TEventID>
{
    /// <summary>
    /// 具有可变参数的事件处理器委托类型
    /// </summary>
    /// <param name="args"></param>
    public delegate void EventHandler(params object[] args);
    /// <summary>
    /// 事件处理器字典
    /// </summary>
    private Dictionary<TEventID, HashSet<EventHandler>> m_HandlerDic = new Dictionary<TEventID, HashSet<EventHandler>>();

    /// <summary>
    /// 添加事件处理器
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="handler"></param>
    public void AddListener(TEventID eventID, EventHandler handler)
    {
        if (m_HandlerDic.ContainsKey(eventID))
        {
            m_HandlerDic[eventID].Add(handler);
        }
        else
        {
            var handlerSet = new HashSet<EventHandler>();
            handlerSet.Add(handler);
            m_HandlerDic[eventID] = handlerSet;
        }
    }

    /// <summary>
    /// 移除事件处理器
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="handler"></param>
    public void RemoveListener(TEventID eventID, EventHandler handler)
    {
        if (m_HandlerDic.ContainsKey(eventID))
        {
            m_HandlerDic[eventID].Remove(handler);
        }
    }

    /// <summary>
    /// 派发事件
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="args"></param>
    public void Dispatch(TEventID eventID, params object[] args)
    {
        if (m_HandlerDic.ContainsKey(eventID))
        {
            var handlerSet = m_HandlerDic[eventID];
            foreach (var handler in handlerSet)
            {
                handler(args);
            }
        }
    }
}