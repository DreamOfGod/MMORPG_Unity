//===============================================
//作    者：
//创建时间：2022-03-17 13:44:24
//备    注：
//===============================================
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// Socket通信管理
/// </summary>
public class NetWorkSocket: Singleton<NetWorkSocket>
{
    private Socket m_Socket;

    /// <summary>
    /// 进行压缩的长度下限
    /// </summary>
    private const int COMPRESS_LENGTH = 200;

    /// <summary>
    /// 接收数据包的字节数组缓冲区
    /// </summary>
    private byte[] m_ReceiveBuffer = new byte[2048];

    /// <summary>
    /// 接收数据包的缓冲数据流
    /// </summary>
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

    /// <summary>
    /// 连接到Socket服务器
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">端口号</param>
    public void Connect(string ip, int port)
    {
        if (m_Socket != null && m_Socket.Connected) return;

        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            m_Socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            Debug.Log(string.Format("连接{0}:{1}成功", ip, port));

            //异步接收数据
            m_Socket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }
        catch(Exception ex)
        {
            Debug.Log(string.Format("连接{0}:{1}失败，error：{2}", ip, port, ex.Message));
        }
    }

    /// <summary>
    /// 接收数据的回调
    /// </summary>
    /// <param name="asyncResult"></param>
    private void ReceiveCallback(IAsyncResult asyncResult)
    {
        int count;
        try
        {
            //异步方法Begin...必须调用相应的End...完成异步操作
            count = m_Socket.EndReceive(asyncResult);
        }
        catch (Exception ex)
        {
            //连接异常
            Console.WriteLine("与{0}的连接异常，error：{1}", m_Socket.RemoteEndPoint.ToString(), ex.Message);
            return;
        }

        if (count > 0)
        {
            //已经接收到数据

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
                        //派发协议消息
                        EventDispatcher.Instance.Dispatch(protoCode, protoContent);

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

            //继续异步接收数据
            m_Socket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }
        else
        {
            //客户端断开连接
            Debug.Log("服务器{0}断开连接" + m_Socket.RemoteEndPoint.ToString());
        }
    }

    /// <summary>
    /// 关闭Socket连接
    /// </summary>
    public void Close()
    {
        if(m_Socket != null && m_Socket.Connected)
        {
            m_Socket.Shutdown(SocketShutdown.Both);
            m_Socket.Close();
        }
    }

    /// <summary>
    /// 封装数据包
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] MakeMsg(byte[] data) 
    {
        //消息格式：数据包长度(ushort)|压缩标志(bool)|crc16(ushort)|先压缩后异或的数据包
        MMO_MemoryStream ms = new MMO_MemoryStream();
        if (data.Length > COMPRESS_LENGTH)
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
    /// <param name="data"></param>
    public void SendMsg(byte[] data)
    {
        byte[] msg = MakeMsg(data);
        m_Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, SendCallback, null);
    }

    /// <summary>
    /// 发送消息的回调
    /// </summary>
    /// <param name="asyncResult"></param>
    private void SendCallback(IAsyncResult asyncResult)
    {
        m_Socket.EndSend(asyncResult);
    }
}