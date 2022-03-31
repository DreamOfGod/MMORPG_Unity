using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace youyou_CreatDBModelTool.ProgressBars
{
    public partial class ProgressBars_A : UserControl
    {
        public ProgressBars_A()
        {
            InitializeComponent();
        }

        private void ProgressBars_A_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(ColorTranslator.FromHtml("#FFFFFF"));

            Pen WhitePen = new Pen(ColorTranslator.FromHtml("#D0E2F8"));

            SolidBrush BlueBrush = new SolidBrush(ColorTranslator.FromHtml("#DBE9FD"));

            g.DrawRectangle(WhitePen, new Rectangle(0, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(1, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(15, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(16, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(30, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(31, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(45, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(46, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(60, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(61, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(75, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(76, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(90, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(91, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(105, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(106, 1, 8, 8));

            g.DrawRectangle(WhitePen, new Rectangle(120, 0, 9, 9));
            g.FillRectangle(BlueBrush, new Rectangle(121 , 1, 8, 8));

            WhitePen.Dispose();
            BlueBrush.Dispose();
            g.Dispose();
        }

        private int x = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            SolidBrush OBrush = new SolidBrush(ColorTranslator.FromHtml("#9FA8C2"));

            SolidBrush BlueBrush = new SolidBrush(ColorTranslator.FromHtml("#DBE9FD"));

            g.FillRectangle(OBrush, new Rectangle(x, 1, 8, 8));

            if (x > 46 && x <=181)
            {
                g.FillRectangle(BlueBrush, new Rectangle(x - 60, 1, 8, 8));
                x += 15;
            }
            else if (x > 181)
            {
                g.Clear(this.BackColor);
                this.Invalidate();

                x = 1;
            }
            else
            {
                x += 15;
            }

            OBrush.Dispose();
            BlueBrush.Dispose();
            g.Dispose();
        }
    }
}
