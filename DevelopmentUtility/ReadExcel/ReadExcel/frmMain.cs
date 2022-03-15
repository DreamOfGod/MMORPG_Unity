using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadExcel
{
    public partial class ExcelToByte : Form
    {
        //异或因子
        private byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子

        public ExcelToByte()
        {
            InitializeComponent();
        }

        #region btnSelect_Click 选择表格按钮点击事件
        /// <summary>
        /// 选择表格按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFilePath.Text = "";
                foreach (string strName in this.openFileDialog.FileNames)
                {
                    this.txtFilePath.Text += strName + "\r\n";
                }
            }
        }
        #endregion

        #region btnCreate_Click 创建按钮点击事件
        /// <summary>
        /// 创建按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            //try
            //{
            string[] arr = this.txtFilePath.Text.Trim().Split('\r', '\n');
            if (arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    ReadData(arr[i]);
                }
            }

            MessageBox.Show("创建成功");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("创建失败" + ex.Message);
            //}
        }
        #endregion

        #region ReadData 读取数据
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path"></param>
        private void ReadData(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            string tableName = GetFirstSheetNameFromExcelFileName(path, 1);

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";

            DataTable dt = null;

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                strExcel = string.Format("select * from [{0}$]", tableName);
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds, "table1");
                dt = ds.Tables[0];
                myCommand.Dispose();
            }

            CreateData(path, dt);
        }
        #endregion

        #region CreateData 生成加密后的文件
        /// <summary>
        /// 生成加密后的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        private void CreateData(string path, DataTable dt)
        {
            //数据格式 行数 列数 二维数组每项的值 这里不做判断 都用string存储

            string filePath = path.Substring(0, path.LastIndexOf('\\') + 1);
            string fileFullName = path.Substring(path.LastIndexOf('\\') + 1);
            string fileName = fileFullName.Substring(0, fileFullName.LastIndexOf('.'));

            byte[] buffer = null;
            string[,] dataArr = null;

            using (MMO_MemoryStream ms = new MMO_MemoryStream())
            {
                int row = dt.Rows.Count;
                int columns = dt.Columns.Count;

                dataArr = new string[columns, 3];

                ms.WriteInt(row);
                ms.WriteInt(columns);
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (i < 3)
                        {
                            dataArr[j, i] = dt.Rows[i][j].ToString().Trim();
                        }

                        ms.WriteUTF8String(dt.Rows[i][j].ToString().Trim());
                    }
                }
                buffer = ms.ToArray();
            }

            //------------------
            //第1步：xor加密
            //------------------
            int iScaleLen = xorScale.Length;
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(buffer[i] ^ xorScale[i % iScaleLen]);
            }

            //------------------
            //第2步：压缩
            //------------------
            //压缩后的字节流
            buffer = ZlibHelper.CompressBytes(buffer);

            //------------------
            //第3步：写入文件
            //------------------
            FileStream fs = new FileStream(string.Format("{0}{1}", filePath, fileName + ".data"), FileMode.Create);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();

            CreateEntity(filePath, fileName, dataArr);
            CreateDBModel(filePath, fileName, dataArr);
        }
        #endregion

        /// <summary>
        /// 创建实体
        /// </summary>
        private void CreateEntity(string filePath, string fileName, string[,] dataArr)
        {
            if (dataArr == null) return;

            if (!Directory.Exists(string.Format("{0}Create", filePath)))
            {
                Directory.CreateDirectory(string.Format("{0}Create", filePath));
            }

            if (!Directory.Exists(string.Format("{0}CreateLua", filePath)))
            {
                Directory.CreateDirectory(string.Format("{0}CreateLua", filePath));
            }

            StringBuilder sbr = new StringBuilder();
            sbr.Append("\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("//作    者：\r\n");
            sbr.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbr.Append("//备    注：此代码为工具生成 请勿手工修改\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("using System.Collections;\r\n");
            sbr.Append("\r\n");
            sbr.Append("/// <summary>\r\n");
            sbr.AppendFormat("/// {0}实体\r\n", fileName);
            sbr.Append("/// </summary>\r\n");
            sbr.AppendFormat("public partial class {0}Entity : AbstractEntity\r\n", fileName);
            sbr.Append("{\r\n");

            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                if (i == 0) continue;
                sbr.Append("    /// <summary>\r\n");
                sbr.AppendFormat("    /// {0}\r\n", dataArr[i, 2]);
                sbr.Append("    /// </summary>\r\n");
                sbr.AppendFormat("    public {0} {1} {{ get; set; }}\r\n", dataArr[i, 1], dataArr[i, 0]);
                sbr.Append("\r\n");
            }

            sbr.Append("}\r\n");


            using (FileStream fs = new FileStream(string.Format("{0}Create/{1}Entity.cs", filePath, fileName), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }

            //=======================创建Lua的实体
            sbr.Clear();

            sbr.AppendFormat("{0}Entity = {{ ", fileName);

            for (int i = 0; i < dataArr.GetLength(0); i++)
            {

                if (i == dataArr.GetLength(0) - 1)
                {
                    if (dataArr[i, 1].Equals("string", StringComparison.CurrentCultureIgnoreCase))
                    {
                        sbr.AppendFormat("{0} = \"\"", dataArr[i, 0]);
                    }
                    else
                    {
                        sbr.AppendFormat("{0} = 0", dataArr[i, 0]);
                    }
                }
                else
                {
                    if (dataArr[i, 1].Equals("string", StringComparison.CurrentCultureIgnoreCase))
                    {
                        sbr.AppendFormat("{0} = \"\", ", dataArr[i, 0]);
                    }
                    else
                    {
                        sbr.AppendFormat("{0} = 0, ", dataArr[i, 0]);
                    }
                }
            }
            sbr.Append(" }\r\n");

            sbr.Append("\r\n");
            sbr.Append("--这句是重定义元表的索引，就是说有了这句，这个才是一个类\r\n");
            sbr.AppendFormat("{0}Entity.__index = {0}Entity;\r\n", fileName);
            sbr.Append("\r\n");
            sbr.AppendFormat("function {0}Entity.New(", fileName);
            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                if (i == dataArr.GetLength(0) - 1)
                {
                    sbr.AppendFormat("{0}", dataArr[i, 0]);
                }
                else
                {
                    sbr.AppendFormat("{0}, ", dataArr[i, 0]);
                }
            }
            sbr.Append(")\r\n");
            sbr.Append("    local self = { }; --初始化self\r\n");
            sbr.Append("");
            sbr.AppendFormat("    setmetatable(self, {0}Entity); --将self的元表设定为Class\r\n", fileName);
            sbr.Append("\r\n");
            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                sbr.AppendFormat("    self.{0} = {0};\r\n", dataArr[i, 0]);
            }
            sbr.Append("\r\n");
            sbr.Append("    return self;\r\n");
            sbr.Append("end");

            using (FileStream fs = new FileStream(string.Format("{0}CreateLua/{1}Entity.lua", filePath, fileName), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }
        }

        /// <summary>
        /// 创建数据管理类
        /// </summary>
        private void CreateDBModel(string filePath, string fileName, string[,] dataArr)
        {
            if (dataArr == null) return;

            if (!Directory.Exists(string.Format("{0}Create", filePath)))
            {
                Directory.CreateDirectory(string.Format("{0}Create", filePath));
            }

            StringBuilder sbr = new StringBuilder();
            sbr.Append("\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("//作    者：\r\n");
            sbr.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbr.Append("//备    注：此代码为工具生成 请勿手工修改\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("using System.Collections;\r\n");
            sbr.Append("using System.Collections.Generic;\r\n");
            sbr.Append("using System;\r\n");
            sbr.Append("\r\n");
            sbr.Append("/// <summary>\r\n");
            sbr.AppendFormat("/// {0}数据管理\r\n", fileName);
            sbr.Append("/// </summary>\r\n");
            sbr.AppendFormat("public partial class {0}DBModel : AbstractDBModel<{0}DBModel, {0}Entity>\r\n", fileName);
            sbr.Append("{\r\n");
            sbr.Append("    /// <summary>\r\n");
            sbr.Append("    /// 文件名称\r\n");
            sbr.Append("    /// </summary>\r\n");
            sbr.AppendFormat("    protected override string FileName {{ get {{ return \"{0}.data\"; }} }}\r\n", fileName);
            sbr.Append("\r\n");
            sbr.Append("    /// <summary>\r\n");
            sbr.Append("    /// 创建实体\r\n");
            sbr.Append("    /// </summary>\r\n");
            sbr.Append("    /// <param name=\"parse\"></param>\r\n");
            sbr.Append("    /// <returns></returns>\r\n");
            sbr.AppendFormat("    protected override {0}Entity MakeEntity(GameDataTableParser parse)\r\n", fileName);
            sbr.Append("    {\r\n");
            sbr.AppendFormat("        {0}Entity entity = new {0}Entity();\r\n", fileName);

            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                sbr.AppendFormat("        entity.{0} = parse.GetFieldValue(\"{0}\"){1};\r\n", dataArr[i, 0], ChangeTypeName(dataArr[i, 1]));
            }
            sbr.Append("        return entity;\r\n");
            sbr.Append("    }\r\n");
            sbr.Append("}\r\n");

            using (FileStream fs = new FileStream(string.Format("{0}Create/{1}DBModel.cs", filePath, fileName), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }

            //===============生成lua的DBModel
            sbr.Clear();
            sbr.Append("");

            sbr.AppendFormat("require \"Download/XLuaLogic/Data/Create/{0}Entity\"\r\n", fileName);
            sbr.Append("\r\n");
            sbr.Append("--数据访问\r\n");
            sbr.AppendFormat("{0}DBModel = {{ }}\r\n", fileName);
            sbr.Append("\r\n");
            sbr.AppendFormat("local this = {0}DBModel;\r\n", fileName);
            sbr.Append("\r\n");
            sbr.AppendFormat("local {0}Table = {{ }}; --定义表格\r\n", fileName.ToLower());
            sbr.Append("\r\n");
            sbr.AppendFormat("function {0}DBModel.New()\r\n", fileName);
            sbr.Append("    return this;\r\n");
            sbr.Append("end\r\n");
            sbr.Append("\r\n");
            sbr.AppendFormat("function {0}DBModel.Init()\r\n", fileName);
            sbr.Append("\r\n");
            sbr.Append("    --这里从C#代码中获取一个数组\r\n");
            sbr.Append("\r\n");
            sbr.AppendFormat("    local gameDataTable = CS.LuaHelper.Instance:GetData(\"{0}.data\");\r\n", fileName);
            sbr.Append("");
            sbr.Append("    --表格的前三行是表头 所以获取数据时候 要从 3 开始\r\n");
            sbr.Append("    --print(\"行数\"..gameDataTable.Row);\r\n");
            sbr.Append("    --print(\"列数\"..gameDataTable.Column);\r\n");
            sbr.Append("\r\n");
            sbr.Append("    for i = 3, gameDataTable.Row - 1, 1 do\r\n");
            sbr.AppendFormat("        {0}Table[#{0}Table+1] = {1}Entity.New( ", fileName.ToLower(), fileName);

            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                if (i == dataArr.GetLength(0) - 1)
                {
                    if (dataArr[i, 1].Equals("string", StringComparison.CurrentCultureIgnoreCase))
                    {
                        sbr.AppendFormat("gameDataTable.Data[i][{0}]", i);
                    }
                    else
                    {
                        sbr.AppendFormat("tonumber(gameDataTable.Data[i][{0}])", i);
                    }
                }
                else
                {
                    if (dataArr[i, 1].Equals("string", StringComparison.CurrentCultureIgnoreCase))
                    {
                        sbr.AppendFormat("gameDataTable.Data[i][{0}], ", i);
                    }
                    else
                    {
                        sbr.AppendFormat("tonumber(gameDataTable.Data[i][{0}]), ", i);
                    }
                }
            }
            sbr.Append(" );\r\n");
            sbr.Append("    end\r\n");
            sbr.Append("\r\n");
            sbr.Append("end\r\n");
            sbr.Append("\r\n");
            sbr.AppendFormat("function {0}DBModel.GetList()\r\n", fileName);
            sbr.AppendFormat("    return {0}Table;\r\n", fileName.ToLower());
            sbr.Append("end");
            sbr.Append("\r\n");
            sbr.Append("\r\n");
            sbr.AppendFormat("function {0}DBModel.GetEntity(id)\r\n", fileName);
            sbr.AppendFormat("    local ret = nil;\r\n");
            sbr.AppendFormat("    for i = 1, #{0}Table, 1 do\r\n", fileName.ToLower());
            sbr.AppendFormat("        if ({0}Table[i].Id == id) then\r\n", fileName.ToLower());
            sbr.AppendFormat("            ret = {0}Table[i];\r\n", fileName.ToLower());
            sbr.AppendFormat("            break;\r\n");
            sbr.AppendFormat("        end\r\n");
            sbr.AppendFormat("    end\r\n");
            sbr.AppendFormat("    return ret;\r\n");
            sbr.AppendFormat("end");

            using (FileStream fs = new FileStream(string.Format("{0}CreateLua/{1}DBModel.lua", filePath, fileName), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }
        }

        private string ChangeTypeName(string type)
        {
            string str = string.Empty;

            switch (type)
            {
                case "int":
                    str = ".ToInt()";
                    break;
                case "long":
                    str = ".ToLong()";
                    break;
                case "float":
                    str = ".ToFloat()";
                    break;
            }

            return str;
        }

        #region GetFirstSheetNameFromExcelFileName 获取表格的第一个数据表名称
        /// <summary>
        /// 获取表格的第一个数据表名称
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="numberSheetID"></param>
        /// <returns></returns>
        public string GetFirstSheetNameFromExcelFileName(string filepath, int numberSheetID)
        {
            if (!System.IO.File.Exists(filepath))
            {
                return null;
            }
            if (numberSheetID <= 1) { numberSheetID = 1; }
            try
            {
                Microsoft.Office.Interop.Excel.Application obj = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbook objWB = default(Microsoft.Office.Interop.Excel.Workbook);
                string strFirstSheetName = null;

                obj = (Microsoft.Office.Interop.Excel.Application)Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application", string.Empty);
                objWB = obj.Workbooks.Open(filepath, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                strFirstSheetName = ((Microsoft.Office.Interop.Excel.Worksheet)objWB.Worksheets[1]).Name;

                objWB.Close(Type.Missing, Type.Missing, Type.Missing);
                objWB = null;
                obj.Quit();
                obj = null;
                return strFirstSheetName;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void btnSelectData_Click(object sender, EventArgs e)
        {
            if (this.openDataFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFileData.Text = "";
                string strPath = this.openDataFileDialog.FileName;

                using (GameDataTableParser parse = new GameDataTableParser(strPath))
                {
                    while (!parse.Eof)
                    {
                        StringBuilder sbr = new StringBuilder();
                        for (int i = 0; i < parse.FieldName.Length; i++)
                        {
                            sbr.AppendFormat("{0} ", parse.GetFieldValue(parse.FieldName[i]));
                        }
                        this.txtFileData.Text += sbr.ToString() + "\r\n";
                        parse.Next();
                    }
                }
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}