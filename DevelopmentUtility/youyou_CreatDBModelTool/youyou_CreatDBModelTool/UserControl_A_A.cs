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
    public partial class UserControl_A_A : UserControl
    {
        public UserControl_A_A()
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
        {
            if (this.radioButton1.Checked)
            {//StringClass
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("using System;\n");
                sbContent.Append("using System.Collections;\n");
                sbContent.Append("using System.Collections.Generic;\n");
                sbContent.Append("using System.Text;\n");
                sbContent.Append("using System.Text.RegularExpressions;\n");
                sbContent.Append("\n");
                sbContent.Append("namespace PublicLayer\n");
                sbContent.Append("{\n");
                sbContent.Append("	public class StringClass\n");
                sbContent.Append("	{\n");
                sbContent.Append("		#region cutString\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ȡ�ַ�\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"strInput\">������ַ���</param>\n");
                sbContent.Append("		/// <param name=\"intLen\">����</param>\n");
                sbContent.Append("		/// <returns>��ȡ����ַ���</returns>\n");
                sbContent.Append("		public static string cutString(string strInput, int intLen)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			strInput = strInput.Trim();\n");
                sbContent.Append("			byte[] myByte = System.Text.Encoding.Default.GetBytes(strInput);\n");
                sbContent.Append("			if (myByte.Length > intLen)\n");
                sbContent.Append("			{\n");
                sbContent.Append("				//��ȡ����\n");
                sbContent.Append("				string resultStr = \"\";\n");
                sbContent.Append("				for (int i = 0; i < strInput.Length; i++)\n");
                sbContent.Append("				{\n");
                sbContent.Append("					byte[] tempByte = System.Text.Encoding.Default.GetBytes(resultStr);\n");
                sbContent.Append("					if (tempByte.Length < intLen - 4)\n");
                sbContent.Append("					{\n");
                sbContent.Append("						resultStr += strInput.Substring(i, 1);\n");
                sbContent.Append("					}\n");
                sbContent.Append("					else\n");
                sbContent.Append("					{\n");
                sbContent.Append("						break;\n");
                sbContent.Append("					}\n");
                sbContent.Append("				}\n");
                sbContent.Append("				return resultStr + \"..\";\n");
                sbContent.Append("			}\n");
                sbContent.Append("			else\n");
                sbContent.Append("			{\n");
                sbContent.Append("				return strInput;\n");
                sbContent.Append("			}\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion cutString\n");
                sbContent.Append("\n");
                sbContent.Append("		#region ChangeText\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �ؼ��ֱ�ɫ\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"oText\">Դ����</param>\n");
                sbContent.Append("		/// <param name=\"oKeyWords\">�ؼ���</param>\n");
                sbContent.Append("		/// <returns>�滻�������</returns>\n");
                sbContent.Append("		public static string ChangeText(object oText, object oKeyWords)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Regex reg = new Regex(oKeyWords.ToString());\n");
                sbContent.Append("			return reg.Replace(oText.ToString(), \"<span style='color:#FF0000;'>\" + oKeyWords.ToString() + \"</span>\");\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion ChangeText\n");

                sbContent.Append("	}\n");
                sbContent.Append("}");
                this.textEditorControl1.Text = sbContent.ToString();
            }
            else if (this.radioButton2.Checked)
            {
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("using System;\n");
                sbContent.Append("\n");
                sbContent.Append("namespace PublicLayer\n");
                sbContent.Append("{\n");
                sbContent.Append("	public class JavaScript\n");
                sbContent.Append("	{\n");
                sbContent.Append("		#region GetJavaScript\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ������ʾ��\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"text\">��ʾ����</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string GetJavaScript(string text)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return \"<script>alert('\" + text + \"');\" + \"</\" + \"script>\";\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ������ʾ��ת����ҳ\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"text\">��ʾ����</param>\n");
                sbContent.Append("		/// <param name=\"page\">��ҳ</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string GetJavaScript(string text, string page)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return \"<script>alert('\" + text + \"');location.href='\" + page + \"';\" + \"</\" + \"script>\";\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetJavaScript\n");
                sbContent.Append("\n");
                sbContent.Append("		#region GetJavaScriptBack\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ������ʼҳ\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string GetJavaScriptBack()\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return \"<script>parent.contents.history.go(0);\" + \"</\" + \"script>\";\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetJavaScriptBack\n");
                sbContent.Append("\n");
                sbContent.Append("		#region GetJavaScriptMainBack\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ת����ҳ\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"page\"></param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string GetJavaScriptMainBack(string page)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return \"<script>location.href='\" + page + \"';\" + \"</\" + \"script>\";\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetJavaScriptMainBack\n");
                sbContent.Append("\n");
                sbContent.Append("		#region GetJavaScriptOpenNewWin\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ���´��ڴ�ҳ��\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"page\">ҳ��</param>\n");
                sbContent.Append("		/// <param name=\"pageName\">��������</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string GetJavaScriptOpenNewWin(string page, string pageName)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return \"<script>window.open('\" + page + \"', '\" + pageName + \"');\" + \"</\" + \"script>\";\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetJavaScriptOpenNewWin\n");
                sbContent.Append("\n");
                sbContent.Append("		#region GetJavaScriptTextAndClose\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ������ʾ�򲢹رմ���\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"text\">��ʾ����</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string GetJavaScriptTextAndClose(string text)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return \"<script>alert('\" + text + \"');window.close();\" + \"</\" + \"script>\";\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetJavaScriptTextAndClose\n");
                sbContent.Append("\n");
                sbContent.Append("		#region GetJavaScriptClose\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �رմ���\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string GetJavaScriptClose()\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return \"<script>window.close();\" + \"</\" + \"script>\";\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetJavaScriptClose\n");
                sbContent.Append("	}\n");
                sbContent.Append("}");

                this.textEditorControl1.Text = sbContent.ToString();
            }
            else if (this.radioButton3.Checked)
            {
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("using System;\n");
                sbContent.Append("using System.Configuration;\n");
                sbContent.Append("using System.IO;\n");
                sbContent.Append("using System.Security.Cryptography;\n");
                sbContent.Append("using System.Web;\n");
                sbContent.Append("using System.Web.Security;\n");
                sbContent.Append("\n");
                sbContent.Append("namespace PublicLayer\n");
                sbContent.Append("{\n");
                sbContent.Append("	public class Encrypt\n");
                sbContent.Append("	{\n");
                sbContent.Append("		#region Kdc\n");
                sbContent.Append("\n");
                sbContent.Append("		private static string KEY_64 = \"JasmineT\";\n");
                sbContent.Append("		private static string IV_64 = \"JasmineT\";\n");
                sbContent.Append("\n");
                sbContent.Append("		#region Encode\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// Kdc����\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"data\">����ǰ�ַ�</param>\n");
                sbContent.Append("		/// <returns>���ܺ��ַ�</returns>\n");
                sbContent.Append("		public static string Encode(string data)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);\n");
                sbContent.Append("			byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);\n");
                sbContent.Append("\n");
                sbContent.Append("			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();\n");
                sbContent.Append("			int i = cryptoProvider.KeySize;\n");
                sbContent.Append("			MemoryStream ms = new MemoryStream();\n");
                sbContent.Append("			CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);\n");
                sbContent.Append("\n");
                sbContent.Append("			StreamWriter sw = new StreamWriter(cst);\n");
                sbContent.Append("			sw.Write(data);\n");
                sbContent.Append("			sw.Flush();\n");
                sbContent.Append("			cst.FlushFinalBlock();\n");
                sbContent.Append("			sw.Flush();\n");
                sbContent.Append("			return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion Encode\n");
                sbContent.Append("\n");
                sbContent.Append("		#region Decode\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// Kdc����\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"data\">����ǰ�ַ�</param>\n");
                sbContent.Append("		/// <returns>���ܺ��ַ�</returns>\n");
                sbContent.Append("		public static string Decode(string data)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);\n");
                sbContent.Append("			byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);\n");
                sbContent.Append("\n");
                sbContent.Append("			byte[] byEnc;\n");
                sbContent.Append("			try\n");
                sbContent.Append("			{\n");
                sbContent.Append("				byEnc = Convert.FromBase64String(data);\n");
                sbContent.Append("			}\n");
                sbContent.Append("			catch\n");
                sbContent.Append("			{\n");
                sbContent.Append("				return null;\n");
                sbContent.Append("			}\n");
                sbContent.Append("\n");
                sbContent.Append("			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();\n");
                sbContent.Append("			MemoryStream ms = new MemoryStream(byEnc);\n");
                sbContent.Append("			CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);\n");
                sbContent.Append("			StreamReader sr = new StreamReader(cst);\n");
                sbContent.Append("			return sr.ReadToEnd();\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion Decode\n");
                sbContent.Append("\n");
                sbContent.Append("		#endregion Kdc\n");
                sbContent.Append("\n");
                sbContent.Append("		#region Sha1\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// Sha1����\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"strText\">����ǰ�ַ�</param>\n");
                sbContent.Append("		/// <returns>���ܺ��ַ�</returns>\n");
                sbContent.Append("		public static string Sha1( string strText)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return FormsAuthentication.HashPasswordForStoringInConfigFile(strText.Trim(), \"sha1\");\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion Sha1\n");
                sbContent.Append("\n");
                sbContent.Append("		#region Md5\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// Md5����\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"strText\">����ǰ�ַ�</param>\n");
                sbContent.Append("		/// <returns>���ܺ��ַ�</returns>\n");
                sbContent.Append("		public static string Md5(string strText)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return FormsAuthentication.HashPasswordForStoringInConfigFile(strText.Trim(), \"md5\");\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		public static string Md5(string strText, bool Is16)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return FormsAuthentication.HashPasswordForStoringInConfigFile(strText.Trim(), \"md5\").Substring(8, 16);\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion Md5\n");
                sbContent.Append("	}\n");
                sbContent.Append("}");

                this.textEditorControl1.Text = sbContent.ToString();
            }
            else if (this.radioButton4.Checked)
            {
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("using System;\n");
                sbContent.Append("using System.Text;\n");
                sbContent.Append("using System.Web;\n");
                sbContent.Append("using System.Web.UI.WebControls;\n");
                sbContent.Append("using System.Text.RegularExpressions;\n");
                sbContent.Append("\n");
                sbContent.Append("namespace PublicLayer\n");
                sbContent.Append("{\n");
                sbContent.Append("	public class Validate\n");
                sbContent.Append("	{\n");
                sbContent.Append("		private static Regex RegNumber = new Regex(\"^[0-9]+$\");\n");
                sbContent.Append("		private static Regex RegNumberSign = new Regex(\"^[+-]?[0-9]+$\");\n");
                sbContent.Append("		private static Regex RegDecimal = new Regex(\"^[0-9]+[.]?[0-9]+$\");\n");
                sbContent.Append("		private static Regex RegDecimalSign = new Regex(\"^[+-]?[0-9]+[.]?[0-9]+$\");\n");
                sbContent.Append("		private static Regex RegEmail = new Regex(\"^[\\\\w-]+@[\\\\w-]+\\\\.(com|net|org|edu|mil|tv|biz|info)$\");\n");
                sbContent.Append("		private static Regex RegCHZN = new Regex(\"[\\u4e00-\\u9fa5]\");\n");
                sbContent.Append("\n");
                sbContent.Append("		#region �����ַ������\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶�����\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"req\">Request</param>\n");
                sbContent.Append("		/// <param name=\"inputKey\">Request�ļ�ֵ</param>\n");
                sbContent.Append("		/// <param name=\"maxLen\">��󳤶�</param>\n");
                sbContent.Append("		/// <returns>����Request��ѯ�ַ���</returns>\n");
                sbContent.Append("		public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			string retVal = string.Empty;\n");
                sbContent.Append("			if(inputKey != null && inputKey != string.Empty)\n");
                sbContent.Append("			{\n");
                sbContent.Append("				retVal = req.QueryString[inputKey];\n");
                sbContent.Append("				if(null == retVal)\n");
                sbContent.Append("					retVal = req.Form[inputKey];\n");
                sbContent.Append("				if(null != retVal)\n");
                sbContent.Append("				{\n");
                sbContent.Append("					retVal = SqlText(retVal, maxLen);\n");
                sbContent.Append("					if(!IsNumber(retVal))\n");
                sbContent.Append("						retVal = string.Empty;\n");
                sbContent.Append("				}\n");
                sbContent.Append("			}\n");
                sbContent.Append("			if(retVal == null)\n");
                sbContent.Append("				retVal = string.Empty;\n");
                sbContent.Append("			return retVal;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �Ƿ������ַ���\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"inputData\">�����ַ���</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static bool IsNumber(string inputData)\n");
                sbContent.Append("		{");
                sbContent.Append("			Match m = RegNumber.Match(inputData);\n");
                sbContent.Append("			return m.Success;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �Ƿ������ַ��� �ɴ�������\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"inputData\">�����ַ���</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static bool IsNumberSign(string inputData)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Match m = RegNumberSign.Match(inputData);\n");
                sbContent.Append("			return m.Success;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �Ƿ��Ǹ�����\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"inputData\">�����ַ���</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static bool IsDecimal(string inputData)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Match m = RegDecimal.Match(inputData);\n");
                sbContent.Append("			return m.Success;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �Ƿ��Ǹ����� �ɴ�������\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"inputData\">�����ַ���</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static bool IsDecimalSign(string inputData)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Match m = RegDecimalSign.Match(inputData);\n");
                sbContent.Append("			return m.Success;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		#endregion\n");
                sbContent.Append("\n");
                sbContent.Append("		#region ���ļ��\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ����Ƿ��������ַ�\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"inputData\"></param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static bool IsHasCHZN(string inputData)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Match m = RegCHZN.Match(inputData);\n");
                sbContent.Append("			return m.Success;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		#endregion\n");
                sbContent.Append("\n");
                sbContent.Append("		#region �ʼ���ַ\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// �Ƿ��Ǹ����� �ɴ�������\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"inputData\">�����ַ���</param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static bool IsEmail(string inputData)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Match m = RegEmail.Match(inputData);\n");
                sbContent.Append("			return m.Success;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		#endregion\n");

                sbContent.Append("		#region\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		///\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"sqlInput\"></param>\n");
                sbContent.Append("		/// <param name=\"maxLength\"></param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string SqlText(string sqlInput, int maxLength)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			if(sqlInput != null && sqlInput != string.Empty)\n");
                sbContent.Append("			{\n");
                sbContent.Append("				sqlInput = sqlInput.Trim();\n");
                sbContent.Append("				if(sqlInput.Length > maxLength)\n");
                sbContent.Append("					sqlInput = sqlInput.Substring(0, maxLength);\n");
                sbContent.Append("			}\n");
                sbContent.Append("			return sqlInput;\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		///\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"inputData\"></param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static string HtmlEncode(string inputData)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			return HttpUtility.HtmlEncode(inputData);\n");
                sbContent.Append("		}\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		///\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"lbl\"></param>\n");
                sbContent.Append("		/// <param name=\"txtInput\"></param>\n");
                sbContent.Append("		public static void SetLabel(Label lbl, string txtInput)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			lbl.Text = HtmlEncode(txtInput);\n");
                sbContent.Append("		}\n");
                sbContent.Append("		public static void SetLabel(Label lbl, object inputObj)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			SetLabel(lbl, inputObj.ToString());\n");
                sbContent.Append("		}\n");
                sbContent.Append("\n");
                sbContent.Append("		#endregion\n");

                sbContent.Append("	}\n");
                sbContent.Append("}");

                this.textEditorControl1.Text = sbContent.ToString();
            }
            else if (this.radioButton5.Checked)
            {
                StringBuilder sbContent = new StringBuilder();

                sbContent.Append("using System;\n");
                sbContent.Append("using Wuqi.Webdiyer;//����AspNetPager(�������AspNetPager.dll,��Ŀ¼InClude)\n");
                sbContent.Append("\n");
                sbContent.Append("namespace PublicLayer\n");
                sbContent.Append("{\n");
                sbContent.Append("	public class AspNetPagerClass\n");
                sbContent.Append("	{\n");
                sbContent.Append("		#region AspNetPagerMethod\n");
                sbContent.Append("\n");
                sbContent.Append("		#region GetPagerTop\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ȡ��ҳ����ͷ\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"Pager\"></param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static AspNetPager GetPagerTop(AspNetPager Pager)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Pager.PageSize = 10;\n");
                sbContent.Append("			Pager.ShowInputBox = (ShowInputBox)Enum.Parse(typeof(ShowInputBox), \"Always\");\n");
                sbContent.Append("			Pager.SubmitButtonText = \"Go\";\n");
                sbContent.Append("			Pager.TextBeforeInputBox = \"ת����\";\n");
                sbContent.Append("			Pager.TextAfterInputBox = \"ҳ\";\n");
                sbContent.Append("			Pager.ShowCustomInfoSection = (ShowCustomInfoSection)Enum.Parse(typeof(ShowCustomInfoSection), \"Left\");\n");
                sbContent.Append("			return Pager;\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetPagerTop\n");
                sbContent.Append("\n");
                sbContent.Append("		#region GetPagerEnd\n");
                sbContent.Append("\n");
                sbContent.Append("		/// <summary>\n");
                sbContent.Append("		/// ��ȡ��ҳ����β\n");
                sbContent.Append("		/// </summary>\n");
                sbContent.Append("		/// <param name=\"Pager\"></param>\n");
                sbContent.Append("		/// <returns></returns>\n");
                sbContent.Append("		public static AspNetPager GetPagerEnd(AspNetPager Pager)\n");
                sbContent.Append("		{\n");
                sbContent.Append("			Pager.CustomInfoHTML = \"��<font color=#FF0000><b>\" + Pager.RecordCount.ToString() + \"</b></font>����¼ \";\n");
                sbContent.Append("			Pager.CustomInfoHTML += \"��ǰ��<font color=#FF0000><b>\" + Pager.CurrentPageIndex.ToString() + \"</b></font>/<b>\" + Pager.PageCount.ToString() + \"</b>ҳ\";\n");
                sbContent.Append("			return Pager;\n");
                sbContent.Append("		}\n");
                sbContent.Append("		#endregion GetPagerEnd\n");
                sbContent.Append("\n");
                sbContent.Append("		#endregion AspNetPagerMethod\n");
                sbContent.Append("	}\n");
                sbContent.Append("}");

                this.textEditorControl1.Text = sbContent.ToString();
            }
            else if (this.radioButton6.Checked)
            {
                if (Config.CurrentConn == null)
                {
                    new MessageForm("�������ӷ�����").ShowDialog();
                    return;
                }
                else
                {
                    StringBuilder sbContent = new StringBuilder();

                    sbContent.Append("using System;\n");
                    sbContent.Append("using System.Collections.Generic;\n");
                    sbContent.Append("using System.Text;\n");
                    sbContent.Append("\n");
                    sbContent.Append("namespace " + Config.CurrentDataBaseName + ".SQLServerDAL\n");
                    sbContent.Append("{\n");
                    sbContent.Append("	public class Conn\n");
                    sbContent.Append("	{\n");
                    sbContent.Append("		public static readonly string strConn = \"" + Config.CurrentConn + "\";\n");
                    sbContent.Append("	}\n");
                    sbContent.Append("}");

                    this.textEditorControl1.Text = sbContent.ToString();
                }
            }
        }
    }
}
