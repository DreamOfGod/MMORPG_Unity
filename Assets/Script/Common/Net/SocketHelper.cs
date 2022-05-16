//===============================================
//作    者：
//创建时间：2022-03-17 13:44:24
//备    注：
//===============================================
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security;
using UnityEngine;

/// <summary>
/// Socket帮助类
/// </summary>
public class SocketHelper: MonoBehaviour
{
    #region 单例
    private SocketHelper() { }
    public static readonly SocketHelper Instance;
    static SocketHelper()
    {
        GameObject obj = new GameObject(nameof(SocketHelper));
        DontDestroyOnLoad(obj);
        Instance = obj.AddComponent<SocketHelper>();
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
        HashSet<Action<byte[]>> handlerSet;
        if(!m_SocketMsgHandlerDic.TryGetValue(protoCode, out handlerSet))
        {
            handlerSet = new HashSet<Action<byte[]>>();
            m_SocketMsgHandlerDic.Add(protoCode, handlerSet);
        }
        handlerSet.Add(handler);
    }

    /// <summary>
    /// 移除Socket消息监听器
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void RemoveListener(ushort protoCode, Action<byte[]> handler)
    {
        m_SocketMsgHandlerDic[protoCode].Remove(handler);
    }

    //派发消息
    private void Dispatch(ushort protoCode, byte[] buffer)
    {
        HashSet<Action<byte[]>> handlerSet;
        if(m_SocketMsgHandlerDic.TryGetValue(protoCode, out handlerSet))
        {
            if (handlerSet.Count > 0)
            {
                DebugLogger.Log($"派发Socket消息，协议ID：{ protoCode }");
                foreach (var handler in handlerSet)
                {
                    handler(buffer);
                }
                return;
            }
        }
        DebugLogger.Log($"消息没有处理器，协议ID：{ protoCode }");
    }
    #endregion

    #region socket对象
    //socket对象
    private Socket m_Socket;

    //锁对象
    private readonly object m_Lock = new object();

    //当前socket连接的ip
    private string m_Ip;

    //当前socket连接的端口号
    private int m_Port = -1;
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
    public event Action ConnectionExceptionOccured;
    #endregion

    #region 状态
    //状态枚举
    private enum SocketHelperStatus: byte
    {
        Disconnected,//未连接
        Connecting,//正在连接
        ConnectSucceed,//连接成功
        ConnectFailed,//连接失败
        Connected,//已连接
        ConnectionExceptionOccurred,//连接发生异常
    }

    //状态
    private SocketHelperStatus m_Status = SocketHelperStatus.Disconnected;
    //异常信息
    private string m_ExceptionInfo;
    #endregion

    #region 接收相关字段
    //接收数据包的字节数组缓冲区
    private byte[] m_ReceiveBuffer = new byte[2048];

    //接收数据包的缓冲数据流
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

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

    //等待派发的消息队列
    private Queue<ReceivedMsg> m_WaitToBeDispatchedMsgQueue = new Queue<ReceivedMsg>();
    #endregion

    #region 发送相关字段
    //待发送的消息队列
    private Queue<byte[]> m_ToBeSentMsgQueue = new Queue<byte[]>();

    //进行压缩的长度下限
    private const int m_CompressLength = 200;
    #endregion

