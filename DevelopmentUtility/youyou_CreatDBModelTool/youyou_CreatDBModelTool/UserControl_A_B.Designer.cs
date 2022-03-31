namespace youyou_CreatDBModelTool
{
    partial class UserControl_A_B
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_A_B));
            this.textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControl();
            this.buttons_A1 = new youyou_CreatDBModelTool.Buttons.Buttons_A();
            this.cb_Add = new System.Windows.Forms.CheckBox();
            this.cb_Update = new System.Windows.Forms.CheckBox();
            this.cb_Delete = new System.Windows.Forms.CheckBox();
            this.cb_GetModel = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.Encoding = ((System.Text.Encoding)(resources.GetObject("textEditorControl1.Encoding")));
            this.textEditorControl1.IsIconBarVisible = false;
            this.textEditorControl1.Location = new System.Drawing.Point(0, 36);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.ShowEOLMarkers = true;
            this.textEditorControl1.ShowInvalidLines = false;
            this.textEditorControl1.ShowSpaces = true;
            this.textEditorControl1.ShowTabs = true;
            this.textEditorControl1.ShowVRuler = true;
            this.textEditorControl1.Size = new System.Drawing.Size(689, 519);
            this.textEditorControl1.TabIndex = 1;
            this.textEditorControl1.VRulerRow = 300;
            // 
            // buttons_A1
            // 
            this.buttons_A1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttons_A1.Location = new System.Drawing.Point(617, 7);
            this.buttons_A1.Name = "buttons_A1";
            this.buttons_A1.PicFirst = global::youyou_CreatDBModelTool.Properties.Resources.Button_Create_1;
            this.buttons_A1.PicSecond = global::youyou_CreatDBModelTool.Properties.Resources.Button_Create_2;
            this.buttons_A1.PicThird = global::youyou_CreatDBModelTool.Properties.Resources.Button_Create_3;
            this.buttons_A1.Size = new System.Drawing.Size(59, 22);
            this.buttons_A1.TabIndex = 0;
            this.buttons_A1.ButtonClick += new youyou_CreatDBModelTool.Buttons.ButtonClickHandler(this.buttons_A1_ButtonClick);
            // 
            // cb_Add
            // 
            this.cb_Add.AutoSize = true;
            this.cb_Add.BackColor = System.Drawing.Color.Transparent;
            this.cb_Add.Checked = true;
            this.cb_Add.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Add.Location = new System.Drawing.Point(3, 11);
            this.cb_Add.Name = "cb_Add";
            this.cb_Add.Size = new System.Drawing.Size(42, 16);
            this.cb_Add.TabIndex = 2;
            this.cb_Add.Text = "Add";
            this.cb_Add.UseVisualStyleBackColor = false;
            // 
            // cb_Update
            // 
            this.cb_Update.AutoSize = true;
            this.cb_Update.BackColor = System.Drawing.Color.Transparent;
            this.cb_Update.Checked = true;
            this.cb_Update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Update.Location = new System.Drawing.Point(51, 11);
            this.cb_Update.Name = "cb_Update";
            this.cb_Update.Size = new System.Drawing.Size(60, 16);
            this.cb_Update.TabIndex = 3;
            this.cb_Update.Text = "Update";
            this.cb_Update.UseVisualStyleBackColor = false;
            // 
            // cb_Delete
            // 
            this.cb_Delete.AutoSize = true;
            this.cb_Delete.BackColor = System.Drawing.Color.Transparent;
            this.cb_Delete.Checked = true;
            this.cb_Delete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Delete.Location = new System.Drawing.Point(117, 11);
            this.cb_Delete.Name = "cb_Delete";
            this.cb_Delete.Size = new System.Drawing.Size(60, 16);
            this.cb_Delete.TabIndex = 4;
            this.cb_Delete.Text = "Delete";
            this.cb_Delete.UseVisualStyleBackColor = false;
            // 
            // cb_GetModel
            // 
            this.cb_GetModel.AutoSize = true;
            this.cb_GetModel.BackColor = System.Drawing.Color.Transparent;
            this.cb_GetModel.Checked = true;
            this.cb_GetModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_GetModel.Location = new System.Drawing.Point(183, 11);
            this.cb_GetModel.Name = "cb_GetModel";
            this.cb_GetModel.Size = new System.Drawing.Size(78, 16);
            this.cb_GetModel.TabIndex = 5;
            this.cb_GetModel.Text = "GetEntity";
            this.cb_GetModel.UseVisualStyleBackColor = false;
            // 
            // UserControl_A_B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cb_GetModel);
            this.Controls.Add(this.cb_Delete);
            this.Controls.Add(this.cb_Update);
            this.Controls.Add(this.cb_Add);
            this.Controls.Add(this.textEditorControl1);
            this.Controls.Add(this.buttons_A1);
            this.Name = "UserControl_A_B";
            this.Size = new System.Drawing.Size(689, 555);
            this.SizeChanged += new System.EventHandler(this.UserControl_A_A_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserControl_A_A_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private youyou_CreatDBModelTool.Buttons.Buttons_A buttons_A1;
        private ICSharpCode.TextEditor.TextEditorControl textEditorControl1;
        private System.Windows.Forms.CheckBox cb_Add;
        private System.Windows.Forms.CheckBox cb_Update;
        private System.Windows.Forms.CheckBox cb_Delete;
        private System.Windows.Forms.CheckBox cb_GetModel;
    }
}
