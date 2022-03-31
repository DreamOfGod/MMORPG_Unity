using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

namespace youyou_CreatDBModelTool
{
    public class Config
    {
        public static readonly string strUrl = "http://www.u3dol.com";
        public static readonly string CurrentVersion = "2016.03";

        public static string strEdition = CurrentVersion.Equals(ServerVersion) ? "本系统适用于SqlServer    当前版本：Beta V" + CurrentVersion + "    开发：北京-边涯    QQ：2838771247" : "                                    您的软件不是最新版本　请下载最新　V" + ServerVersion + "　版";

        public static string DefaultIP = "127.0.0.1";
        public static string DefaultUID = "root";
        public static string DefaultPwd = "123456";

        private static string _ServerVersion;

        /// <summary>
        /// 服务器版本
        /// </summary>
        public static string ServerVersion
        {
            get
            {
                _ServerVersion = CurrentVersion;
                return _ServerVersion;
            }
        }

        private static string _DefaultConn;

        /// <summary>
        /// 登录服务器连接
        /// </summary>
        public static string DefaultConn
        {
            get { return _DefaultConn; }
            set { _DefaultConn = value; }
        }

        private static string _CurrentConn;

        /// <summary>
        /// 连接数据表连接
        /// </summary>
        public static string CurrentConn
        {
            get { return _CurrentConn; }
            set { _CurrentConn = value; }
        }

        private static DataTable _CurrentTable;

        /// <summary>
        /// 当前表
        /// </summary>
        public static DataTable CurrentTable
        {
            get { return _CurrentTable; }
            set { _CurrentTable = value; }
        }

        private static string _CurrentTableName;

        /// <summary>
        /// 当前表名
        /// </summary>
        public static string CurrentTableName
        {
            get { return _CurrentTableName; }
            set { _CurrentTableName = value; }
        }

        private static string _CurrentDataBaseName;

        /// <summary>
        /// 当前数据库名
        /// </summary>
        public static string CurrentDataBaseName
        {
            get { return _CurrentDataBaseName; }
            set { _CurrentDataBaseName = value; }
        }

