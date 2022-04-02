namespace youyou_CreatDBModelTool
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.userControl_A1 = new youyou_CreatDBModelTool.UserControl_A();
            this.buttons_A1 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.buttons_A4 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.buttons_A3 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.buttons_A2 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::youyou_CreatDBModelTool.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(13, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 54);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel1.Location = new System.Drawing.Point(12, 682);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 12);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.u3dol.com";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Maroon;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // userControl_A1
            // 
            this.userControl_A1.BackColor = System.Drawing.Color.White;
            this.userControl_A1.Location = new System.Drawing.Point(1, 69);
            this.userControl_A1.Name = "userControl_A1";
            this.userControl_A1.Size = new System.Drawing.Size(898, 608);
            this.userControl_A1.TabIndex = 5;
            // 
            // buttons_A1
            // 
            this.buttons_A1.Location = new System.Drawing.Point(823, 2);
            this.buttons_A1.Name = "buttons_A1";
            this.buttons_A1.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_Normal_1;
            this.buttons_A1.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_Normal_2;
            this.buttons_A1.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_Normal_3;
            this.buttons_A1.Size = new System.Drawing.Size(26, 16);
            this.buttons_A1.TabIndex = 3;
            this.buttons_A1.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A1_ButtonClick);
            // 
            // buttons_A4
            // 
            this.buttons_A4.Location = new System.Drawing.Point(849, 2);
            this.buttons_A4.Name = "buttons_A4";
            this.buttons_A4.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_Close_1;
            this.buttons_A4.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_Close_2;
            this.buttons_A4.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_Close_3;
            this.buttons_A4.Size = new System.Drawing.Size(42, 16);
            this.buttons_A4.TabIndex = 2;
            this.buttons_A4.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A4_ButtonClick);
            // 
            // buttons_A3
            // 
            this.buttons_A3.Location = new System.Drawing.Point(823, 2);
            this.buttons_A3.Name = "buttons_A3";
            this.buttons_A3.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_Max_1;
            this.buttons_A3.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_Max_2;
            this.buttons_A3.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_Max_3;
            this.buttons_A3.Size = new System.Drawing.Size(26, 16);
            this.buttons_A3.TabIndex = 1;
            this.buttons_A3.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A3_ButtonClick);
            // 
            // buttons_A2
            // 
            this.buttons_A2.Location = new System.Drawing.Point(798, 2);
            this.buttons_A2.Name = "buttons_A2";
            this.buttons_A2.PicFirst = ((System.Drawing.Bitmap)(resources.GetObject("buttons_A2.PicFirst")));
            this.buttons_A2.PicSecond = ((System.Drawing.Bitmap)(resources.GetObject("buttons_A2.PicSecond")));
            this.buttons_A2.PicThird = ((System.Drawing.Bitmap)(resources.GetObject("buttons_A2.PicThird")));
            this.buttons_A2.Size = new System.Drawing.Size(25, 16);
            this.buttons_A2.TabIndex = 0;
            this.buttons_A2.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A2_ButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.userControl_A1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttons_A1);
            this.Controls.Add(this.buttons_A4);
            this.Controls.Add(this.buttons_A3);
            this.Controls.Add(this.buttons_A2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "悠游课堂代码生成器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A2;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A3;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A4;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private UserControl_A userControl_A1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