    #region 异步连接：连接成功，派发ConnectSucceed事件；连接失败或出现异常，派发ConnectFailed事件
    /// <summary>
    /// 异步连接
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">端口号</param>
    public void BeginConnect(string ip, int port)
    {
        lock(m_Lock)
        {
            if (m_Status != SocketHelperStatus.Disconnected)
            {
                throw new InvalidOperationException($"{ nameof(SocketHelper) }处于{ SocketHelperStatus.Disconnected }状态才能调用{ nameof(BeginConnect) }。当前状态为{ m_Status }");
            }
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var iep = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                m_Socket.BeginConnect(iep, ConnectCallback, m_Socket);
                m_Status = SocketHelperStatus.Connecting;
                m_Ip = ip;
                m_Port = port;
            }
            catch (SocketException ex)
            {
                m_Status = SocketHelperStatus.ConnectFailed;
                m_Socket.Close();
                m_Socket = null;
                m_Ip = null;
                m_Port = -1;
                m_ExceptionInfo = $"尝试连接时发生异常，exception：\n{ ex }";
            }
            catch (SecurityException ex)
            {
                m_Status = SocketHelperStatus.ConnectFailed;
                m_Socket.Close();
                m_Socket = null;
                m_Ip = null;
                m_Port = -1;
                m_ExceptionInfo = $"连接操作没有权限，exception:\n{ ex }";
            }
        }
    }

    //异步连接的回调
    private void ConnectCallback(IAsyncResult ar)
    {
        var socket = (Socket)ar.AsyncState;
        lock (m_Lock)
        {
            if (m_Status != SocketHelperStatus.Connecting || socket != m_Socket)
            {
                return;
            }
            try
            {
                m_Socket.EndConnect(ar);
            }
            catch (SocketException ex)
            {
                m_Status = SocketHelperStatus.ConnectFailed;
                m_ExceptionInfo = $"尝试连接{ m_Ip }:{ m_Port }时发生异常，exception：\n{ ex }";
                m_Socket.Close();
                m_Socket = null;
                m_Ip = null;
                m_Port = -1;
                return;
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            if (m_Socket.Connected)
            {
                m_Status = SocketHelperStatus.ConnectSucceed;
                m_ExceptionInfo = $"尝试连接{ m_Ip }:{ m_Port }成功";
            }
            else
            {
                m_Status = SocketHelperStatus.ConnectFailed;
                m_ExceptionInfo = $"尝试连接{ m_Ip }:{ m_Port }失败";
                m_Socket.Close();
                m_Socket = null;
                m_Ip = null;
                m_Port = -1;
            }
        }
    }
    #endregion

    #region 主线程Update循环
    //主线程Update循环
    private void Update()
    {
        lock (m_Lock)
        {
            if (m_Status == SocketHelperStatus.Connected)
            {
                //将已接收到的消息放入等待派发的消息队列
                lock (m_ReceivedMsgQueue)
                {
                    while (m_ReceivedMsgQueue.Count > 0)
                    {
                        m_WaitToBeDispatchedMsgQueue.Enqueue(m_ReceivedMsgQueue.Dequeue());
                    }
                }
            }
            else if (m_Status == SocketHelperStatus.ConnectionExceptionOccurred)
            {
                m_Status = SocketHelperStatus.Disconnected;
                DebugLogger.LogError(m_ExceptionInfo);
                m_ExceptionInfo = null;
                ConnectionExceptionOccured?.Invoke();
            }
            else if (m_Status == SocketHelperStatus.ConnectFailed)
            {
                m_Status = SocketHelperStatus.Disconnected;
                DebugLogger.LogError(m_ExceptionInfo);
                m_ExceptionInfo = null;
                ConnectFailed?.Invoke();
            }
            else if (m_Status == SocketHelperStatus.ConnectSucceed)
            {
                m_Status = SocketHelperStatus.Connected;
                DebugLogger.Log(m_ExceptionInfo);
                m_ExceptionInfo = null;
                ConnectSucceed?.Invoke();
                BeginReceive();
            }
        }

        //派发消息
        ReceivedMsg msg;
        while (m_WaitToBeDispatchedMsgQueue.Count > 0)
        {
            msg = m_WaitToBeDispatchedMsgQueue.Dequeue();
            Dispatch(msg.ProtoCode, msg.ProtoContent);
        }
    }
    #endregion

    #region 异步接收：出现SocketException异常或服务器断开连接，派发ConnectExceptionOccurred事件。出现ObjectDisposedException异常，忽略
    //异步接收
    private void BeginReceive()
    {
        try
        {
            m_Socket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallback, m_Socket);
        }
        catch (SocketException ex)
        {
            m_Status = SocketHelperStatus.ConnectionExceptionOccurred;
            m_ExceptionInfo = $"与{ m_Ip }:{ m_Port }的连接发生异常，exception：{ ex }";
            try
            {
                m_Socket.Shutdown(SocketShutdown.Both);
            }
            finally
            {
                m_Socket.Close();
                m_Socket = null;
                m_Ip = null;
                m_Port = -1;
                m_ToBeSentMsgQueue.Clear();
            }
        }
        catch (ObjectDisposedException)
        {
            
        }
    }

    //接收数据的回调
    private void ReceiveCallback(IAsyncResult ar)
    {
        var socket = (Socket)ar.AsyncState;
        int count;
        lock (m_Lock)
        {
            if (m_Status != SocketHelperStatus.Connected || socket != m_Socket)
            {
                return;
            }
            try
            {
                count = m_Socket.EndReceive(ar);
            }
            catch (SocketException ex)
            {
                m_Status = SocketHelperStatus.ConnectionExceptionOccurred;
                m_ExceptionInfo = $"与{ m_Ip }:{ m_Port }的连接异常，exception：{ ex }";
                try
                {
                    m_Socket.Shutdown(SocketShutdown.Both);
                }
                finally
                {
                    m_Socket.Close();
                    m_Socket = null;
                    m_Ip = null;
                    m_Port = -1;
                    m_ToBeSentMsgQueue.Clear();
                }
                return;
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            if (count == 0)
            {
                m_Status = SocketHelperStatus.ConnectionExceptionOccurred;
                m_ExceptionInfo = $"服务器{ m_Ip }:{ m_Port }断开连接";
                try
                {
                    m_Socket.Shutdown(SocketShutdown.Both);
                }
                finally
                {
                    m_Socket.Close();
                    m_Socket = null;
                    m_Ip = null;
                    m_Port = -1;
                    m_ToBeSentMsgQueue.Clear();
                }
                return;
            }
        }

        lock (m_ReceiveMS)//防止多个ReceiveCallback回调并发修改
        {
            //把接收的字节写入字节流的尾部
            m_ReceiveMS.Write(m_ReceiveBuffer, 0, count);
            ParseMsg();
        }

        lock(m_Lock)
        {
            if(m_Status == SocketHelperStatus.Connected && socket == m_Socket)
            {
                BeginReceive();
            }
        }
    }

    //解析消息
    private void ParseMsg()
    {
        //字节流长度达到2个字节，至少消息的数据包长度接收完成
        if (m_ReceiveMS.Length >= 2)
        {
            m_ReceiveMS.Position = 0;
            while (true)
            {
                //读取内容的字节数
                var contentCount = m_ReceiveMS.ReadUShort();
                if (m_ReceiveMS.Length - m_ReceiveMS.Position >= contentCount)
                {
                    //消息的内容已经接收完成
                    //压缩标志
                    var compressed = m_ReceiveMS.ReadBool();
                    //crc16校验码
                    var crc16 = m_ReceiveMS.ReadUShort();
                    //数据包
                    var content = new byte[contentCount - 3];
                    m_ReceiveMS.Read(content, 0, content.Length);
                    //crc16校验：未通过校验就丢弃
                    if (Crc16.CalculateCrc16(content) == crc16)
                    {
                        //数据包异或
                        SecurityUtil.XOR(content);
                        //解压
                        if (compressed)
                        {
                            content = ZlibHelper.DeCompressBytes(content);
                        }

                        var ms = new MMO_MemoryStream(content);
                        //协议ID
                        var protoCode = ms.ReadUShort();
                        //协议内容
                        var protoContent = new byte[contentCount - 2];
                        ms.Read(protoContent, 0, protoContent.Length);
                        //消息入队，等主线程处理
                        lock (m_ReceivedMsgQueue)
                        {
                            m_ReceivedMsgQueue.Enqueue(new ReceivedMsg(protoCode, protoContent));
                        }
                    }
                    var leftCount = m_ReceiveMS.Length - m_ReceiveMS.Position;
                    if (leftCount < 2)
                    {
                        //剩余不到2个字节，将剩余字节移到字节流开头，等内容接收完整再解析
                        var buffer = m_ReceiveMS.GetBuffer();
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
                    var buffer = m_ReceiveMS.GetBuffer();
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
    }
    #endregion

    #region 异步发送数据
    //封装数据包
    private byte[] MakeMsg(byte[] data)
    {
        //消息格式：数据包长度(ushort)|压缩标志(bool)|crc16(ushort)|先压缩后异或的数据包
        var ms = new MMO_MemoryStream();
        if (data.Length > m_CompressLength)
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
    /// 异步发送
    /// </summary>
    /// <param name="data">二进制数据</param>
    public void BeginSend(byte[] data)
    {
        var msg = MakeMsg(data);
        lock (m_Lock)
        {
            if(m_Status != SocketHelperStatus.Connected)
            {
                return;
            }
            m_ToBeSentMsgQueue.Enqueue(msg);
            if (m_ToBeSentMsgQueue.Count == 1)
            {
                try
                {
                    m_Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, SendCallback, m_Socket);
                }
                catch (SocketException ex)
                {
                    m_Status = SocketHelperStatus.ConnectionExceptionOccurred;
                    var iep = (IPEndPoint)m_Socket.RemoteEndPoint;
                    m_ExceptionInfo = $"与{ iep.Address }:{ iep.Port }的连接异常，exception：{ ex }";
                    try
                    {
                        m_Socket.Shutdown(SocketShutdown.Both);
                    }
                    finally
                    {
                        m_Socket.Close();
                        m_Socket = null;
                        m_Ip = null;
                        m_Port = -1;
                        m_ToBeSentMsgQueue.Clear();
                    }
                }
                catch (ObjectDisposedException)
                {

                }
            }
        }
    }

    //异步发送的回调
    private void SendCallback(IAsyncResult asyncResult)
    {
        var socket = (Socket)asyncResult.AsyncState;
        lock (m_Lock)
        {
            if (m_Status != SocketHelperStatus.Connected || socket != m_Socket)
            {
                return;
            }
            try
            {
                var count = m_Socket.EndSend(asyncResult);
                if (count == m_ToBeSentMsgQueue.Peek().Length)
                {
                    //消息发送完整
                    m_ToBeSentMsgQueue.Dequeue();
                    if (m_ToBeSentMsgQueue.Count > 0)
                    {
                        var msg = m_ToBeSentMsgQueue.Peek();
                        m_Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, SendCallback, null);
                    }
                }
                else if (count >= 0)
                {
                    //消息未发送完整
                    var msg = m_ToBeSentMsgQueue.Dequeue();
                    var leftMsg = new byte[msg.Length - count];//消息的剩余未发送的字节
                    Array.Copy(msg, count, leftMsg, 0, leftMsg.Length);
                    m_ToBeSentMsgQueue.Enqueue(leftMsg);
                    m_Socket.BeginSend(leftMsg, 0, leftMsg.Length, SocketFlags.None, SendCallback, null);
                }
                else
                {
                    //发送失败，重新发送
                    var msg = m_ToBeSentMsgQueue.Peek();
                    m_Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, SendCallback, null);
                }
            }
            catch (SocketException ex)
            {
                m_Status = SocketHelperStatus.ConnectionExceptionOccurred;
                m_ExceptionInfo = $"与{ m_Ip }:{ m_Port }的连接异常，exception：{ ex }";
                try
                {
                    m_Socket.Shutdown(SocketShutdown.Both);
                }
                finally
                {
                    m_Socket.Close();
                    m_Socket = null;
                    m_Ip = null;
                    m_Port = -1;
                    m_ToBeSentMsgQueue.Clear();
                }
            }
            catch (ObjectDisposedException)
            {

            }
        }
    }
    #endregion

    #region 关闭连接
    /// <summary>
    /// 关闭连接
    /// </summary>
    public void Close()
    {
        lock(m_Lock)
        {
            if (m_Status == SocketHelperStatus.Disconnected)
            {
                throw new InvalidOperationException($"{ nameof(SocketHelper) }处于{ SocketHelperStatus.Disconnected }状态不能调用{ nameof(Close) }。先连接才能关闭");
            }
            m_Status = SocketHelperStatus.Disconnected;
            try
            {
                m_Socket.Shutdown(SocketShutdown.Both);
            }
            finally
            {
                DebugLogger.Log($"关闭与{ m_Ip }:{ m_Port }的连接");
                m_Socket.Close();
                m_Socket = null;
                m_Ip = null;
                m_Port = -1;
                m_ToBeSentMsgQueue.Clear();
            }
        }
    }
    #endregion
}