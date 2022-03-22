//===============================================
//作    者：
//创建时间：2022-03-22 15:54:04
//备    注：
//===============================================
using System.Collections.Generic;

public class EventDispatcher : Singleton<EventDispatcher>
{
    public delegate void EventHanlder(byte[] buffer);

    private Dictionary<ushort, HashSet<EventHanlder>> m_HandlerDic = new Dictionary<ushort, HashSet<EventHanlder>>();

    public void AddListener(ushort protoCode, EventHanlder handler)
    {
        if(m_HandlerDic.ContainsKey(protoCode))
        {
            m_HandlerDic[protoCode].Add(handler);
        }
        else
        {
            HashSet<EventHanlder> handlerSet = new HashSet<EventHanlder>();
            handlerSet.Add(handler);
            m_HandlerDic[protoCode] = handlerSet;
        }
    }

    public void RemoveListener(ushort protoCode, EventHanlder handler)
    {
        if (m_HandlerDic.ContainsKey(protoCode))
        {
            m_HandlerDic[protoCode].Remove(handler);
        }
    }

    public void Dispatch(ushort protoCode, byte[] buffer)
    {
        if (m_HandlerDic.ContainsKey(protoCode))
        {
            HashSet<EventHanlder> handlerSet = m_HandlerDic[protoCode];
            foreach(EventHanlder handler in handlerSet)
            {
                handler(buffer);
            }
        }
    }
}