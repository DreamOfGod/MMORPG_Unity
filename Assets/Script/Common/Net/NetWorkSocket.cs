//===============================================
//作    者：
//创建时间：2022-03-17 13:44:24
//备    注：
//===============================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

/// <summary>
/// Socket通信管理
/// </summary>
public class NetWorkSocket
{
    #region 单例
    private static NetWorkSocket instance;
    public static NetWorkSocket Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new NetWorkSocket();
            }
            return instance;
        }
    }
    #endregion

    private NetWorkSocket() { }

    private Socket m_Socket;

    /// <summary>
    /// 接收数据包的字节数组缓冲区
    /// </summary>
    private byte[] m_ReceiveBuffer = new byte[10240];

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

            //字节流长度达到2个字节，至少一个消息的头部接收完成，因为客户端消息的头部是ushort类型
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
                        byte[] content = new byte[contentCount];
                        m_ReceiveMS.Read(content, 0, contentCount);

                        MMO_MemoryStream ms = new MMO_MemoryStream(content);
                        string contentStr = ms.ReadUTF8String();
                        Debug.Log(contentStr);

                        long leftCount = m_ReceiveMS.Length - m_ReceiveMS.Position;
                        if (leftCount == 0)
                        {
                            //没有剩余字节，指针和长度都置为0
                            m_ReceiveMS.Position = 0;
                            m_ReceiveMS.SetLength(0);
                            break;
                        }
                        else if (leftCount == 1)
                        {
                            //剩余1个字节，移到字节流开头，指针和长度都置为1
                            byte[] buffer = m_ReceiveMS.GetBuffer();
                            buffer[0] = buffer[m_ReceiveMS.Position];
                            m_ReceiveMS.Position = 1;
                            m_ReceiveMS.SetLength(1);
                            break;
                        }
                    }
                    else
                    {
                        //消息内容接收不完整，将剩余字节移到字节流开头，等内容接收完整再解析
                        byte[] buffer = m_ReceiveMS.GetBuffer();
                        long i = 0, j = m_ReceiveMS.Position;
                        while (i < m_ReceiveMS.Length)
                        {
                            buffer[i] = buffer[j];
                            ++i;
                            ++j;
                        }
                        m_ReceiveMS.SetLength(j);
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
    /// 发送消息
    /// </summary>
    /// <param name="data"></param>
    public void SendMsg(byte[] data)
    {
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.WriteUShort((ushort)data.Length);
        ms.Write(data, 0, data.Length);
        byte[] msg = ms.ToArray();
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