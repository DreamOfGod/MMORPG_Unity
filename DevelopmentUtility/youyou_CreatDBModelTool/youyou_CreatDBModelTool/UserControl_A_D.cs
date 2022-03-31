using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Actions;

namespace youyou_CreatDBModelTool
{
    public partial class UserControl_A_D : UserControl
    {
        public UserControl_A_D()
        {
            InitializeComponent();

            textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            textEditorControl1.Encoding = System.Text.Encoding.Default;
        }

        private void DrawRec(Graphics g)
        {
            //填充最上方渐变
            LinearGradientBrush myBrushA = new LinearGradientBrush(new Point(0, 0), new Point(0, 35), ColorTranslator.FromHtml("#F5FAFF"), ColorTranslator.FromHtml("#E1F0FF"));
            g.FillRectangle(myBrushA, 0, 0, this.Width, 35);
            myBrushA.Dispose();
        }

        private void DrawLine(Graphics g)
        {
            //蓝线
            Pen myPenA = new Pen(ColorTranslator.FromHtml("#88A7D3"));
            g.DrawLine(myPenA, new Point(0, 35), new Point(this.Width, 35));
            myPenA.Dispose();
        }

        private void UserControl_A_A_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(ColorTranslator.FromHtml("#FFFFFF"));

            this.DrawRec(g);

            this.DrawLine(g);

            g.Dispose();
        }

        private void UserControl_A_A_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();

