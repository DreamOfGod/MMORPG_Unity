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
    //���ɴ洢����
    public partial class UserControl_A_B : UserControl
    {
        public UserControl_A_B()
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
        {//�洢����
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

            //���ɴ洢����
            DataTable dt = Config.GetDataTableByName(Config.CurrentTableName);
            if (dt == null)
            {
                return;
            }
            this.textEditorControl1.Text = "";

            StringBuilder sbContent = new StringBuilder();

            //��Ӳ�����ʼ
            string strAddcs = "", strAddzd = "", strAddz = "", strAddIdentity = "";
            foreach (DataRow drw in dt.Rows)
            {
                if (drw[3].ToString().Trim().Equals("Id"))
                {
                    continue;
                }

                if (!drw[9].ToString().Trim().Equals("0"))
                {
                    strAddcs += "	@" + drw[3].ToString() + " " + drw[7].ToString() + ",\n";
                }
                else
                {
                    strAddcs += "	@" + drw[3].ToString() + " " + drw[7].ToString() + "(" + drw[8].ToString() + "),\n";
                }

                strAddzd += "" + drw[3].ToString() + ",";
                strAddz += "@" + drw[3].ToString() + ",";
            }

            if (this.cb_Add.Checked)
            {
                sbContent.Append("-- =============================================\n");
                sbContent.Append("-- Author:����-����\n");
                sbContent.Append("-- Create Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                sbContent.Append("-- Description:���\n");
                sbContent.Append("-- =============================================\n");
                sbContent.AppendFormat("CREATE PROCEDURE [{0}_Create]\n", Config.CurrentTableName);
                sbContent.Append("(\n");
                sbContent.Append("    @Id int Output,\n");
                sbContent.Append("	" + strAddcs.Trim() + "\n");
                sbContent.Append("    @RetMsg nvarchar(255) Output\n");
                sbContent.Append(")\n");
                sbContent.Append("AS\n");
                sbContent.Append("BEGIN\n");
                sbContent.Append("	BEGIN TRY\n");
                sbContent.Append("		SET NOCOUNT ON;\n");
                sbContent.Append("		--�Զ����߼���ʼ\n");
                sbContent.Append("		\n");
                sbContent.Append("		--�Զ����߼���\n");
                sbContent.Append("		\n");
                sbContent.AppendFormat("        INSERT INTO {0}\n", Config.CurrentTableName);
                sbContent.Append("			(" + strAddzd.TrimEnd(',') + ")\n");
                sbContent.Append("		VALUES\n");
                sbContent.Append("			(" + strAddz.TrimEnd(',') + ")\n");
                sbContent.Append("	\n");
                sbContent.Append("		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)\n");
                sbContent.Append("			BEGIN\n");
                sbContent.AppendFormat("	            SET @Id = IDENT_CURRENT('{0}')\n", Config.CurrentTableName);
                sbContent.Append("				SET @RetMsg = '��ӳɹ�';\n");
                sbContent.Append("				RETURN 1;\n");
                sbContent.Append("			END\n");
                sbContent.Append("		ELSE\n");
                sbContent.Append("			BEGIN\n");
                sbContent.Append("				SET @RetMsg = '���ʧ��';\n");
                sbContent.Append("				RETURN -2;\n");
                sbContent.Append("			END\n");
                sbContent.Append("    \n");
                sbContent.Append("		SET NOCOUNT OFF;\n");
                sbContent.Append("	END TRY\n");
                sbContent.Append("	BEGIN CATCH\n");
                sbContent.Append("		SET @RetMsg = ERROR_MESSAGE();\n");
                sbContent.Append("		RETURN -1;\n");
                sbContent.Append("	END CATCH\n");
                sbContent.Append("END\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("GO\n");
            }
            //��Ӳ�������
            //�޸Ĳ�����ʼ
            if (this.cb_Update.Checked)
            {
                string strUpdatecs = "", strUpdatezd = "", strUpdateIdentity = "";
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[3].ToString().Trim().Equals("Id") || drw[3].ToString().Trim().Equals("Status"))
                    {
                        continue;
                    }

                    if (!drw[9].ToString().Trim().Equals("0"))
                    {
                        strUpdatecs += "	@" + drw[3].ToString() + " " + drw[7].ToString() + ",\n";
                    }
                    else
                    {
                        strUpdatecs += "	@" + drw[3].ToString() + " " + drw[7].ToString() + "(" + drw[8].ToString() + "),\n";
                    }

                    strUpdatezd += "		    " + drw[3].ToString() + "=@" + drw[3].ToString() + ",\n";
                }
                sbContent.Append("-- =============================================\n");
                sbContent.Append("-- Author:����-����\n");
                sbContent.Append("-- Create Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                sbContent.Append("-- Description:�޸�\n");
                sbContent.Append("-- =============================================\n");
                sbContent.AppendFormat("CREATE PROCEDURE [{0}_Update]\n", Config.CurrentTableName);
                sbContent.Append("(\n");
                sbContent.Append("    @Id int,\n");
                sbContent.Append("    @Status tinyint,\n");
                sbContent.Append("	" + strUpdatecs.Trim() + "\n");
                sbContent.Append("    @RetMsg nvarchar(255) Output\n");
                sbContent.Append(")\n");
                sbContent.Append("AS\n");
                sbContent.Append("BEGIN\n");
                sbContent.Append("	BEGIN TRY\n");
                sbContent.Append("		SET NOCOUNT ON;\n");
                sbContent.Append("		--�Զ����߼���ʼ\n");
                sbContent.Append("		\n");
                sbContent.Append("		--�Զ����߼�����\n");
                sbContent.Append("        \n");
                sbContent.Append("		UPDATE\n");
                sbContent.AppendFormat("			{0}\n", Config.CurrentTableName);
                sbContent.Append("		SET\n");
                sbContent.Append("		    Status=@Status,\n");
                sbContent.Append("		    " + strUpdatezd.Trim().TrimEnd(',') + "\n");
                sbContent.Append("		WHERE\n");
                sbContent.Append("		    Id=@Id\n");
                sbContent.Append("			\n");
                sbContent.Append("		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)\n");
                sbContent.Append("			BEGIN\n");
                sbContent.Append("				SET @RetMsg = '�޸ĳɹ�';\n");
                sbContent.Append("				RETURN 1;\n");
                sbContent.Append("			END\n");
                sbContent.Append("		ELSE\n");
                sbContent.Append("			BEGIN\n");
                sbContent.Append("				SET @RetMsg = '�޸�ʧ��';\n");
                sbContent.Append("				RETURN -2;\n");
                sbContent.Append("			END\n");
                sbContent.Append("			\n");
                sbContent.Append("		SET NOCOUNT OFF;\n");
                sbContent.Append("	END TRY\n");
                sbContent.Append("	BEGIN CATCH\n");
                sbContent.Append("		SET @RetMsg = ERROR_MESSAGE();\n");
                sbContent.Append("		RETURN -1;\n");
                sbContent.Append("	END CATCH\n");
                sbContent.Append("END\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("GO\n");
            }
            //�޸Ĳ�������
            //ɾ��������ʼ
            if (this.cb_Delete.Checked)
            {
                string strDeletecs = "", strDeleteIdentity = "";
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {
                        if (!drw[9].ToString().Trim().Equals("0"))
                        {
                            strDeletecs += "		@" + drw[3].ToString() + " " + drw[7].ToString() + "";
                        }
                        else
                        {
                            strDeletecs += "		@" + drw[3].ToString() + " " + drw[7].ToString() + "(" + drw[8].ToString() + ")";
                        }
                        strDeleteIdentity = "" + drw[3].ToString() + "=@" + drw[3].ToString() + "";
                    }
                }
                sbContent.Append("-- =============================================\n");
                sbContent.Append("-- Author:����-����\n");
                sbContent.Append("-- Create Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                sbContent.Append("-- Description:ɾ��\n");
                sbContent.Append("-- =============================================\n");
                sbContent.AppendFormat("CREATE PROCEDURE [{0}_Delete]\n", Config.CurrentTableName);
                sbContent.Append("(\n");
                sbContent.Append("    @Id int,\n");
                sbContent.Append("    @RetMsg nvarchar(255) Output\n");
                sbContent.Append(")\n");
                sbContent.Append("AS\n");
                sbContent.Append("BEGIN\n");
                sbContent.Append("	BEGIN TRY\n");
                sbContent.Append("		SET NOCOUNT ON;\n");
                sbContent.Append("		--�Զ����߼���ʼ\n");
                sbContent.Append("		\n");
                sbContent.Append("		--�Զ����߼�����\n");
                sbContent.AppendFormat("		UPDATE {0} SET Status=0 WHERE Id=@Id\n", Config.CurrentTableName);
                sbContent.Append("			\n");
                sbContent.Append("		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)\n");
                sbContent.Append("			BEGIN\n");
                sbContent.Append("				SET @RetMsg = 'ɾ���ɹ�';\n");
                sbContent.Append("				RETURN 1;\n");
                sbContent.Append("			END\n");
                sbContent.Append("		ELSE\n");
                sbContent.Append("			BEGIN\n");
                sbContent.Append("				SET @RetMsg = 'ɾ��ʧ��';\n");
                sbContent.Append("				RETURN -2;\n");
                sbContent.Append("			END\n");
                sbContent.Append("			\n");
                sbContent.Append("		SET NOCOUNT OFF;\n");
                sbContent.Append("	END TRY\n");
                sbContent.Append("	BEGIN CATCH\n");
                sbContent.Append("		SET @RetMsg = ERROR_MESSAGE();\n");
                sbContent.Append("		RETURN -1;\n");
                sbContent.Append("	END CATCH\n");
                sbContent.Append("END\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("GO\n");
            }
            //ɾ����������
            //��ѯʵ�忪ʼ
            if (this.cb_GetModel.Checked)
            {
                string strGetModelcs = "", strGetModelzd = "", strGetModelIdentity = "";
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[4].ToString().Equals("��") && drw[5].ToString().Equals("��"))
                    {
                        if (!drw[9].ToString().Trim().Equals("0"))
                        {
                            strGetModelcs += "		@" + drw[3].ToString() + " " + drw[7].ToString() + ",\n";
                        }
                        else
                        {
                            strGetModelcs += "		@" + drw[3].ToString() + " " + drw[7].ToString() + "(" + drw[8].ToString() + "),\n";
                        }
                        strGetModelIdentity = "" + drw[3].ToString() + "=@" + drw[3].ToString() + "";
                    }
                    else
                    {
                        if (!drw[9].ToString().Trim().Equals("0"))
                        {
                            strGetModelcs += "		@" + drw[3].ToString() + " " + drw[7].ToString() + " OUTPUT,\n";
                        }
                        else
                        {
                            strGetModelcs += "		@" + drw[3].ToString() + " " + drw[7].ToString() + "(" + drw[8].ToString() + ") OUTPUT,\n";
                        }
                        strGetModelzd += "			@" + drw[3].ToString() + "=" + drw[3].ToString() + ",\n";
                    }
                }
                sbContent.Append("-- =============================================\n");
                sbContent.Append("-- Author:����-����\n");
                sbContent.Append("-- Create Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                sbContent.Append("-- Description:��ѯʵ��\n");
                sbContent.Append("-- =============================================\n");
                sbContent.AppendFormat("CREATE PROCEDURE [{0}_GetEntity]\n", Config.CurrentTableName);
                sbContent.Append("(\n");
                sbContent.Append("    @Id int\n");
                sbContent.Append(")\n");
                sbContent.Append("AS\n");
                sbContent.Append("BEGIN\n");
                sbContent.Append("	SET NOCOUNT ON;\n");
                sbContent.Append("	\n");
                sbContent.AppendFormat("		SELECT * FROM {0} WHERE Id=@Id\n", Config.CurrentTableName);
                sbContent.Append("	\n");
                sbContent.Append("	SET NOCOUNT OFF;\n");
                sbContent.Append("END\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("GO\n");
            }
            //��ѯʵ�����
            this.textEditorControl1.Text = sbContent.ToString();
        }
    }
}