        /// <summary>
        /// 获取数据库表信息
        /// </summary>
        /// <param name="strName">表名</param>
        /// <returns>数据库表信息</returns>
        public static DataTable GetDataTableByName(string strName)
        {
            StringBuilder sbTable = new StringBuilder();

            if (MySqlHelper.ExecuteScalar(DefaultConn, "SELECT COUNT(1) FROM sysobjects WHERE [NAME] ='sysproperties'").ToString().Trim().Equals("0"))
            {//2005


                sbTable.Append("SELECT");

                sbTable.Append(" TableName=CASE WHEN C.column_id=1 THEN O.name ELSE N'' END,");

                sbTable.Append(" TableDesc=ISNULL(CASE WHEN C.column_id=1 THEN PTB.[value] END,N''),");

                sbTable.Append(" Column_id=C.column_id,");

                sbTable.Append(" ColumnName=C.name,");

                sbTable.Append(" PrimaryKey=ISNULL(IDX.PrimaryKey,N''),");

                sbTable.Append(" [IDENTITY]=CASE WHEN C.is_identity=1 THEN N'√'ELSE N'' END,");

                sbTable.Append(" Computed=CASE WHEN C.is_computed=1 THEN N'√'ELSE N'' END,");

                sbTable.Append(" Type=T.name,");

                sbTable.Append(" Length=C.max_length,");

                sbTable.Append(" [Precision]=C.[precision],");

                sbTable.Append(" Scale=C.scale,");

                sbTable.Append(" NullAble=CASE WHEN C.is_nullable=1 THEN N'√'ELSE N'' END,");

                sbTable.Append(" [Default]=ISNULL(D.definition,N''),");

                sbTable.Append(" ColumnDesc=ISNULL(PFD.[value],N''),");

                sbTable.Append(" IndexName=ISNULL(IDX.IndexName,N''),");

                sbTable.Append(" IndexSort=ISNULL(IDX.Sort,N''),");

                sbTable.Append(" Create_Date=O.Create_Date,");

                sbTable.Append(" Modify_Date=O.Modify_date");

                sbTable.Append(" FROM sys.columns C");

                sbTable.Append(" INNER JOIN sys.objects O");

                sbTable.Append(" ON C.[object_id]=O.[object_id]");

                sbTable.Append(" AND O.type='U'");

                sbTable.Append(" AND O.is_ms_shipped=0");

                sbTable.Append(" INNER JOIN sys.types T");

                sbTable.Append(" ON C.user_type_id=T.user_type_id");

                sbTable.Append(" LEFT JOIN sys.default_constraints D");

                sbTable.Append(" ON C.[object_id]=D.parent_object_id");

                sbTable.Append(" AND C.column_id=D.parent_column_id");

                sbTable.Append(" AND C.default_object_id=D.[object_id]");

                sbTable.Append(" LEFT JOIN sys.extended_properties PFD");

                sbTable.Append(" ON PFD.class=1");

                sbTable.Append(" AND C.[object_id]=PFD.major_id");

                sbTable.Append(" AND C.column_id=PFD.minor_id");

                sbTable.Append(" LEFT JOIN sys.extended_properties PTB");

                sbTable.Append(" ON PTB.class=1");

                sbTable.Append(" AND PTB.minor_id=0");

                sbTable.Append(" AND C.[object_id]=PTB.major_id");

                sbTable.Append(" LEFT JOIN");

                sbTable.Append(" (");

                sbTable.Append(" SELECT");

                sbTable.Append(" IDXC.[object_id],");

                sbTable.Append(" IDXC.column_id,");

                sbTable.Append(" Sort=CASE INDEXKEY_PROPERTY(IDXC.[object_id],IDXC.index_id,IDXC.index_column_id,'IsDescending')");

                sbTable.Append(" WHEN 1 THEN 'DESC' WHEN 0 THEN 'ASC' ELSE '' END,");

                sbTable.Append(" PrimaryKey=CASE WHEN IDX.is_primary_key=1 THEN N'√'ELSE N'' END,");

                sbTable.Append(" IndexName=IDX.Name");

                sbTable.Append(" FROM sys.indexes IDX");

                sbTable.Append(" INNER JOIN sys.index_columns IDXC");

                sbTable.Append(" ON IDX.[object_id]=IDXC.[object_id]");

                sbTable.Append(" AND IDX.index_id=IDXC.index_id");

                sbTable.Append(" LEFT JOIN sys.key_constraints KC");

                sbTable.Append(" ON IDX.[object_id]=KC.[parent_object_id]");

                sbTable.Append(" AND IDX.index_id=KC.unique_index_id");

                sbTable.Append(" INNER JOIN");

                sbTable.Append(" (");

                sbTable.Append(" SELECT [object_id], Column_id, index_id=MIN(index_id)");

                sbTable.Append(" FROM sys.index_columns");

                sbTable.Append(" GROUP BY [object_id], Column_id");

                sbTable.Append(" ) IDXCUQ");

                sbTable.Append(" ON IDXC.[object_id]=IDXCUQ.[object_id]");

                sbTable.Append(" AND IDXC.Column_id=IDXCUQ.Column_id");

                sbTable.Append(" AND IDXC.index_id=IDXCUQ.index_id");

                sbTable.Append(" ) IDX");

                sbTable.Append(" ON C.[object_id]=IDX.[object_id]");

                sbTable.Append(" AND C.column_id=IDX.column_id");

                sbTable.Append(" WHERE O.name='" + strName.Trim() + "'");

                sbTable.Append(" ORDER BY O.name,C.column_id");
            }
            else
            {
                sbTable.Append("Select");
                sbTable.Append(" TableName  = case when a.colorder=1 then d.name else '' end,");
                sbTable.Append(" TableDesc  = '',");
                sbTable.Append(" Column_id  = a.colid,");
                sbTable.Append(" ColumnName = a.name,");
                sbTable.Append(" PrimaryKey = case when exists(Select 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (");
                sbTable.Append(" Select name FROM sysindexes Where indid in(");
                sbTable.Append(" Select indid FROM sysindexkeys Where id = a.id AND colid=a.colid))) then '√' else '' end,");
                sbTable.Append(" [IDENTITY] = case when a.colstat='1' then '√' else '' end,");
                sbTable.Append(" Computed   = '',");
                sbTable.Append(" Type       = b.name,");
                sbTable.Append(" Length = a.length,");
                sbTable.Append(" [Precision]   = case when a.collationid is null then '1' else '0' end,");
                sbTable.Append(" Scale      = '',");
                sbTable.Append(" NullAble   = '',");
                sbTable.Append(" [Default]  = isnull(e.text,''),");
                sbTable.Append(" ColumnDesc = isnull(g.[value],'')");
                sbTable.Append(" FROM");
                sbTable.Append(" syscolumns a");
                sbTable.Append(" left join");
                sbTable.Append(" systypes b");
                sbTable.Append(" on");
                sbTable.Append(" a.xusertype=b.xusertype");
                sbTable.Append(" inner join");
                sbTable.Append(" sysobjects d");
                sbTable.Append(" on");
                sbTable.Append(" a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'");
                sbTable.Append(" left join");
                sbTable.Append(" syscomments e");
                sbTable.Append(" on");
                sbTable.Append(" a.cdefault=e.id");
                sbTable.Append(" left join");
                sbTable.Append(" sysproperties g");
                sbTable.Append(" on");
                sbTable.Append(" a.id=g.id and a.colid=g.smallid");
                sbTable.Append(" left join");
                sbTable.Append(" sysproperties f");
                sbTable.Append(" on");
                sbTable.Append(" d.id=f.id and f.smallid=0");
                sbTable.Append(" where");
                sbTable.Append(" d.name='" + strName.Trim() + "'");
            }
            return SqlHelper.ExecuteDataTable(CurrentConn, CommandType.Text, sbTable.ToString());
        }

