namespace youyou_CreatDBModelTool
{
    partial class MessageForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttons_A1 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("ËÎÌו", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(186, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::youyou_CreatDBModelTool.Properties.Resources.Logo_1616;
            this.pictureBox2.Location = new System.Drawing.Point(7, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::youyou_CreatDBModelTool.Properties.Resources.MessageForm;
            this.pictureBox1.Location = new System.Drawing.Point(1, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 130);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttons_A1
            // 
            this.buttons_A1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_A1.Location = new System.Drawing.Point(188, 116);
            this.buttons_A1.Name = "buttons_A1";
            this.buttons_A1.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_MessageForm_1;
            this.buttons_A1.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_MessageForm_2;
            this.buttons_A1.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_MessageForm_3;
            this.buttons_A1.Size = new System.Drawing.Size(78, 21);
            this.buttons_A1.TabIndex = 15;
            this.buttons_A1.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A1_ButtonClick);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 177);
            this.Controls.Add(this.buttons_A1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MessageForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A1;
    }
}