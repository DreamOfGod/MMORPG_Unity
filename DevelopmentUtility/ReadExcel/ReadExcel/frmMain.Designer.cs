namespace ReadExcel
{
    partial class ExcelToByte
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.zipForge1 = new ComponentAce.Compression.ZipForge.ZipForge();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFileData = new System.Windows.Forms.TextBox();
            this.btnSelectData = new System.Windows.Forms.Button();
            this.openDataFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(155, 173);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(112, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "生成Data文件";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 211);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择文件进行转换";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(6, 173);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(143, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "选择Excel文件";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(6, 20);
            this.txtFilePath.Multiline = true;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFilePath.Size = new System.Drawing.Size(639, 147);
            this.txtFilePath.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Excel表格|*.xls;*.xlsx";
            this.openFileDialog.Multiselect = true;
            // 
            // zipForge1
            // 
            this.zipForge1.Active = false;
            this.zipForge1.BaseDir = "";
            this.zipForge1.CompressionLevel = ComponentAce.Compression.Archiver.CompressionLevel.Fastest;
            this.zipForge1.CompressionMethod = ComponentAce.Compression.Archiver.CompressionMethod.Deflate;
            this.zipForge1.CompressionMode = ((byte)(1));
            this.zipForge1.EncryptionAlgorithm = ComponentAce.Compression.Archiver.EncryptionAlgorithm.None;
            this.zipForge1.ExtractCorruptedFiles = false;
            this.zipForge1.FileName = "";
            this.zipForge1.InMemory = false;
            this.zipForge1.OpenCorruptedArchives = true;
            this.zipForge1.Password = "";
            this.zipForge1.SFXStub = "";
            this.zipForge1.SpanningMode = ComponentAce.Compression.Archiver.SpanningMode.None;
            this.zipForge1.StoreNTFSTimeStamps = false;
            this.zipForge1.TempDir = "";
            this.zipForge1.UnicodeFilenames = true;
            this.zipForge1.Zip64Mode = ComponentAce.Compression.Archiver.Zip64Mode.Auto;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFileData);
            this.groupBox2.Controls.Add(this.btnSelectData);
            this.groupBox2.Location = new System.Drawing.Point(12, 248);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(651, 215);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试读取";
            // 
            // txtFileData
            // 
            this.txtFileData.Location = new System.Drawing.Point(6, 20);
            this.txtFileData.Multiline = true;
            this.txtFileData.Name = "txtFileData";
            this.txtFileData.ReadOnly = true;
            this.txtFileData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFileData.Size = new System.Drawing.Size(639, 147);
            this.txtFileData.TabIndex = 3;
            // 
            // btnSelectData
            // 
            this.btnSelectData.Location = new System.Drawing.Point(6, 186);
            this.btnSelectData.Name = "btnSelectData";
            this.btnSelectData.Size = new System.Drawing.Size(143, 23);
            this.btnSelectData.TabIndex = 3;
            this.btnSelectData.Text = "选择data文件";
            this.btnSelectData.UseVisualStyleBackColor = true;
            this.btnSelectData.Click += new System.EventHandler(this.btnSelectData_Click);
            // 
            // openDataFileDialog
            // 
            this.openDataFileDialog.Filter = "加密文件|*.data";
            // 
            // ExcelToByte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 503);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExcelToByte";
            this.Text = "读取Excel生成Data工具 - 悠游课堂 - 北京边涯";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private ComponentAce.Compression.ZipForge.ZipForge zipForge1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFileData;
        private System.Windows.Forms.Button btnSelectData;
        private System.Windows.Forms.OpenFileDialog openDataFileDialog;
    }
}

