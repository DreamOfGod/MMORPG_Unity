//===============================================
//作    者：
//创建时间：2022-03-14 16:38:29
//备    注：
//===============================================
using System;
using System.IO;
using System.Text;

/// <summary>
/// 数据转换（byte、bool、short、int、long、float、double、string）
/// </summary>
public class MMO_MemoryStream : MemoryStream
{
    #region 读写short
    /// <summary>
    /// 读取short
    /// </summary>
    /// <returns></returns>
    public short ReadShort()
    {
        byte[] arr = new byte[2];
        Read(arr, 0, 2);
        return BitConverter.ToInt16(arr, 0);
    }

    /// <summary>
    /// 写入short
    /// </summary>
    /// <param name="val"></param>
    public void WriteShort(short val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写ushort
    /// <summary>
    /// 读取ushort
    /// </summary>
    /// <returns></returns>
    public ushort ReadUShort()
    {
        byte[] arr = new byte[2];
        Read(arr, 0, 2);
        return BitConverter.ToUInt16(arr, 0);
    }

    /// <summary>
    /// 写入ushort
    /// </summary>
    /// <param name="val"></param>
    public void WriteUShort(ushort val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写 bool
    /// <summary>
    /// 读取bool
    /// </summary>
    /// <returns></returns>
    public bool ReadBool()
    {
        return ReadByte() == 1;
    }

    /// <summary>
    /// 写入bool
    /// </summary>
    /// <param name="val"></param>
    public void WriteBool(bool val)
    {
        WriteByte((byte)(val? 1: 0));
    }
    #endregion

    #region 读写int
    /// <summary>
    /// 读取int
    /// </summary>
    /// <returns></returns>
    public int ReadInt()
    {
        byte[] arr = new byte[4];
        Read(arr, 0, 4);
        return BitConverter.ToInt32(arr, 0);
    }

    /// <summary>
    /// 写入int
    /// </summary>
    /// <param name="val"></param>
    public void WriteInt(int val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写uint
    /// <summary>
    /// 读取uint
    /// </summary>
    /// <returns></returns>
    public uint ReadUInt()
    {
        byte[] arr = new byte[4];
        Read(arr, 0, 4);
        return BitConverter.ToUInt32(arr, 0);
    }

    /// <summary>
    /// 写入uint
    /// </summary>
    /// <param name="val"></param>
    public void WriteUInt(uint val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写long
    /// <summary>
    /// 读取long
    /// </summary>
    /// <returns></returns>
    public long ReadLong()
    {
        byte[] arr = new byte[8];
        Read(arr, 0, 8);
        return BitConverter.ToInt64(arr, 0);
    }

    /// <summary>
    /// 写入long
    /// </summary>
    /// <param name="val"></param>
    public void WriteLong(long val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写ulong
    /// <summary>
    /// 读取long
    /// </summary>
    /// <returns></returns>
    public ulong ReadULong()
    {
        byte[] arr = new byte[8];
        Read(arr, 0, 8);
        return BitConverter.ToUInt64(arr, 0);
    }

    /// <summary>
    /// 写入ulong
    /// </summary>
    /// <param name="val"></param>
    public void WriteULong(ulong val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写float
    /// <summary>
    /// 读取float
    /// </summary>
    /// <returns></returns>
    public float ReadFloat()
    {
        byte[] arr = new byte[4];
        Read(arr, 0, 4);
        return BitConverter.ToSingle(arr, 0);
    }

    /// <summary>
    /// 写入float
    /// </summary>
    /// <param name="val"></param>
    public void WriteFloat(float val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写double
    /// <summary>
    /// 读取double
    /// </summary>
    /// <returns></returns>
    public double ReadDouble()
    {
        byte[] arr = new byte[8];
        Read(arr, 0, 8);
        return BitConverter.ToDouble(arr, 0);
    }

    /// <summary>
    /// 写入double
    /// </summary>
    /// <param name="val"></param>
    public void WriteDouble(double val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        Write(arr, 0, arr.Length);
    }
    #endregion

    #region 读写utf8 string
    /// <summary>
    /// 读取utf8 string
    /// </summary>
    /// <returns></returns>
    public string ReadUTF8String()
    {
        int len = ReadInt();//读取字符串的字节数量
        byte[] arr = new byte[len];
        Read(arr, 0, len);//读取字符串字节
        return Encoding.UTF8.GetString(arr);
    }

    /// <summary>
    /// 写入utf8 string
    /// </summary>
    /// <param name="val"></param>
    public void WriteUTF8String(string val)
    {
        byte[] arr = Encoding.UTF8.GetBytes(val);
        WriteInt(arr.Length);//写入字节数量
        Write(arr, 0, arr.Length);//写入字符串字节数组
    }
    #endregion
}
