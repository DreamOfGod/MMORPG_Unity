//===============================================
//作    者：
//创建时间：2022-03-17 13:44:24
//备    注：
//===============================================
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// Socket帮助类
/// </summary>
public class SocketHelper: MonoBehaviour
{
    #region 单例
    private SocketHelper() { }
    private static SocketHelper instance;
    public static SocketHelper Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject(nameof(SocketHelper));
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<SocketHelper>();
            }
            return instance;
        }
    }
    #endregion

    #region Socket消息派发
    //消息处理器字典
    private Dictionary<ushort, HashSet<Action<byte[]>>> m_SocketMsgHandlerDic = new Dictionary<ushort, HashSet<Action<byte[]>>>();

    /// <summary>
    /// 添加Socket消息监听器
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void AddListener(ushort protoCode, Action<byte[]> handler)
    {
        if (m_SocketMsgHandlerDic.ContainsKey(protoCode))
        {
            m_SocketMsgHandlerDic[protoCode].Add(handler);
        }
        else
        {
            var handlerSet = new HashSet<Action<byte[]>>();
            handlerSet.Add(handler);
            m_SocketMsgHandlerDic[protoCode] = handlerSet;
        }
    }

    /// <summary>
    /// 移除Socket消息监听器
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void RemoveListener(ushort protoCode, Action<byte[]> handler)
    {
        if (m_SocketMsgHandlerDic.ContainsKey(protoCode))
        {
            m_SocketMsgHandlerDic[protoCode].Remove(handler);
        }
    }

    //派发消息
    private void Dispatch(ushort protoCode, byte[] buffer)
    {
        if (m_SocketMsgHandlerDic.ContainsKey(protoCode))
        {
            
            var handlerSet = m_SocketMsgHandlerDic[protoCode];
            if(handlerSet.Count > 0)
            {
                DebugLogger.Log($"派发Socket消息，协议ID：{ protoCode }");
            }
            else
            {
                DebugLogger.Log($"消息没有处理器，协议ID：{ protoCode }");
            }
            foreach (var handler in handlerSet)
            {
                handler(buffer);
            }
        }
        else
        {
            DebugLogger.Log($"消息没有处理器，协议ID：{ protoCode }");
        }
    }
    #endregion

    #region 事件
    /// <summary>
    /// 连接成功事件
    /// </summary>
    public event Action ConnectSucceed;

    /// <summary>
    /// 连接失败事件
    /// </summary>
    public event Action ConnectFailed;

    /// <summary>
    /// 连接发生异常事件
    /// </summary>
    public event Action ConnectExceptionOccured;
    #endregion

    //socket对象
    private Socket m_Socket;

    //当前ip和端口号
    private IPEndPoint m_IPEndPoint;

    #region socket状态
    //socket状态枚举
    private enum SocketStatus
    {
        Disconnected,//未连接
        Connecting,//正在连接
        ConnectSucceed,//连接成功
        ConnectFailed,//连接失败
        Connected,//已连接
        ConnectExceptionOccurred,//连接发生异常
    }

    //socket连接状态
    private volatile SocketStatus m_SocketStatus = SocketStatus.Disconnected;
    #endregion

    //进行压缩的长度下限
    private const int m_compressLength = 200;

    //接收数据包的字节数组缓冲区
    private byte[] m_ReceiveBuffer = new byte[2048];

    //接收数据包的缓冲数据流
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

    #region 已接收的消息队列、待发送的消息队列
    //已接收的消息类型
    private struct ReceivedMsg
    {
        public ushort ProtoCode;//协议编号
        public byte[] ProtoContent;//协议数据
        public ReceivedMsg(ushort protoCode, byte[] protoContent)
        {
            ProtoCode = protoCode;
            ProtoContent = protoContent;
        }
    }

    //已接收的消息队列
    private Queue<ReceivedMsg> m_ReceivedMsgQueue = new Queue<ReceivedMsg>();

    //待发送的消息队列
    private Queue<byte[]> m_ToBeSentMsgQueue = new Queue<byte[]>();

    //是否正在发送消息
    private volatile bool m_IsSending = false;
    #endregion

    #region 异步连接：连接成功，派发ConnectSucceed；连接失败或出现异常，派发ConnectFailed。连接期间调用Close，不派发任何事件
    /// <summary>
    /// 异步连接
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">端口号</param>
    public void BeginConnect(string ip, int port)
    {
        m_SocketStatus = SocketStatus.Connecting;
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var iep = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
            m_Socket.BeginConnect(iep, ConnectCallback, iep);
        }
        catch(SocketException ex)
        {
            DebugLogger.Log($"尝试连接{ iep.Address }:{ iep.Port }时发生异常，exception：{ ex.Message }");
            m_SocketStatus = SocketStatus.Disconnected;
            ConnectFailed?.Invoke();
        }
    }

    //异步连接的回调
    private void ConnectCallback(IAsyncResult ar)
    {
        var iep = (IPEndPoint)ar;
        try
        {
            m_Socket.EndConnect(ar);
        }
        catch(SocketException ex)
        {
            m_SocketStatus = SocketStatus.ConnectFailed;
            DebugLogger.Log($"尝试连接{ iep.Address }:{ iep.Port }时发生异常，exception：{ ex.Message }");
            return;
        }

        if (m_Socket.Connected)
        {
            m_SocketStatus = SocketStatus.ConnectSucceed;
            DebugLogger.Log($"尝试连接{ iep.Address }:{ iep.Port }成功");
            lock (m_ToBeSentMsgQueue)
            {
                if(m_ToBeSentMsgQueue.Count > 0)
                {
                    SendNextMsg();
                    m_IsSending = true;
                }
            }
            BeginReceive();
        }
        else
        {
            m_SocketStatus = SocketStatus.ConnectFailed;
            DebugLogger.Log($"尝试连接{ iep.Address }:{ iep.Port }失败");
        }
    }
    #endregion

    #region 异步接收：出现异常或服务器断开连接，派发ConnectExceptionOccurred
    //异步接收
    private void BeginReceive()
    {
        try
        {
            m_Socket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }
        catch(SocketException ex)
        {
            m_SocketStatus = SocketStatus.ConnectExceptionOccurred;
            DebugLogger.Log($"与{ m_Socket.RemoteEndPoint }的连接异常，exception：{ ex.Message }");
        }
    }

    //接收数据的回调
    private void ReceiveCallback(IAsyncResult ar)
    {
        int count;
        try
        {
            count = m_Socket.EndReceive(ar);
        }
        catch (Exception ex)
        {
            m_SocketStatus = SocketStatus.ConnectExceptionOccurred;
            DebugLogger.Log($"与{ m_Socket.RemoteEndPoint }的连接异常，exception：{ ex.Message }");
            return;
        }

        if(count == 0)
        {
            m_SocketStatus = SocketStatus.ConnectExceptionOccurred;
            DebugLogger.Log($"服务器{ m_Socket.RemoteEndPoint }断开连接");
            return;
        }

        //把接收的字节写入字节流的尾部
        m_ReceiveMS.Write(m_ReceiveBuffer, 0, count);
        //字节流长度达到2个字节，至少消息的数据包长度接收完成
        if (m_ReceiveMS.Length >= 2)
        {
            m_ReceiveMS.Position = 0;
            while (true)
            {
                //读取内容的字节数
                ushort contentCount = m_ReceiveMS.ReadUShort();
                if (m_ReceiveMS.Length - m_ReceiveMS.Position >= contentCount)
                {
                    //消息的内容已经接收完成
                    //压缩标志
                    bool compressed = m_ReceiveMS.ReadBool();
                    //crc16校验码
                    ushort crc16 = m_ReceiveMS.ReadUShort();
                    //数据包
                    byte[] content = new byte[contentCount - 3];
                    m_ReceiveMS.Read(content, 0, content.Length);
                    //crc16校验
                    if (Crc16.CalculateCrc16(content) != crc16)
                    {

                    }
                    //数据包异或
                    SecurityUtil.XOR(content);
                    //解压
                    if (compressed)
                    {
                        content = ZlibHelper.DeCompressBytes(content);
                    }

                    MMO_MemoryStream ms = new MMO_MemoryStream(content);
                    //协议ID
                    ushort protoCode = ms.ReadUShort();
                    //协议内容
                    byte[] protoContent = new byte[contentCount - 2];
                    ms.Read(protoContent, 0, protoContent.Length);
                    //消息入队，等主线程处理
                    lock (m_ReceivedMsgQueue)
                    {
                        m_ReceivedMsgQueue.Enqueue(new ReceivedMsg(protoCode, protoContent));
                    }

                    long leftCount = m_ReceiveMS.Length - m_ReceiveMS.Position;
                    if (leftCount < 2)
                    {
                        //剩余不到2个字节，将剩余字节移到字节流开头，等内容接收完整再解析
                        byte[] buffer = m_ReceiveMS.GetBuffer();
                        long i = 0, j = m_ReceiveMS.Position;
                        while (j < m_ReceiveMS.Length)
                        {
                            buffer[i] = buffer[j];
                            ++i;
                            ++j;
                        }
                        m_ReceiveMS.SetLength(i);
                        break;
                    }
                }
                else
                {
                    //消息内容接收不完整，将剩余字节移到字节流开头，等内容接收完整再解析
                    byte[] buffer = m_ReceiveMS.GetBuffer();
                    long i = 0, j = m_ReceiveMS.Position;
                    while (j < m_ReceiveMS.Length)
                    {
                        buffer[i] = buffer[j];
                        ++i;
                        ++j;
                    }
                    m_ReceiveMS.SetLength(i);
                    break;
                }
            }
        }

        BeginReceive();
    }
    #endregion

    #region Update循环
    //主线程Update循环
    private void Update()
    {
        if(m_SocketStatus == SocketStatus.Connected)
        {
            if (m_ReceivedMsgQueue.Count > 0)//确实有消息入队，再尝试加锁，避免每帧都进行加锁
            {
                lock (m_ReceivedMsgQueue)
                {
                    while (m_ReceivedMsgQueue.Count > 0)
                    {
                        var msg = m_ReceivedMsgQueue.Dequeue();
                        Dispatch(msg.ProtoCode, msg.ProtoContent);
                    }
                }
            }
        }
        else if(m_SocketStatus == SocketStatus.ConnectExceptionOccurred)
        {
            m_SocketStatus = SocketStatus.Disconnected;
            ConnectExceptionOccured?.Invoke();
        }
        else if(m_SocketStatus == SocketStatus.ConnectSucceed)
        {
            m_SocketStatus = SocketStatus.Connected;
            ConnectSucceed?.Invoke();
        }
        else if(m_SocketStatus == SocketStatus.ConnectFailed)
        {
            m_SocketStatus = SocketStatus.Disconnected;
            ConnectFailed?.Invoke();
        }
    }
    #endregion

    #region 异步发送数据
    //封装数据包
    private byte[] MakeMsg(byte[] data) 
    {
        //消息格式：数据包长度(ushort)|压缩标志(bool)|crc16(ushort)|先压缩后异或的数据包
        MMO_MemoryStream ms = new MMO_MemoryStream();
        if (data.Length > m_compressLength)
        {
            //压缩
            data = ZlibHelper.CompressBytes(data);
            //数据包长度
            ms.WriteUShort((ushort)(data.Length + 3));
            //压缩标志
            ms.WriteBool(true);
        }
        else
        {
            //不压缩
            //数据包长度
            ms.WriteUShort((ushort)(data.Length + 3));
            //压缩标志
            ms.WriteBool(false);
        }
        //异或
        SecurityUtil.XOR(data);
        //crc16
        ms.WriteUShort(Crc16.CalculateCrc16(data));
        //数据包
        ms.Write(data, 0, data.Length);
        return ms.ToArray();
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="data">二进制数据</param>
    public void SendMsg(byte[] data)
    {
        byte[] msg = MakeMsg(data);
        lock (m_ToBeSentMsgQueue)
        {
            m_ToBeSentMsgQueue.Enqueue(msg);
            if (!m_IsSending)
            {
                SendNextMsg();
                m_IsSending = true;
            }
        }
    }

    //发送下一条消息
    private void SendNextMsg()
    {
        byte[] nextMsg = m_ToBeSentMsgQueue.Peek();
        try
        {
            m_Socket.BeginSend(nextMsg, 0, nextMsg.Length, SocketFlags.None, SendCallback, null);
        }
        catch(SocketException ex)
        {
            m_SocketStatus = SocketStatus.ConnectExceptionOccurred;
            DebugLogger.Log($"与{ m_Socket.RemoteEndPoint }的连接异常，exception：{ ex.Message }");
        }
        catch(ObjectDisposedException)
        {
            //忽略
        }
    }

    //异步发送的回调
    private void SendCallback(IAsyncResult asyncResult)
    {
        try
        {
            int count = m_Socket.EndSend(asyncResult);
            if (count == (int)SocketError.SocketError)
            {

            }
            else
            {
                lock (m_ToBeSentMsgQueue)
                {
                    m_ToBeSentMsgQueue.Dequeue();
                    if(m_ToBeSentMsgQueue.Count > 0)
                    {
                        SendNextMsg();
                    }
                    else
                    {
                        m_IsSending = false;
                    }
                }
            }
        }
        catch(Exception ex)
        {
            m_SocketStatus = SocketStatus.ConnectExceptionOccurred;
            DebugLogger.Log($"与{ m_Socket.RemoteEndPoint }的连接异常，exception：{ ex.Message }");
        }
        
    }
    #endregion

    #region 关闭连接
    /// <summary>
    /// 关闭Socket连接
    /// </summary>
    public void Close()
    {
        if(m_SocketStatus != SocketStatus.Connected)
        {
            throw new Exception("socket未连接");
        }
        m_Socket.Shutdown(SocketShutdown.Both);
        m_Socket.Close();
        m_SocketStatus = SocketStatus.Disconnected;
        m_Socket = null;
    }
    #endregion
}