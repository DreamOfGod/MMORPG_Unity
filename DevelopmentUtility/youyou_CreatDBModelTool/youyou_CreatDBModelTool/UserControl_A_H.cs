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
    public partial class UserControl_A_H : UserControl
    {
        public UserControl_A_H()
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

            if (this.radioButton1.Checked)
            {
                //aspx
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("//TextBox\n");
                sbContent.Append("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">\n");
                foreach (DataRow drw in dt.Rows)
                {
                    sbContent.Append("	<tr>\n");
                    sbContent.Append("		<td width=\"30%\">" + drw[3].ToString().Trim() + "</td>\n");
                    sbContent.Append("		<td width=\"*\"><asp:TextBox id=\"txt" + drw[3].ToString().Trim() + "\" runat=\"server\" Width=\"200px\"></asp:TextBox></td>\n");
                    sbContent.Append("	</tr>\n");
                }
                sbContent.Append("</table>\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//Lable\n");
                sbContent.Append("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">\n");
                foreach (DataRow drw in dt.Rows)
                {
                    sbContent.Append("	<tr>\n");
                    sbContent.Append("		<td width=\"30%\">" + drw[3].ToString().Trim() + "</td>\n");
                    sbContent.Append("		<td width=\"*\"><asp:Label id=\"lab" + drw[3].ToString().Trim() + "\" runat=\"server\"></asp:Label></td>\n");
                    sbContent.Append("	</tr>\n");
                }
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//GridView\n");
                sbContent.Append("<asp:GridView ID=\"GridView1\" runat=\"server\" AutoGenerateColumns=\"False\" Width=\"100%\">\n");
                sbContent.Append("	<Columns>\n");
                foreach (DataRow drw in dt.Rows)
                {
                    sbContent.Append("		<asp:BoundField HeaderText=\"" + drw[3].ToString().Trim() + "\" DataField=\"" + drw[3].ToString().Trim() + "\" ></asp:BoundField>\n");
                }
                sbContent.Append("	</Columns>\n");
                sbContent.Append("</asp:GridView>\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//DataList\n");
                sbContent.Append("<asp:DataList ID=\"DataList1\" runat=\"server\" Width=\"100%\">\n");
                sbContent.Append("	<ItemTemplate>\n");
                foreach (DataRow drw in dt.Rows)
                {
                    sbContent.Append("		<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\n");
                    sbContent.Append("			<tr>\n");
                    sbContent.Append("				<td width=\"30%\">" + drw[3].ToString().Trim() + "</td>\n");
                    sbContent.Append("				<td width=\"*\"><%# DataBinder.Eval(Container.DataItem, \"" + drw[3].ToString().Trim() + "\")%></td>\n");
                    sbContent.Append("			</tr>\n");
                    sbContent.Append("		</table>\n");
                }
                sbContent.Append("	</ItemTemplate>\n");
                sbContent.Append("</asp:DataList>");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//AspNetPager\n");
                sbContent.Append("<webdiyer:AspNetPager ID=\"AspNetPager1\" runat=\"server\" OnPageChanged=\"AspNetPager1_PageChanged\"></webdiyer:AspNetPager>");

                this.textEditorControl1.Text = sbContent.ToString();
            }
            else if (this.radioButton2.Checked)
            {
                //aspx.cs
                StringBuilder sbContent = new StringBuilder();

                string strIdentity = "", strIdentityType = "", strAllFields = "";
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {
                        strIdentityType = Config.GetCSharpType(drw[7].ToString());
                        strIdentity = drw[3].ToString();
                    }
                }

                sbContent.Append("//Add\n");
                sbContent.Append("private void Add()\n");
                sbContent.Append("{\n");
                sbContent.Append("	" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " Model = new " + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + "();\n");
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {

                    }
                    else
                    {
                        if (Config.GetCSharpType(drw[7].ToString().Trim()).Equals("string"))
                        {
                            sbContent.Append("	Model." + drw[3].ToString().Trim() + " = this.txt" + drw[3].ToString().Trim() + ".Text.Trim();\n");
                        }
                        else
                        {
                            sbContent.Append("	Model." + drw[3].ToString().Trim() + " = " + Config.GetCSharpType(drw[7].ToString().Trim()) + ".Parse(this.txt" + drw[3].ToString().Trim() + ".Text.Trim());\n");
                        }
                    }
                }
                sbContent.Append("	new " + Config.CurrentDataBaseName + ".BLL." + Config.CurrentTableName + "().Add(Model);\n");
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//Delete\n");
                sbContent.Append("private void Delete(" + strIdentityType + " " + strIdentity + ")\n");
                sbContent.Append("{\n");
                sbContent.Append("	new " + Config.CurrentDataBaseName + ".BLL." + Config.CurrentTableName + "().Delete(" + strIdentity + ");\n");
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//GetModel\n");
                sbContent.Append("private void GetModel(" + strIdentityType + " " + strIdentity + ")\n");
                sbContent.Append("{\n");
                sbContent.Append("	" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " Model = new " + Config.CurrentDataBaseName + ".BLL." + Config.CurrentTableName + "().GetModel(" + strIdentity + ");\n");
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {
                        sbContent.Append("	this.lab" + drw[3].ToString().Trim() + ".Text = Model." + drw[3].ToString().Trim() + ".ToString();\n");
                    }
                    else
                    {
                        sbContent.Append("	this.lab" + drw[3].ToString().Trim() + ".Text = Model." + drw[3].ToString().Trim() + ".ToString();\n");
                    }
                }
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                foreach (DataRow drw in dt.Rows)
                {
                    strAllFields += drw[3].ToString().Trim() + ", ";
                }
                sbContent.Append("protected void Page_Load(object sender, EventArgs e)\n");
                sbContent.Append("{\n");
                sbContent.Append("	this.strWhere = \"1=1\";//��ѯ����\n");
                sbContent.Append("	this.strGetFields = \"" + strAllFields.TrimEnd(',', ' ') + "\";//��Ҫ���ص���\n");
                sbContent.Append("	this.fldName = \"" + strIdentity + "\";//������ֶ���\n");
                sbContent.Append("	if (!Page.IsPostBack)\n");
                sbContent.Append("	{\n");
                sbContent.Append("		this.GetCountPage();\n");
                sbContent.Append("	}\n");
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//GetCountPage\n");
                sbContent.Append("private void GetCountPage()\n");
                sbContent.Append("{\n");
                sbContent.Append("	PublicLayer.AspNetPagerClass.GetPagerTop(this.AspNetPager1);\n");
                sbContent.Append("	this.AspNetPager1.PageSize = 20;\n");
                sbContent.Append("	this.AspNetPager1.RecordCount = new " + Config.CurrentDataBaseName + ".BLL." + Config.CurrentTableName + "().GetCountPage(this.strWhere);\n");
                sbContent.Append("	this.GetCollectionPage();\n");
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//GetCollectionPage\n");
                sbContent.Append("private void GetCollectionPage()\n");
                sbContent.Append("{\n");
                sbContent.Append("	this.GridView1.DataSource = new " + Config.CurrentDataBaseName + ".BLL." + Config.CurrentTableName + "().GetCollectionPage(this.strGetFields, this.fldName, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, true, this.strWhere);\n");
                sbContent.Append("	this.GridView1.DataBind();\n");
                sbContent.Append("	PublicLayer.AspNetPagerClass.GetPagerEnd(this.AspNetPager1);\n");
                sbContent.Append("}");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("private string _strWhere;\n");
                sbContent.Append("/// <summary>\n");
                sbContent.Append("/// ��ѯ����\n");
                sbContent.Append("/// </summary>\n");
                sbContent.Append("public string strWhere\n");
                sbContent.Append("{\n");
                sbContent.Append("	get { return _strWhere; }\n");
                sbContent.Append("	set { _strWhere = value; }\n");
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("private string _strGetFields;\n");
                sbContent.Append("/// <summary>\n");
                sbContent.Append("/// ��Ҫ���ص���\n");
                sbContent.Append("/// </summary>\n");
                sbContent.Append("public string strGetFields\n");
                sbContent.Append("{\n");
                sbContent.Append("	get { return _strGetFields; }\n");
                sbContent.Append("	set { _strGetFields = value; }\n");
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("private string _fldName;\n");
                sbContent.Append("/// <summary>\n");
                sbContent.Append("/// ������ֶ���\n");
                sbContent.Append("/// </summary>\n");
                sbContent.Append("public string fldName\n");
                sbContent.Append("{\n");
                sbContent.Append("	get { return _fldName; }\n");
                sbContent.Append("	set { _fldName = value; }\n");
                sbContent.Append("}\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("//AspNetPager\n");
                sbContent.Append("protected void AspNetPager1_PageChanged(object sender, EventArgs e)\n");
                sbContent.Append("{\n");
                sbContent.Append("	this.GetCollectionPage();\n");
                sbContent.Append("}");

                this.textEditorControl1.Text = sbContent.ToString();
            }
        }
    }
}