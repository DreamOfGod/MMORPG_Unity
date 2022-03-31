using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace youyou_CreatDBModelTool.Buttons
{
    public delegate void ButtonClickHandler(object sender, EventArgs e);//事件步骤1

    public partial class Buttons_A : UserControl
    {
        public event ButtonClickHandler ButtonClick;//事件步骤2

        public Buttons_A()
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



        private void pictureBox1_MouseEnter(object sender, System.EventArgs e)
        {
            this.pictureBox1.Visible = false;
        }

        private void pictureBox2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.pictureBox3.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, System.EventArgs e)
        {
            this.pictureBox1.Visible = true;
            this.pictureBox3.Visible = false;
        }

        private void pictureBox2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
            if (this.PicSecond == null)
            {
                OnButtonClick(this, e);//事件步骤4
                return;
            }
            else
            {
                this.pictureBox3.Visible = false;
                if (e.X >= 0 && e.X <= _PicFirst.Size.Width)
                {
                    if (e.Y >= 0 && e.Y <= _PicFirst.Size.Height)
                    {
                        OnButtonClick(this, e);//事件步骤4
                    }
                }
            }
        }

        private System.Drawing.Bitmap _PicFirst;
        private System.Drawing.Bitmap _PicSecond;
        private System.Drawing.Bitmap _PicThird;

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

        public System.Drawing.Bitmap PicThird
        {
            get { return _PicThird; }
            set
            {
                _PicThird = value;
                if (_PicThird != null)
                {
                    this.pictureBox3.Image = _PicThird;
                    this.pictureBox3.Width = _PicThird.Size.Width;
                    this.pictureBox3.Height = _PicThird.Size.Height;
                }
            }
        }
    }
}
