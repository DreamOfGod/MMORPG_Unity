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
    public partial class UserControl_A_F : UserControl
    {
        public UserControl_A_F()
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
        {//实体层
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

            //生成实体层
            DataTable dt = Config.GetDataTableByName(Config.CurrentTableName);
            if (dt == null)
            {
                return;
            }

            this.textEditorControl1.Text = "";

            if (this.radioButton1.Checked)
            {
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("using System;\n");
                sbContent.Append("using System.Configuration;\n");
                sbContent.Append("using System.Collections.Generic;\n");
                sbContent.Append("using System.Text;\n");
                sbContent.Append("\n");
                sbContent.Append("using System.Reflection;\n");
                sbContent.Append("\n");
                sbContent.Append("namespace " + Config.CurrentDataBaseName + ".DALFactory\n");
                sbContent.Append("{\n");
                sbContent.Append("	public sealed class DataAccess\n");
                sbContent.Append("	{\n");
                sbContent.Append("		private static readonly string path = \"" + Config.CurrentDataBaseName + ".SQLServerDAL\";//ConfigurationManager.AppSettings[\"DAL\"];//名称空间的名字\n");
                sbContent.Append("\n");
                sbContent.Append("		private DataAccess() { }\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// 创建对象或从缓存获取\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		public static object CreateObject(string path, string CacheKey)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			object objType = DataCache.GetCache(CacheKey);//从缓存读取\n");
                sbContent.Append("			if (objType == null)\n");
                sbContent.Append("			{\n");
                sbContent.Append("				try\n");
                sbContent.Append("				{\n");
                sbContent.Append("					objType = Assembly.Load(path).CreateInstance(CacheKey);//反射创建\n");
                sbContent.Append("					DataCache.SetCache(CacheKey, objType);// 写入缓存\n");
                sbContent.Append("				}\n");
                sbContent.Append("				catch\n");
                sbContent.Append("				{ }\n");
                sbContent.Append("			}\n");
                sbContent.Append("			return objType;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		public static " + Config.CurrentDataBaseName + ".IDAL.I" + Config.CurrentTableName + " Create" + Config.CurrentTableName + "()\n");
                sbContent.Append("		{\n");
                sbContent.Append("			string CacheKey = path + \"." + Config.CurrentTableName + "\";\n");
                sbContent.Append("			return (" + Config.CurrentDataBaseName + ".IDAL.I" + Config.CurrentTableName + ")CreateObject(path, CacheKey);//返回程序集指定的类实例\n");
                sbContent.Append("		}\n");
                sbContent.Append("	}\n");
                sbContent.Append("}");

                this.textEditorControl1.Text = sbContent.ToString();
            }
            else if (this.radioButton2.Checked)
            { 
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("using System;\n");
                sbContent.Append("using System.Collections.Generic;\n");
                sbContent.Append("using System.Text;\n");
                sbContent.Append("using System.Web;\n");
                sbContent.Append("\n");
                sbContent.Append("namespace " + Config.CurrentDataBaseName + ".DALFactory\n");
                sbContent.Append("{\n");
                sbContent.Append("	/// <summary>\n");
                sbContent.Append("	/// 缓存相关的操作类\n");
                sbContent.Append("	/// </summary>\n");
                sbContent.Append("	public class DataCache\n");
                sbContent.Append("	{\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// 获取当前应用程序指定CacheKey的Cache值\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"CacheKey\"></param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static object GetCache(string CacheKey)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			System.Web.Caching.Cache objCache = HttpRuntime.Cache;\n");
                sbContent.Append("			return objCache[CacheKey];\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// 设置当前应用程序指定CacheKey的Cache值\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"CacheKey\"></param>\n");
                sbContent.Append("		/// <param name=\"objObject\"></param>\n");
                sbContent.Append("		public static void SetCache(string CacheKey, object objObject)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			System.Web.Caching.Cache objCache = HttpRuntime.Cache;\n");
                sbContent.Append("			objCache.Insert(CacheKey, objObject);\n");
                sbContent.Append("		}\n");
                sbContent.Append("	}\n");
                sbContent.Append("}");

                this.textEditorControl1.Text = sbContent.ToString();
            }
        }
    }
}