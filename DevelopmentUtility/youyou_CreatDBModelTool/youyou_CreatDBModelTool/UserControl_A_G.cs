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
    public partial class UserControl_A_G : UserControl
    {
        public UserControl_A_G()
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
        {//BLL
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

            //生成逻辑层
            DataTable dt = Config.GetDataTableByName(Config.CurrentTableName);
            if (dt == null)
            {
                return;
            }
            this.textEditorControl1.Text = "";

            StringBuilder sbContent = new StringBuilder();
            sbContent.Append("/// <summary>\n");
            sbContent.AppendFormat("/// 类名 : {0}CacheModel\n", Config.CurrentTableName);
            sbContent.Append("/// 作者 : 北京-边涯\n");
            sbContent.Append("/// 说明 : \n");
            sbContent.Append("/// 创建日期 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
            sbContent.Append("/// </summary>\n");
            sbContent.Append("using System;\n");
            sbContent.Append("using System.Collections.Generic;\n");
            sbContent.Append("using System.Linq;\n");
            sbContent.Append("using System.Text;\n");
            sbContent.Append("\n");
            sbContent.Append("using Mmcoy.Framework;\n");
            sbContent.Append("using Mmcoy.Framework.AbstractBase;\n");
            sbContent.Append("\n");
            sbContent.Append("/// <summary>\n");
            sbContent.Append("/// CacheModel\n");
            sbContent.Append("/// </summary>\n");
            sbContent.AppendFormat("public partial class {0}CacheModel : MFAbstractCacheModel\n", Config.CurrentTableName);
            sbContent.Append("{\n");
            sbContent.AppendFormat("    #region {0}CacheModel 私有构造\n", Config.CurrentTableName);
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 私有构造\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.AppendFormat("    private {0}CacheModel()\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region 单例\n");
            sbContent.Append("    private static object lock_object = new object();\n");
            sbContent.AppendFormat("    private static {0}CacheModel instance = null;\n", Config.CurrentTableName);
            sbContent.AppendFormat("    public static {0}CacheModel Instance\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        get\n");
            sbContent.Append("        {\n");
            sbContent.Append("            if (instance == null)\n");
            sbContent.Append("            {\n");
            sbContent.Append("                lock (lock_object)\n");
            sbContent.Append("                {\n");
            sbContent.Append("                    if (instance == null)\n");
            sbContent.Append("                    {\n");
            sbContent.AppendFormat("                        instance = new {0}CacheModel();\n", Config.CurrentTableName);
            sbContent.Append("                    }\n");
            sbContent.Append("                }\n");
            sbContent.Append("            }\n");
            sbContent.Append("            return instance;\n");
            sbContent.Append("        }\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region DBModel 数据模型层单例\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 数据模型层单例\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.AppendFormat("    private {0}DBModel DBModel {{ get {{ return {0}DBModel.Instance; }} }}\n", Config.CurrentTableName);
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region Create 创建\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 创建\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"entity\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public MFReturnValue<object> Create({0}Entity entity)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Create(entity);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region Update 修改\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 修改\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"entity\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public MFReturnValue<object> Update({0}Entity entity)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Update(entity);\n");
            sbContent.Append("    }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 按条件修改指定字段\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"setStr\"></param>\n");
            sbContent.Append("    /// <param name=\"conditionStr\"></param>\n");
            sbContent.Append("    /// <param name=\"parameters\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.Append("    public MFReturnValue<object> Update(string setStr, string conditionStr, IDictionary<string, object> parameters)\n");
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Update(setStr, conditionStr, parameters);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region Delete 根据编号删除\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 根据编号删除\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"id\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.Append("    public MFReturnValue<object> Delete(int? id)\n");
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Delete(id);\n");
            sbContent.Append("    }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 根据多个编号删除\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"ids\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.Append("    public MFReturnValue<object> Delete(string ids)\n");
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Delete(ids);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region GetCount 根据条件查询数量\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 根据条件查询数量\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"condition\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.Append("    public int GetCount(string condition = \"\")\n");
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.GetCount(condition);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region GetEntity\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 根据编号查询实体\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"id\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public {0}Entity GetEntity(int? id)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.GetEntity(id);\n");
            sbContent.Append("    }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 根据条件查询实体\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"condition\"></param>\n");
            sbContent.Append("    /// <param name=\"isAutoStatus\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public {0}Entity GetEntity(string condition, bool isAutoStatus = true)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.GetEntity(condition, isAutoStatus);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region GetList 获取列表\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 获取列表\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"tableName\"></param>\n");
            sbContent.Append("    /// <param name=\"columns\"></param>\n");
            sbContent.Append("    /// <param name=\"condition\"></param>\n");
            sbContent.Append("    /// <param name=\"orderby\"></param>\n");
            sbContent.Append("    /// <param name=\"isDesc\"></param>\n");
            sbContent.Append("    /// <param name=\"isAutoStatus\"></param>\n");
            sbContent.Append("    /// <param name=\"isEfficient\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public List<{0}Entity> GetList(string tableName = \"\", string columns = \"*\", string condition = \"\", string orderby = \"Id\", bool isDesc = true, bool isAutoStatus = true, bool isEfficient = false)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.GetList(tableName, columns, condition, orderby, isDesc, isAutoStatus, isEfficient);\n");
            sbContent.Append("    }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// 获取分页列表\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"tableName\"></param>\n");
            sbContent.Append("    /// <param name=\"columns\"></param>\n");
            sbContent.Append("    /// <param name=\"condition\"></param>\n");
            sbContent.Append("    /// <param name=\"orderby\"></param>\n");
            sbContent.Append("    /// <param name=\"isDesc\"></param>\n");
            sbContent.Append("    /// <param name=\"pageSize\"></param>\n");
            sbContent.Append("    /// <param name=\"pageIndex\"></param>\n");
            sbContent.Append("    /// <param name=\"isAutoStatus\"></param>\n");
            sbContent.Append("    /// <param name=\"isEfficient\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public MFReturnValue<List<{0}Entity>> GetPageList(string tableName = \"\", string columns = \"*\", string condition = \"\", string orderby = \"Id\", bool isDesc = true, int? pageSize = 20, int? pageIndex = 1, bool isAutoStatus = true, bool isEfficient = false)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.GetPageList(tableName, columns, condition, orderby, isDesc, pageSize, pageIndex, isAutoStatus, isEfficient);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("}\n");

            this.textEditorControl1.Text = sbContent.ToString();
        }
    }
}
