using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace youyou_CreatDBModelTool.Buttons
{
    public delegate void ButtonClickHandlerB(object sender, EventArgs e);//事件步骤1

    public partial class Buttons_B : UserControl
    {
        public event ButtonClickHandlerB ButtonClick;//事件步骤2

        public Buttons_B()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, EventArgs e)//事件步骤3
        {
            if (ButtonClick != null)
            {
                ButtonClick(this, e);
            }
        }

        private System.Drawing.Bitmap _PicFirst;
        private System.Drawing.Bitmap _PicSecond;

        public System.Drawing.Bitmap PicFirst
        {
            get { return _PicFirst; }
            set
            {
                _PicFirst = value;
                if (_PicFirst != null)
                {
                    this.pictureBox1.Image = _PicFirst;
                    this.pictureBox1.Width = _PicFirst.Size.Width;
                    this.pictureBox1.Height = _PicFirst.Size.Height;
                    this.Width = this.pictureBox1.Width;
                    this.Height = this.pictureBox1.Height;
                }
            }
        }

        public System.Drawing.Bitmap PicSecond
        {
            get { return _PicSecond; }
            set
            {
                _PicSecond = value;
                if (_PicSecond != null)
                {
                    this.pictureBox2.Image = _PicSecond;
                    this.pictureBox2.Width = _PicSecond.Size.Width;
                    this.pictureBox2.Height = _PicSecond.Size.Height;
                }
            }
        }

        private bool _IsChecked;

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value;
            if (_IsChecked == true)
            {
                this.pictureBox1.Visible = true;
                this.pictureBox2.Visible = false;
            }
            else
            {
                this.pictureBox1.Visible = false;
                this.pictureBox2.Visible = true;
            }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //未选中状态
            OnButtonClick(this, e);//事件步骤4
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //选中状态
            OnButtonClick(this, e);//事件步骤4
        }

        private void Buttons_B_Load(object sender, EventArgs e)
        {

        }

        private void Buttons_B_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
