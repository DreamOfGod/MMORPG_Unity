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
            //������Ϸ�����
            LinearGradientBrush myBrushA = new LinearGradientBrush(new Point(0, 0), new Point(0, 35), ColorTranslator.FromHtml("#F5FAFF"), ColorTranslator.FromHtml("#E1F0FF"));
            g.FillRectangle(myBrushA, 0, 0, this.Width, 35);
            myBrushA.Dispose();
        }

        private void DrawLine(Graphics g)
        {
            //����
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
                new MessageForm("�������ӷ�����").ShowDialog();
                return;
            }
            if (Config.CurrentTableName == null)
            {
                new MessageForm("����ѡ�����ݱ�").ShowDialog();
                return;
            }

            //�����߼���
            DataTable dt = Config.GetDataTableByName(Config.CurrentTableName);
            if (dt == null)
            {
                return;
            }
            this.textEditorControl1.Text = "";

            StringBuilder sbContent = new StringBuilder();
            sbContent.Append("/// <summary>\n");
            sbContent.AppendFormat("/// ���� : {0}CacheModel\n", Config.CurrentTableName);
            sbContent.Append("/// ���� : ����-����\n");
            sbContent.Append("/// ˵�� : \n");
            sbContent.Append("/// �������� : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
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
            sbContent.AppendFormat("    #region {0}CacheModel ˽�й���\n", Config.CurrentTableName);
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ˽�й���\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.AppendFormat("    private {0}CacheModel()\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region ����\n");
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
            sbContent.Append("    #region DBModel ����ģ�Ͳ㵥��\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ����ģ�Ͳ㵥��\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.AppendFormat("    private {0}DBModel DBModel {{ get {{ return {0}DBModel.Instance; }} }}\n", Config.CurrentTableName);
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region Create ����\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ����\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"entity\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public MFReturnValue<object> Create({0}Entity entity)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Create(entity);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region Update �޸�\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// �޸�\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"entity\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public MFReturnValue<object> Update({0}Entity entity)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Update(entity);\n");
            sbContent.Append("    }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// �������޸�ָ���ֶ�\n");
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
            sbContent.Append("    #region Delete ���ݱ��ɾ��\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ���ݱ��ɾ��\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"id\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.Append("    public MFReturnValue<object> Delete(int? id)\n");
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Delete(id);\n");
            sbContent.Append("    }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ���ݶ�����ɾ��\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"ids\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.Append("    public MFReturnValue<object> Delete(string ids)\n");
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.Delete(ids);\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region GetCount ����������ѯ����\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ����������ѯ����\n");
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
            sbContent.Append("    /// ���ݱ�Ų�ѯʵ��\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    /// <param name=\"id\"></param>\n");
            sbContent.Append("    /// <returns></returns>\n");
            sbContent.AppendFormat("    public {0}Entity GetEntity(int? id)\n", Config.CurrentTableName);
            sbContent.Append("    {\n");
            sbContent.Append("        return this.DBModel.GetEntity(id);\n");
            sbContent.Append("    }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ����������ѯʵ��\n");
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
            sbContent.Append("    #region GetList ��ȡ�б�\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ��ȡ�б�\n");
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
            sbContent.Append("    /// ��ȡ��ҳ�б�\n");
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
