namespace youyou_CreatDBModelTool.AttributeBuild
{
    partial class AttributeBuild
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.buttons_A4 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.buttons_A1 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(48, 57);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(300, 367);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(442, 57);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(300, 367);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // buttons_A4
            // 
            this.buttons_A4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_A4.Location = new System.Drawing.Point(742, 2);
            this.buttons_A4.Name = "buttons_A4";
            this.buttons_A4.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_CloseB_1;
            this.buttons_A4.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_CloseB_2;
            this.buttons_A4.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_CloseB_3;
            this.buttons_A4.Size = new System.Drawing.Size(43, 16);
            this.buttons_A4.TabIndex = 3;
            this.buttons_A4.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A4_ButtonClick);
            // 
            // buttons_A1
            // 
            this.buttons_A1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_A1.Location = new System.Drawing.Point(362, 443);
            this.buttons_A1.Name = "buttons_A1";
            this.buttons_A1.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_Create_1;
            this.buttons_A1.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_Create_2;
            this.buttons_A1.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_Create_3;
            this.buttons_A1.Size = new System.Drawing.Size(59, 22);
            this.buttons_A1.TabIndex = 2;
            this.buttons_A1.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A1_ButtonClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::youyou_CreatDBModelTool.Properties.Resources.Logo_1616;
            this.pictureBox2.Location = new System.Drawing.Point(7, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // AttributeBuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttons_A4);
            this.Controls.Add(this.buttons_A1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AttributeBuild";
            this.ShowInTaskbar = false;
            this.Text = "AttributeBuild";
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AttributeBuild_Paint);
            this.Load += new System.EventHandler(this.AttributeBuild_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A1;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A4;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}