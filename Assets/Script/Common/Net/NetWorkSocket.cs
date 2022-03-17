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
        }
        catch(Exception ex)
        {
            Debug.Log(string.Format("连接{0}:{1}失败，error：{2}", ip, port, ex.Message));
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
    /// <param name="msg"></param>
    public void SendMsg(string msg)
    {
        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.Position = 2;
        ms.WriteUTF8String(msg);
        ms.Position = 0;
        ms.WriteUShort((ushort)(ms.Length - 2));
        byte[] buffer = ms.ToArray();
        m_Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, null);
    }

    /// <summary>
    /// 发送消息的回调
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallback(IAsyncResult ar)
    {
        m_Socket.EndSend(ar);
    }
}