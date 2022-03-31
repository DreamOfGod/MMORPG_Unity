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
    public partial class UserControl_A_C : UserControl
    {
        public UserControl_A_C()
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
        {//ʵ���
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

            //����ʵ���
            DataTable dt = Config.GetDataTableByName(Config.CurrentTableName);
            if (dt == null)
            {
                return;
            }
            this.textEditorControl1.Text = "";

            StringBuilder sbContent = new StringBuilder();
            sbContent.Append("/// <summary>\n");
            sbContent.AppendFormat("/// ���� : {0}Entity\n", Config.CurrentTableName);
            sbContent.Append("/// ���� : ����-����\n");
            sbContent.Append("/// ˵�� : \n");
            sbContent.Append("/// �������� : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
            sbContent.Append("/// </summary>\n");
            sbContent.Append("using System;\n");
            sbContent.Append("using System.Collections.Generic;\n");
            sbContent.Append("using System.Linq;\n");
            sbContent.Append("using System.Text;\n");
            sbContent.Append("\n");
            sbContent.Append("using Mmcoy.Framework.AbstractBase;\n");
            sbContent.Append("\n");
            sbContent.Append("/// <summary>\n");
            sbContent.Append("/// \n");
            sbContent.Append("/// </summary>\n");
            sbContent.Append("[Serializable]\n");
            sbContent.AppendFormat("public partial class {0}Entity : MFAbstractEntity\n", Config.CurrentTableName);
            sbContent.Append("{\n");
            sbContent.Append("    #region ��д��������\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ����\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    public override int? PKValue\n");
            sbContent.Append("    {\n");
            sbContent.Append("        get\n");
            sbContent.Append("        {\n");
            sbContent.Append("            return this.Id;\n");
            sbContent.Append("        }\n");
            sbContent.Append("        set\n");
            sbContent.Append("        {\n");
            sbContent.Append("            this.Id = value;\n");
            sbContent.Append("        }\n");
            sbContent.Append("    }\n");
            sbContent.Append("    #endregion\n");
            sbContent.Append("\n");
            sbContent.Append("    #region ʵ������\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ���\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    public int? Id { get; set; }\n");
            sbContent.Append("\n");
            sbContent.Append("    /// <summary>\n");
            sbContent.Append("    /// ״̬\n");
            sbContent.Append("    /// </summary>\n");
            sbContent.Append("    public EnumEntityStatus Status { get; set; }\n");
            sbContent.Append("\n");
            foreach (DataRow drw in dt.Rows)
            {
                if (drw[3].ToString().Trim().Equals("Id") || drw[3].ToString().Trim().Equals("Status"))
                {
                    continue;
                }
                sbContent.Append("    /// <summary>\n");
                sbContent.AppendFormat("    ///{0} \n", drw[13].ToString());
                sbContent.Append("    /// </summary>\n");
                sbContent.AppendFormat("    public {0} {1} {{ get; set; }}\n", Config.GetCSharpType(drw[7].ToString().Trim()), drw[3].ToString().Trim());
                sbContent.Append("\n");
            }
            sbContent.Append("    #endregion\n");
            sbContent.Append("}\n");

            //sbContent.Append("using System;\n");
            //sbContent.Append("using System.Collections;\n");
            //sbContent.Append("using System.Collections.Generic;\n");
            //sbContent.Append("using System.Text;\n");

            //sbContent.Append("namespace " + Config.CurrentDataBaseName + ".Model\n");
            //sbContent.Append("{\n");
            //sbContent.Append("	/// <summary>\n");
            //sbContent.Append("	/// " + Config.CurrentTableName + "����\n");
            //sbContent.Append("	/// </summary>\n");
            //sbContent.Append("	public class " + Config.CurrentTableName + "\n");
            //sbContent.Append("	{\n");
            //sbContent.Append("		#region Model\n");

            //string strbl = "";
            //string strsx = "";
            //string strsyq = "";
            //foreach (DataRow drw in dt.Rows)
            //{
            //    strbl += "		private " + Config.GetCSharpType(drw[7].ToString().Trim()) + " _" + drw[3].ToString().Trim() + ";\n";

            //    strsx += "		/// <summary>\n";
            //    strsx += "		/// " + drw[13].ToString() + "\n";
            //    strsx += "		/// </summary>\n";
            //    strsx += "		public " + Config.GetCSharpType(drw[7].ToString().Trim()) + " " + drw[3].ToString().Trim() + "\n";
            //    strsx += "		{\n";
            //    strsx += "			set { _" + drw[3].ToString().Trim() + " = value; }\n";
            //    strsx += "			get { return _" + drw[3].ToString().Trim() + "; }\n";
            //    strsx += "		}\n";

            //    strsyq += "					case \"" + drw[3].ToString().Trim() + "\":\n";
            //    strsyq += "						this." + drw[3].ToString().Trim() + " = (" + Config.GetCSharpType(drw[7].ToString().Trim()) + ")value;\n";
            //    strsyq += "						break;\n";
            //}

            //sbContent.Append(strbl);
            //sbContent.Append(strsx);
            //sbContent.Append("		#endregion Model\n");
            //sbContent.Append("\n");
            //sbContent.Append("		#region Index\n");
            //sbContent.Append("		/// <summary>\n");
            //sbContent.Append("		/// ����������\n");
            //sbContent.Append("		/// </summary>\n");
            //sbContent.Append("		/// <param name=\"strName\">��������</param>\n");
            //sbContent.Append("		public object this[string strName]\n");
            //sbContent.Append("		{\n");
            //sbContent.Append("			set\n");
            //sbContent.Append("			{\n");
            //sbContent.Append("				switch (strName)\n");
            //sbContent.Append("				{ \n");
            //sbContent.Append(strsyq);
            //sbContent.Append("					default:\n");
            //sbContent.Append("						break;\n");
            //sbContent.Append("				}\n");
            //sbContent.Append("			}\n");
            //sbContent.Append("		}\n");
            //sbContent.Append("		#endregion Index\n");
            //sbContent.Append("	}\n");

            //sbContent.Append("\n");
            //sbContent.Append("\n");
            //sbContent.Append("\n");

            //sbContent.Append("	/// <summary>\n");
            //sbContent.Append("	/// " + Config.CurrentTableName + "����(�Զ��弯���ڴ�����Ŀ���ж��ص����ƣ������Զ�������)\n");
            //sbContent.Append("	/// </summary>\n");
            //sbContent.Append("	public class " + Config.CurrentTableName + "Collection:CollectionBase\n");
            //sbContent.Append("	{\n");
            //sbContent.Append("		public " + Config.CurrentTableName + " this[int index]\n");
            //sbContent.Append("		{\n");
            //sbContent.Append("			get { return (" + Config.CurrentTableName + ")List[index]; }\n");
            //sbContent.Append("			set { List[index] = value; }\n");
            //sbContent.Append("		}\n");

            //sbContent.Append("		public int Add(" + Config.CurrentTableName + " value)\n");
            //sbContent.Append("		{\n");
            //sbContent.Append("			return List.Add(value);\n");
            //sbContent.Append("		}\n");

            //sbContent.Append("		public int IndexOf(" + Config.CurrentTableName + " value)\n");
            //sbContent.Append("		{\n");
            //sbContent.Append("			return List.IndexOf(value);\n");
            //sbContent.Append("		}\n");

            //sbContent.Append("		public void Insert(int index, " + Config.CurrentTableName + " value)\n");
            //sbContent.Append("		{\n");
            //sbContent.Append("			List.Insert(index, value);\n");
            //sbContent.Append("		}\n");

            //sbContent.Append("		public void Remove(" + Config.CurrentTableName + " value)\n");
            //sbContent.Append("		{\n");
            //sbContent.Append("			List.Remove(value);\n");
            //sbContent.Append("		}\n");

            //sbContent.Append("		public bool Contains(" + Config.CurrentTableName + " value)\n");
            //sbContent.Append("		{\n");
            //sbContent.Append("			return List.Contains(value);\n");
            //sbContent.Append("		}\n");
            //sbContent.Append("	}\n");

            //sbContent.Append("}");

            this.textEditorControl1.Text = sbContent.ToString();
        }
    }
}
