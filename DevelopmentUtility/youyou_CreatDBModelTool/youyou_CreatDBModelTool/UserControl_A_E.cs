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
    public partial class UserControl_A_E : UserControl
    {
        public UserControl_A_E()
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
        {//IDAL
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

            //�������ݲ�
            DataTable dt = Config.GetDataTableByName(Config.CurrentTableName);
            if (dt == null)
            {
                return;
            }
            this.textEditorControl1.Text = "";

            StringBuilder sbContent = new StringBuilder();

            sbContent.Append("using System;\n");
            sbContent.Append("using System.Collections;\n");
            sbContent.Append("using System.Collections.Generic;\n");
            sbContent.Append("using System.Text;\n");
            sbContent.Append("\n");
            sbContent.Append("namespace " + Config.CurrentDataBaseName + ".IDAL\n");
            sbContent.Append("{\n");
            sbContent.Append("	public interface I" + Config.CurrentTableName + "\n");
            sbContent.Append("	{\n");
            //��ӿ�ʼ
            if (this.cb_Add.Checked)
            {
                string strAddIdentityType = "";
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {
                        strAddIdentityType = Config.GetCSharpType(drw[7].ToString());
                    }
                    else
                    {

                    }
                }

                sbContent.Append("		#region Add\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ���\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"Model\">" + Config.CurrentTableName + "����</param>\n");
                sbContent.Append("		/// <returns>����</returns>\n");
                sbContent.Append("		" + strAddIdentityType + " Add(" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " Model);\n");
                sbContent.Append("		#endregion Add\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //��ӽ���
            //�޸Ŀ�ʼ
            if (this.cb_Update.Checked)
            {
                sbContent.Append("		#region Update\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �޸�\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"Model\">" + Config.CurrentTableName + "����</param>\n");
                sbContent.Append("		void Update(" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " Model);\n");
                sbContent.Append("		#endregion Update\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //�޸Ľ���
            //ɾ����ʼ
            if (this.cb_Delete.Checked)
            {
                string strDeleteIdentity = "", strDeleteIdentityType = "", strDeleteIdentityType2 = "";

                foreach (DataRow drw in dt.Rows)
                {

                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {
                        strDeleteIdentity = drw[3].ToString();
                        strDeleteIdentityType = Config.GetSqlDbType(drw[7].ToString());
                        strDeleteIdentityType2 = Config.GetCSharpType(drw[7].ToString());
                    }
                    else
                    {

                    }
                }
                sbContent.Append("		#region Delete\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ɾ��\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"" + strDeleteIdentity + "\">����</param>\n");
                sbContent.Append("		void Delete(" + strDeleteIdentityType2 + " " + strDeleteIdentity + ");\n");
                sbContent.Append("		#endregion Delete\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //ɾ������
            //��ѯʵ�忪ʼ
            if (this.cb_GetModel.Checked)
            {
                string strGetModelIdentity = "", strGetModelIdentityType2 = "";
                int iGetModel = 0;
                foreach (DataRow drw in dt.Rows)
                {
                    iGetModel++;
                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {
                        strGetModelIdentity = drw[3].ToString();
                        strGetModelIdentityType2 = Config.GetCSharpType(drw[7].ToString());
                    }
                    else
                    {

                    }
                }
                sbContent.Append("		#region GetModel\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ѯʵ��\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"" + strGetModelIdentity + "\">����</param>\n");
                sbContent.Append("		/// <returns>" + Config.CurrentTableName + "����</returns>\n");
                sbContent.Append("		" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " GetModel(" + strGetModelIdentityType2 + " " + strGetModelIdentity + ");\n");
                sbContent.Append("		#endregion GetModel\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //��ѯʵ�����
            //��ѯ���Ͽ�ʼ
            if (this.cb_GetCollection.Checked)
            {
                sbContent.Append("		#region GetCollection\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ѯ����" + Config.CurrentTableName + "\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <returns>����" + Config.CurrentTableName + "</returns>\n");
                sbContent.Append("		" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + "Collection GetCollection();\n");
                sbContent.Append("		#endregion GetCollection\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //��ѯ���Ͻ���
            //��ѯ��������ʼ
            if (this.cb_GetCollectionPage.Checked)
            {
                sbContent.Append("		#region GetCountPage\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ȡ������\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"strWhere\">��ѯ���� (ע��: ��Ҫ�� where)</param>\n");
                sbContent.Append("		/// <returns>������</returns>\n");
                sbContent.Append("		int GetCountPage(string strWhere);\n");
                sbContent.Append("		#endregion GetCountPage\n");

                //��ѯ����������
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                //��ѯ��ҳ���Ͽ�ʼ
                sbContent.Append("		#region GetCollectionPage\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ѯ��ҳ����\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"strGetFields\">��Ҫ���ص���</param>\n");
                sbContent.Append("		/// <param name=\"fldName\">������ֶ���</param>\n");
                sbContent.Append("		/// <param name=\"PageSize\">ҳ�ߴ�</param>\n");
                sbContent.Append("		/// <param name=\"PageIndex\">ҳ��</param>\n");
                sbContent.Append("		/// <param name=\"OrderType\">������������, true���� false����</param>\n");
                sbContent.Append("		/// <param name=\"strWhere\">��ѯ���� (ע��: ��Ҫ�� where)</param>\n");
                sbContent.Append("		/// <returns>��ҳ����</returns>\n");
                sbContent.Append("		IList<" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + "> GetCollectionPage(string strGetFields, string fldName, int PageSize, int PageIndex, bool OrderType, string strWhere);\n");
                sbContent.Append("		#endregion GetCollectionPage\n");
            }
            //��ѯ��ҳ���Ͻ���
            sbContent.Append("	}\n");
            sbContent.Append("}");
            this.textEditorControl1.Text = sbContent.ToString();
        }
    }
}