            this.textEditorControl1.Size = new Size(this.Width, this.Height - 36);
        }

        private void buttons_A1_ButtonClick(object sender, EventArgs e)
        {//SQLServerDAL
            if (Config.DefaultConn == null)
            {
                new MessageForm("请先连接服务器").ShowDialog();
                return;
            }
            if (Config.CurrentTableName == null)
            {
                new MessageForm("请先选择数据表").ShowDialog();
                return;
            }

            //生成数据层
            DataTable dt = Config.GetDataTableByName(Config.CurrentTableName);
            if (dt == null)
            {
                return;
            }
            this.textEditorControl1.Text = "";

            StringBuilder sbContent = new StringBuilder();
            sbContent.Append("/// <summary>\n");
            sbContent.AppendFormat("/// 类名 : {0}DBModel\n", Config.CurrentTableName);
            sbContent.Append("/// 作者 : \n");
            sbContent.Append("/// 说明 : \n");
            sbContent.Append("/// 创建日期 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
            sbContent.Append("/// </summary>\n");
            sbContent.Append("using System;\n");
            sbContent.Append("using System.Collections.Generic;\n");
            sbContent.Append("using System.Linq;\n");
            sbContent.Append("using System.Text;\n");
            sbContent.Append("using System.Data.SqlClient;\n");
            sbContent.Append("using System.Data;\n");
            sbContent.Append("\n");
            sbContent.Append("using Mmcoy.Framework.AbstractBase;\n");
            sbContent.Append("\n");
            sbContent.Append("/// <summary>\n");
            sbContent.Append("/// DBModel\n");
            sbContent.Append("/// </summary>\n");
            sbContent.AppendFormat("public partial class {0}DBModel : MFAbstractSQLDBModel<{0}Entity>\n", Config.CurrentTableName);
            sbContent.Append("{\n");
            sbContent.AppendFormat("    #region {0}DBModel 私有构造\n", Config.CurrentTableName);
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 私有构造\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.AppendFormat("    private {0}DBModel()\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region 单例\n");
            sbContent.Append("    private static object lock_object = new object();\n");
            sbContent.AppendFormat("    private static {0}DBModel instance = null;\n", Config.CurrentTableName);
            sbContent.AppendFormat("    public static {0}DBModel Instance\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        get\n");
            sbContent.Append("        {\n");
            sbContent.Append("            if (instance == null)\n");
            sbContent.Append("            {\n");
            sbContent.Append("                lock (lock_object)\n");
            sbContent.Append("                {\n");
            sbContent.Append("                    if (instance == null)\n");
            sbContent.Append("                    {\n");
            sbContent.AppendFormat("                        instance = new {0}DBModel();\n", Config.CurrentTableName);
            sbContent.Append("                    }\n");
            sbContent.Append("                }\n");
            sbContent.Append("            }\n");
            sbContent.Append("            return instance;\n");
            sbContent.Append("        }\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region 实现基类的属性和方法\n");
            sbContent.Append("\n");
            sbContent.Append("    #region ConnectionString 数据库连接字符串\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 数据库连接字符串\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    protected override string ConnectionString\n");
            sbContent.Append("    {\n");
            sbContent.AppendFormat("        get {{ return DBConn.{0}; }}\n", Config.CurrentDataBaseName);
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region TableName 表名\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 表名\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    protected override string TableName\n");
            sbContent.Append("    {\n");
            sbContent.AppendFormat("        get {{ return \"{0}\"; }}\n", Config.CurrentTableName);
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");


            string strColumns = "";
            foreach (DataRow drw in dt.Rows)
            {
                strColumns += string.Format("\"{0}\", ", drw[3].ToString());
            }

            sbContent.Append("    #region ColumnList 列名集合\n");
            sbContent.Append("    private IList<string> _ColumnList;\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 列名集合\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    protected override IList<string> ColumnList\n");
            sbContent.Append("    {\n");
            sbContent.Append("        get\n");
            sbContent.Append("        {\n");
            sbContent.Append("            if (_ColumnList == null)\n");
            sbContent.Append("            {\n");
            sbContent.AppendFormat("                _ColumnList = new List<string> {{ {0} }};\n", strColumns.Trim().TrimEnd(','));
            sbContent.Append("            }\n");
            sbContent.Append("            return _ColumnList;\n");
            sbContent.Append("        }\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region ValueParas 转换参数\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 转换参数\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"entity\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    protected override SqlParameter[] ValueParas({0}Entity entity)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        SqlParameter[] parameters = new SqlParameter[] {\n");
            foreach (DataRow drw in dt.Rows)
            {
                sbContent.AppendFormat("                new SqlParameter(\"@{0}\", entity.{0}) {{ DbType = DbType.{1} }},\n", drw[3].ToString(), Config.GetSqlDbType(drw[7].ToString()));
            }

            sbContent.Append("                new SqlParameter(\"@RetMsg\", SqlDbType.NVarChar, 255),\n");
            sbContent.Append("                new SqlParameter(\"@ReturnValue\", SqlDbType.Int)\n");
            sbContent.Append("            };\n");
            sbContent.Append("        return parameters;\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region GetEntitySelfProperty 封装对象\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 封装对象\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"reader\"></param>\n");
            sbContent.Append("    /// <param name=\"table\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    protected override {0}Entity GetEntitySelfProperty(IDataReader reader, DataTable table)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.AppendFormat("        {0}Entity entity = new {0}Entity();\n", Config.CurrentTableName);
            sbContent.Append("        foreach (DataRow row in table.Rows)\n");
            sbContent.Append("        {\n");
            sbContent.Append("            var colName = row.Field<string>(0);\n");
            sbContent.Append("            if (reader[colName] is DBNull)\n");
            sbContent.Append("                continue;\n");
            sbContent.Append("            switch (colName.ToLower())\n");
            sbContent.Append("            {\n");
            sbContent.Append("                case \"id\":\n");
            sbContent.Append("                    if (!(reader[\"Id\"] is DBNull))\n");
            sbContent.Append("                        entity.Id = Convert.ToInt32(reader[\"Id\"]);\n");
            sbContent.Append("                    break;\n");
            sbContent.Append("                case \"status\":\n");
            sbContent.Append("                    if (!(reader[\"Status\"] is DBNull))\n");
            sbContent.Append("                        entity.Status = (EnumEntityStatus)Convert.ToInt32(reader[\"Status\"]);\n");
            sbContent.Append("                    break;\n");
            foreach (DataRow drw in dt.Rows)
            {
                if (drw[3].ToString().Trim().Equals("Id") || drw[3].ToString().Trim().Equals("Status"))
                {
                    continue;
                }
                sbContent.AppendFormat("                case \"{0}\":\n", drw[3].ToString().ToLower());
                sbContent.AppendFormat("                    if (!(reader[\"{0}\"] is DBNull))\n", drw[3].ToString());
                sbContent.AppendFormat("                        entity.{0} = Convert.{1}(reader[\"{0}\"]);\n", drw[3].ToString(), Config.GetConvertType(drw[7].ToString()));
                sbContent.Append("                    break;\n");
            }
            sbContent.Append("            }\n");
            sbContent.Append("        }\n");
            sbContent.Append("        return entity;\n");
            sbContent.Append("   }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("}\n");

            this.textEditorControl1.Text = sbContent.ToString();
        }
    }
}
