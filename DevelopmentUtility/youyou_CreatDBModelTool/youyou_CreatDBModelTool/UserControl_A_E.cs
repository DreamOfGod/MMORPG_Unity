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
        {//IDAL
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

            sbContent.Append("using System;\n");
            sbContent.Append("using System.Collections;\n");
            sbContent.Append("using System.Collections.Generic;\n");
            sbContent.Append("using System.Text;\n");
            sbContent.Append("\n");
            sbContent.Append("namespace " + Config.CurrentDataBaseName + ".IDAL\n");
            sbContent.Append("{\n");
            sbContent.Append("	public interface I" + Config.CurrentTableName + "\n");
            sbContent.Append("	{\n");
            //添加开始
            if (this.cb_Add.Checked)
            {
                string strAddIdentityType = "";
                foreach (DataRow drw in dt.Rows)
                {
                    if (drw[4].ToString().Equals("√") && drw[5].ToString().Equals("√"))
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
                sbContent.Append("		/// 添加\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"Model\">" + Config.CurrentTableName + "对象</param>\n");
                sbContent.Append("		/// <returns>主键</returns>\n");
                sbContent.Append("		" + strAddIdentityType + " Add(" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " Model);\n");
                sbContent.Append("		#endregion Add\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //添加结束
            //修改开始
            if (this.cb_Update.Checked)
            {
                sbContent.Append("		#region Update\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// 修改\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"Model\">" + Config.CurrentTableName + "对象</param>\n");
                sbContent.Append("		void Update(" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " Model);\n");
                sbContent.Append("		#endregion Update\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //修改结束
            //删除开始
            if (this.cb_Delete.Checked)
            {
                string strDeleteIdentity = "", strDeleteIdentityType = "", strDeleteIdentityType2 = "";

                foreach (DataRow drw in dt.Rows)
                {

                    if (drw[4].ToString().Equals("√") && drw[5].ToString().Equals("√"))
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
                sbContent.Append("		/// 删除\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"" + strDeleteIdentity + "\">主键</param>\n");
                sbContent.Append("		void Delete(" + strDeleteIdentityType2 + " " + strDeleteIdentity + ");\n");
                sbContent.Append("		#endregion Delete\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //删除结束
            //查询实体开始
            if (this.cb_GetModel.Checked)
            {
                string strGetModelIdentity = "", strGetModelIdentityType2 = "";
                int iGetModel = 0;
                foreach (DataRow drw in dt.Rows)
                {
                    iGetModel++;
                    if (drw[4].ToString().Equals("√") && drw[5].ToString().Equals("√"))
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
                sbContent.Append("		/// 查询实体\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"" + strGetModelIdentity + "\">主键</param>\n");
                sbContent.Append("		/// <returns>" + Config.CurrentTableName + "对象</returns>\n");
                sbContent.Append("		" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + " GetModel(" + strGetModelIdentityType2 + " " + strGetModelIdentity + ");\n");
                sbContent.Append("		#endregion GetModel\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //查询实体结束
            //查询集合开始
            if (this.cb_GetCollection.Checked)
            {
                sbContent.Append("		#region GetCollection\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// 查询集合" + Config.CurrentTableName + "\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <returns>集合" + Config.CurrentTableName + "</returns>\n");
                sbContent.Append("		" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + "Collection GetCollection();\n");
                sbContent.Append("		#endregion GetCollection\n");

                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
            }
            //查询集合结束
            //查询总数量开始
            if (this.cb_GetCollectionPage.Checked)
            {
                sbContent.Append("		#region GetCountPage\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// 获取总数量\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"strWhere\">查询条件 (注意: 不要加 where)</param>\n");
                sbContent.Append("		/// <returns>总数量</returns>\n");
                sbContent.Append("		int GetCountPage(string strWhere);\n");
                sbContent.Append("		#endregion GetCountPage\n");

                //查询总数量结束
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                //查询分页集合开始
                sbContent.Append("		#region GetCollectionPage\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// 查询分页集合\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"strGetFields\">需要返回的列</param>\n");
                sbContent.Append("		/// <param name=\"fldName\">排序的字段名</param>\n");
                sbContent.Append("		/// <param name=\"PageSize\">页尺寸</param>\n");
                sbContent.Append("		/// <param name=\"PageIndex\">页码</param>\n");
                sbContent.Append("		/// <param name=\"OrderType\">设置排序类型, true降序 false升序</param>\n");
                sbContent.Append("		/// <param name=\"strWhere\">查询条件 (注意: 不要加 where)</param>\n");
                sbContent.Append("		/// <returns>分页集合</returns>\n");
                sbContent.Append("		IList<" + Config.CurrentDataBaseName + ".Model." + Config.CurrentTableName + "> GetCollectionPage(string strGetFields, string fldName, int PageSize, int PageIndex, bool OrderType, string strWhere);\n");
                sbContent.Append("		#endregion GetCollectionPage\n");
            }
            //查询分页集合结束
            sbContent.Append("	}\n");
            sbContent.Append("}");
            this.textEditorControl1.Text = sbContent.ToString();
        }
    }
}
