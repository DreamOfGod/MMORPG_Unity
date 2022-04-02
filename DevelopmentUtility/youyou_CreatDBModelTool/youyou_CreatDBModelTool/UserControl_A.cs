using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace youyou_CreatDBModelTool
{
    public partial class UserControl_A : UserControl
    {
        public UserControl_A()
        {
            InitializeComponent();
        }

        private void UserControl_A_Load(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
            this.buttons_B2.IsChecked = true;
            this.userControl_A_B1.Visible = true;
        }

        /// <summary>
        /// 画矩形框
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrame(Graphics g)
        {
            Pen myPen = new Pen(ColorTranslator.FromHtml("#87A6D4"));
            g.DrawRectangle(myPen, 203, 8, this.Width - 207, this.Height - 14);
            myPen.Dispose();
        }

        /// <summary>
        /// 画矩形
        /// </summary>
        private void DrawRec(Graphics g)
        {
            SolidBrush myBrushA = new SolidBrush(ColorTranslator.FromHtml("#F5F8FF"));
            g.FillRectangle(myBrushA, 204, 9, this.Width - 209, 11);
            myBrushA.Dispose();

            SolidBrush myBrushB = new SolidBrush(ColorTranslator.FromHtml("#E1EFFC"));
            g.FillRectangle(myBrushB, 204, 20, this.Width - 209, 22);
            myBrushB.Dispose();

            SolidBrush myBrushC = new SolidBrush(ColorTranslator.FromHtml("#D6E7FB"));
            g.FillRectangle(myBrushC, 204, 44, this.Width - 209, 2);
            myBrushC.Dispose();
        }

        private void DrawLine(Graphics g)
        {
            //蓝色线条
            Pen myPenA = new Pen(ColorTranslator.FromHtml("#87A6D2"));
            g.DrawLine(myPenA, new Point(204, 42), new Point(this.Width-5, 42));
            myPenA.Dispose();

            //白色线条
            Pen myPenB = new Pen(ColorTranslator.FromHtml("#FFFFFB"));
            g.DrawLine(myPenB, new Point(204, 43), new Point(this.Width - 5, 43));
            myPenB.Dispose();

            //蓝色线条
            Pen myPenC = new Pen(ColorTranslator.FromHtml("#87A6D2"));
            g.DrawLine(myPenC, new Point(204, 46), new Point(this.Width - 5, 46));
            myPenC.Dispose();
        }

        private void UserControl_A_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(ColorTranslator.FromHtml("#D7E7F7"));

            this.DrawFrame(g);

            this.DrawRec(g);

            this.DrawLine(g);

            g.Dispose();
        }

        private void UserControl_A_SizeChanged(object sender, EventArgs e)
        {
            this.userControl_A_Left1.Size = new Size(this.userControl_A_Left1.Size.Width, this.Height - 13);
            
            this.userControl_A_B1.Size = new Size(this.Width - 209, this.Height - 53);
            this.userControl_A_C1.Size = new Size(this.Width - 209, this.Height - 53);
            this.userControl_A_D1.Size = new Size(this.Width - 209, this.Height - 53);
            this.userControl_A_E1.Size = new Size(this.Width - 209, this.Height - 53);
            this.userControl_A_G1.Size = new Size(this.Width - 209, this.Height - 53);
            this.userControl_A_H1.Size = new Size(this.Width - 209, this.Height - 53);

            this.Invalidate();
        }

        private void ReSetButtons()
        {
            this.buttons_B2.IsChecked = false;
            this.buttons_B3.IsChecked = false;
            this.buttons_B4.IsChecked = false;
            this.buttons_B7.IsChecked = false;
        }

        private void ReSetControls()
        {
            this.userControl_A_B1.Visible = false;
            this.userControl_A_C1.Visible = false;
            this.userControl_A_D1.Visible = false;
            this.userControl_A_E1.Visible = false;
            this.userControl_A_G1.Visible = false;
            this.userControl_A_H1.Visible = false;
        }

        private void buttons_B1_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
        }

        private void buttons_B2_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
            this.buttons_B2.IsChecked = true;
            this.userControl_A_B1.Visible = true;
        }

        private void buttons_B3_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
            this.buttons_B3.IsChecked = true;
            this.userControl_A_C1.Visible = true;
        }

        private void buttons_B4_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
            this.buttons_B4.IsChecked = true;
            this.userControl_A_D1.Visible = true;
        }

        private void buttons_B5_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
            this.userControl_A_E1.Visible = true;
        }

        private void buttons_B6_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
        }

        private void buttons_B7_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
            this.buttons_B7.IsChecked = true;
            this.userControl_A_G1.Visible = true;
        }

        private void buttons_B8_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.ReSetControls();
            this.userControl_A_H1.Visible = true;
        }
    }
}