        /// <summary>
        /// 获取c#SqlDbType类型
        /// </summary>
        /// <param name="strSqlDbType">原始类型</param>
        /// <returns>新类型</returns>
        public static string GetSqlDbType(string strSqlDbType)
        {
            string strNewSqlDbType = "";
            switch (strSqlDbType.Trim())
            {
                case "tinyint":
                    strNewSqlDbType = "Byte";
                    break;
                case "bigint":
                    strNewSqlDbType = "Int64";
                    break;
                case "int":
                    strNewSqlDbType = "Int32";
                    break;
                case "smallint":
                    strNewSqlDbType = "Int16";
                    break;
                case "nvarchar":
                    strNewSqlDbType = "String";
                    break;
                case "text":
                    strNewSqlDbType = "String";
                    break;
                case "datetime":
                    strNewSqlDbType = "DateTime";
                    break;
                case "char":
                    strNewSqlDbType = "String";
                    break;
                case "varchar":
                    strNewSqlDbType = "String";
                    break;
                case "bit":
                    strNewSqlDbType = "Boolean";
                    break;
                default:
                    strNewSqlDbType = "String";
                    break;
            }
            return strNewSqlDbType;
        }

        public static string GetConvertType(string strSqlDbType)
        {
            string strNewSqlDbType = "";
            switch (strSqlDbType.Trim())
            {
                case "tinyint":
                    strNewSqlDbType = "ToByte";
                    break;
                case "bigint":
                    strNewSqlDbType = "ToInt64";
                    break;
                case "int":
                    strNewSqlDbType = "ToInt32";
                    break;
                case "smallint":
                    strNewSqlDbType = "ToInt16";
                    break;
                case "nvarchar":
                    strNewSqlDbType = "ToString";
                    break;
                case "text":
                    strNewSqlDbType = "ToString";
                    break;
                case "datetime":
                    strNewSqlDbType = "ToDateTime";
                    break;
                case "char":
                    strNewSqlDbType = "ToString";
                    break;
                case "varchar":
                    strNewSqlDbType = "ToString";
                    break;
                case "bit":
                    strNewSqlDbType = "ToBoolean";
                    break;
                default:
                    strNewSqlDbType = "ToString";
                    break;
            }
            return strNewSqlDbType;
        }

        public static string GetCSharpType(string strType)
        {
            string strNewType = "";
            switch (strType.Trim())
            {
                case "tinyint":
                    strNewType = "byte";
                    break;
                case "bigint":
                    strNewType = "long";
                    break;
                case "int":
                    strNewType = "int";
                    break;
                case "smallint":
                    strNewType = "short";
                    break;
                case "nvarchar":
                    strNewType = "string";
                    break;
                case "text":
                    strNewType = "string";
                    break;
                case "datetime":
                    strNewType = "DateTime";
                    break;
                case "char":
                    strNewType = "string";
                    break;
                case "varchar":
                    strNewType = "string";
                    break;
                case "bit":
                    strNewType = "bool";
                    break;
                default:
                    strNewType = "string";
                    break;
            }
            return strNewType;
        }

        //
        public static string GetIP()
        {
            Uri uri = new Uri("http://www.ikaka.com/ip/index.asp");
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            req.CookieContainer = new System.Net.CookieContainer();
            req.GetRequestStream().Write(new byte[0], 0, 0);
            System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)(req.GetResponse());
            StreamReader rs = new StreamReader(res.GetResponseStream(), System.Text.Encoding.GetEncoding("GB18030"));
            string s = rs.ReadToEnd();
            rs.Close();
            req.Abort();
            res.Close();
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(s, @"IP：\[(?<IP>[0-9\.]*)\]");
            if (m.Success) return m.Groups["IP"].Value;
            return string.Empty;
        }

        public static Hashtable GetMyInfo()
        {
            Hashtable ht = new Hashtable();
            ht.Add("IP", GetIP() + "/new");
            ht.Add("Name", Dns.GetHostName());
            ht.Add("Screen", Screen.PrimaryScreen.Bounds.Width.ToString() + "*" + Screen.PrimaryScreen.Bounds.Height.ToString());
            return ht;
        }
    }
}
