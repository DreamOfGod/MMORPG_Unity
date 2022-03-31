namespace youyou_CreatDBModelTool
{
    partial class UserControl_A_Left
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_A_Left));
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.txt_UID = new System.Windows.Forms.TextBox();
            this.txt_PWD = new System.Windows.Forms.TextBox();
            this.comBox_DataBase = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.progressBars_A1 = new youyou_CreatDBModelTool.ProgressBars.ProgressBars_A();
            this.buttons_A2 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.buttons_A1 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.buttons_B2 = new youyou_CreatDBModelTool.Buttons.Buttons_B();
            this.buttons_B1 = new youyou_CreatDBModelTool.Buttons.Buttons_B();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton1.Location = new System.Drawing.Point(44, 129);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(89, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "Windows认证";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(44, 161);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(101, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "SqlServer认证";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(16, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "服务器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(16, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "帐　号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(16, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "密　码";
            // 
            // txt_IP
            // 
            this.txt_IP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_IP.Location = new System.Drawing.Point(63, 202);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(100, 14);
            this.txt_IP.TabIndex = 8;
            // 
            // txt_UID
            // 
            this.txt_UID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_UID.Location = new System.Drawing.Point(63, 229);
            this.txt_UID.Name = "txt_UID";
            this.txt_UID.Size = new System.Drawing.Size(100, 14);
            this.txt_UID.TabIndex = 9;
            // 
            // txt_PWD
            // 
            this.txt_PWD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_PWD.Location = new System.Drawing.Point(63, 256);
            this.txt_PWD.Name = "txt_PWD";
            this.txt_PWD.PasswordChar = '*';
            this.txt_PWD.Size = new System.Drawing.Size(100, 14);
            this.txt_PWD.TabIndex = 10;
            // 
            // comBox_DataBase
            // 
            this.comBox_DataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBox_DataBase.FormattingEnabled = true;
            this.comBox_DataBase.Location = new System.Drawing.Point(42, 358);
            this.comBox_DataBase.Name = "comBox_DataBase";
            this.comBox_DataBase.Size = new System.Drawing.Size(121, 20);
            this.comBox_DataBase.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::youyou_CreatDBModelTool.Properties.Resources.A_Left_Pic;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(188, 46);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 99);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(184, 493);
            this.treeView1.TabIndex = 15;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "A_Left_DataBase.png");
            this.imageList1.Images.SetKeyName(1, "A_Left_Table.png");
            // 
            // progressBars_A1
            // 
            this.progressBars_A1.Location = new System.Drawing.Point(33, 333);
            this.progressBars_A1.Name = "progressBars_A1";
            this.progressBars_A1.Size = new System.Drawing.Size(130, 10);
            this.progressBars_A1.TabIndex = 14;
            this.progressBars_A1.Visible = false;
            // 
            // buttons_A2
            // 
            this.buttons_A2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_A2.Location = new System.Drawing.Point(63, 397);
            this.buttons_A2.Name = "buttons_A2";
            this.buttons_A2.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_D_1;
            this.buttons_A2.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_D_2;
            this.buttons_A2.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_D_3;
            this.buttons_A2.Size = new System.Drawing.Size(78, 21);
            this.buttons_A2.TabIndex = 13;
            this.buttons_A2.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A2_ButtonClick);
            // 
            // buttons_A1
            // 
            this.buttons_A1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_A1.Location = new System.Drawing.Point(63, 295);
            this.buttons_A1.Name = "buttons_A1";
            this.buttons_A1.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_C_1;
            this.buttons_A1.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_C_2;
            this.buttons_A1.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_C_3;
            this.buttons_A1.Size = new System.Drawing.Size(78, 21);
            this.buttons_A1.TabIndex = 12;
            this.buttons_A1.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A1_ButtonClick);
            // 
            // buttons_B2
            // 
            this.buttons_B2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_B2.IsChecked = false;
            this.buttons_B2.Location = new System.Drawing.Point(96, 47);
            this.buttons_B2.Name = "buttons_B2";
            this.buttons_B2.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_B_1;
            this.buttons_B2.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_B_2;
            this.buttons_B2.Size = new System.Drawing.Size(93, 25);
            this.buttons_B2.TabIndex = 2;
            this.buttons_B2.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandlerB(this.buttons_B2_ButtonClick);
            // 
            // buttons_B1
            // 
            this.buttons_B1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_B1.IsChecked = false;
            this.buttons_B1.Location = new System.Drawing.Point(1, 47);
            this.buttons_B1.Name = "buttons_B1";
            this.buttons_B1.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_A_1;
            this.buttons_B1.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_A_Left_A_2;
            this.buttons_B1.Size = new System.Drawing.Size(95, 25);
            this.buttons_B1.TabIndex = 1;
            this.buttons_B1.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandlerB(this.buttons_B1_ButtonClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(13, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 16;
            this.label4.Visible = false;
            // 
            // UserControl_A_Left
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.progressBars_A1);
            this.Controls.Add(this.buttons_A2);
            this.Controls.Add(this.buttons_A1);
            this.Controls.Add(this.comBox_DataBase);
            this.Controls.Add(this.txt_PWD);
            this.Controls.Add(this.txt_UID);
            this.Controls.Add(this.txt_IP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.buttons_B2);
            this.Controls.Add(this.buttons_B1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UserControl_A_Left";
            this.Size = new System.Drawing.Size(190, 595);
            this.Load += new System.EventHandler(this.UserControl_A_Left_Load);
            this.SizeChanged += new System.EventHandler(this.UserControl_A_Left_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserControl_A_Left_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private youyou_CreatDBModelTool.Buttons.Buttons_B buttons_B1;
        private youyou_CreatDBModelTool.Buttons.Buttons_B buttons_B2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.TextBox txt_UID;
        private System.Windows.Forms.TextBox txt_PWD;
        private System.Windows.Forms.ComboBox comBox_DataBase;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A1;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A2;
        private youyou_CreatDBModelTool.ProgressBars.ProgressBars_A progressBars_A1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label4;
    }
}
