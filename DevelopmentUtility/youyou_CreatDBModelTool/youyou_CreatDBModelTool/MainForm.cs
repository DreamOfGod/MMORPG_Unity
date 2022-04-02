using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace youyou_CreatDBModelTool
{
    public partial class MainForm : Form
    {
        private const int HTCAPTION = 0x0002;
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3; 

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 外围裁减
        /// </summary>
        private void DrawRegion()
        {
            GraphicsPath shape = new GraphicsPath();

            Point[] p = new Point[]
            {
                new Point(4, 0),

                new Point(this.Width-4, 0),
                new Point(this.Width-3, 1),
                new Point(this.Width-2, 2),
                new Point(this.Width-1, 3),
                new Point(this.Width-0, 4),

                new Point(this.Width-0, this.Height-4),
                new Point(this.Width-1, this.Height-3),
                new Point(this.Width-2, this.Height-2),
                new Point(this.Width-3, this.Height-1),
                new Point(this.Width-4, this.Height-0),

                new Point(4, this.Height-0),
                new Point(3, this.Height-1),
                new Point(2, this.Height-2),
                new Point(1, this.Height-3),
                new Point(0, this.Height-4),

                new Point(0, 4),
                new Point(1, 3),
                new Point(2, 2),
                new Point(3, 1),

                new Point(4, 0)
            };
            shape.AddPolygon(p);

            //将窗体的显示区域设为GraphicsPath的实例   
            this.Region = new System.Drawing.Region(shape);

            shape.Dispose();
        }

        /// <summary>
        /// 绘制头
        /// </summary>
        /// <param name="g"></param>
        private void DrawHeader(Graphics g)
        {
            //渐变矩形A
            LinearGradientBrush myBrushA = new LinearGradientBrush(new Point(1, 1), new Point(1, 66), ColorTranslator.FromHtml("#6894C6"), ColorTranslator.FromHtml("#CFDFF3"));
            g.FillRectangle(myBrushA, 1, 1, this.Width-2, 65);
            myBrushA.Dispose();

            //渐变矩形B
            LinearGradientBrush myBrushB = new LinearGradientBrush(new Point(4, 1), new Point(24, 1), ColorTranslator.FromHtml("#6894C6"), ColorTranslator.FromHtml("#D8E6F7"));
            g.FillRectangle(myBrushB, 4, 1, 20, 1);
            myBrushB.Dispose();

            //渐变矩形C
            LinearGradientBrush myBrushC = new LinearGradientBrush(new Point(this.Width - 5, 1), new Point(this.Width - 25, 1), ColorTranslator.FromHtml("#6894C6"), ColorTranslator.FromHtml("#D8E6F7"));
            g.FillRectangle(myBrushC, this.Width - 25, 1, 20, 1);
            myBrushC.Dispose();

            //线A
            Pen myPenA = new Pen(ColorTranslator.FromHtml("#D8E6F7"));
            g.DrawLine(myPenA, new Point(24, 1), new Point(this.Width - 25, 1));
            myPenA.Dispose();

            //线B
            Pen myPenB = new Pen(ColorTranslator.FromHtml("#7692B9"));
            g.DrawLine(myPenB, new Point(1, 66), new Point(this.Width - 2, 66));
            myPenB.Dispose();

            //线C
            Pen myPenC = new Pen(ColorTranslator.FromHtml("#B1C8E8"));
            g.DrawLine(myPenC, new Point(1, 67), new Point(this.Width - 2, 67));
            myPenC.Dispose();

            //线D
            Pen myPenD = new Pen(ColorTranslator.FromHtml("#C9DBF3"));
            g.DrawLine(myPenD, new Point(1, 68), new Point(this.Width - 2, 68));
            myPenD.Dispose();
        }

        /// <summary>
        /// 绘制中间
        /// </summary>
        /// <param name="g"></param>
        private void DrawMiddel(Graphics g)
        {
            //背景
            SolidBrush myBrushA = new SolidBrush(ColorTranslator.FromHtml("#D7E5F6"));
            g.FillRectangle(myBrushA, 1, 69, this.Width - 2, this.Height - 92);
            myBrushA.Dispose();
        }

        /// <summary>
        /// 绘制底部
        /// </summary>
        /// <param name="g"></param>
        private void DrawBottom(Graphics g)
        {
            //底部蓝线
            Pen myPenA = new Pen(ColorTranslator.FromHtml("#6687B9"));
            g.DrawLine(myPenA, new Point(0, this.Height - 23), new Point(this.Width - 1, this.Height - 23));
            myPenA.Dispose();

            //底部白线
            Pen myPenB = new Pen(ColorTranslator.FromHtml("#8FA6CC"));
            g.DrawLine(myPenB, new Point(0, this.Height - 22), new Point(this.Width - 1, this.Height - 22));
            myPenB.Dispose();

            int nPoint = (int)(this.Width * 0.7);

            //填充最下方渐变
            LinearGradientBrush myBrushA = new LinearGradientBrush(new Point(0, this.Height - 21), new Point(nPoint, this.Height - 21), ColorTranslator.FromHtml("#5D7DAC"), ColorTranslator.FromHtml("#7699D0"));
            g.FillRectangle(myBrushA, 0, this.Height - 21, nPoint, 20);
            myBrushA.Dispose();

            Rectangle myRect2 = new Rectangle(nPoint - 1, this.Height - 21, this.Width - nPoint, 20);
            LinearGradientBrush myBrushB = new LinearGradientBrush(myRect2, ColorTranslator.FromHtml("#7699D0"), ColorTranslator.FromHtml("#5D7DAC"), LinearGradientMode.Horizontal);
            g.FillRectangle(myBrushB, nPoint, this.Height - 21, this.Width - nPoint, 20);
            myBrushB.Dispose();
        }

        /// <summary>
        /// 绘制窗体边框
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrame(Graphics g)
        {
            GraphicsPath shape = new GraphicsPath();

            Point[] p = new Point[]
            {
                new Point(4, 0),

                new Point(this.Width-5, 0),
                new Point(this.Width-4, 1),
                new Point(this.Width-3, 2),
                new Point(this.Width-2, 3),
                new Point(this.Width-1, 4),

                new Point(this.Width-1, this.Height-5),
                new Point(this.Width-2, this.Height-4),
                new Point(this.Width-3, this.Height-3),
                new Point(this.Width-4, this.Height-2),
                new Point(this.Width-5, this.Height-1),

                new Point(4, this.Height-1),
                new Point(3, this.Height-2),
                new Point(2, this.Height-3),
                new Point(1, this.Height-4),
                new Point(0, this.Height-5),

                new Point(0, 4),
                new Point(1, 3),
                new Point(2, 2),
                new Point(3, 1),

                new Point(4, 0)
            };
            shape.AddPolygon(p);
            g.DrawPath(new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(88)))), ((int)(((byte)(112))))), 1), shape);

            shape.Dispose();
        }

        /// <summary>
        /// 绘制版本信息
        /// </summary>
        /// <param name="g"></param>
        private void DrawVersion()
        {
            Graphics g = this.CreateGraphics();

            g.SmoothingMode = SmoothingMode.AntiAlias;

            SolidBrush myBrushB = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));
            g.DrawString(Config.strEdition, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))), myBrushB, new PointF(this.Width - 560, this.Height - 17));
            myBrushB.Dispose();

            g.Dispose();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            this.DrawRegion();

            this.DrawHeader(g);

            this.DrawMiddel(g);

            this.DrawBottom(g);

            this.DrawFrame(g);

            new System.Threading.Thread(DrawVersion).Start();

            g.Dispose();
        }

        private void buttons_A2_ButtonClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttons_A3_ButtonClick(object sender, EventArgs e)
        {
            //最大化
            this.WindowState = FormWindowState.Maximized;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Invalidate();
        }

        private void buttons_A4_ButtonClick(object sender, EventArgs e)
        {
            //关闭
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            this.ReSetControls();
            this.userControl_A1.Visible = true;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.buttons_A2.Location = new Point(this.Width - 102, 2);

            this.buttons_A3.Location = new Point(this.Width - 77, 2);
            this.buttons_A1.Location = new Point(this.Width - 77, 2);

            this.buttons_A4.Location = new Point(this.Width - 51, 2);

            if (this.WindowState == FormWindowState.Normal)
            {
                this.buttons_A3.Visible = true;
                this.buttons_A1.Visible = false;
            }
            else
            {
                this.buttons_A3.Visible = false;
                this.buttons_A1.Visible = true;
            }

            this.userControl_A1.Size = new Size(this.Width - 2, this.Height - 92);

            this.linkLabel1.Location = new Point(this.linkLabel1.Location.X, this.Height - 18);
        }

        private void buttons_A1_ButtonClick(object sender, EventArgs e)
        {
            //还原
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(900, 700);
            this.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    if (this.WindowState == FormWindowState.Maximized)
                    {

                    }
                    else
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }
                    break;

                case WM_NCLBUTTONDBLCLK://双击
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        //最大化
                        this.WindowState = FormWindowState.Maximized;
                        this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                        this.Invalidate();
                    }
                    else if (this.WindowState == FormWindowState.Maximized)
                    {
                        //还原
                        this.WindowState = FormWindowState.Normal;
                        this.Size = new Size(900, 700);
                        this.Invalidate();
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void ReSetControls()
        {
            this.userControl_A1.Visible = false;
        }

        private void buttons_A5_ButtonClick(object sender, EventArgs e)
        {
            if (this.userControl_A1.Visible == true)
            {
                return;
            }

            this.ReSetControls();
            this.userControl_A1.Visible = true;
        }

        private void buttons_A6_ButtonClick(object sender, EventArgs e)
        {

        }

        private void buttons_A7_ButtonClick(object sender, EventArgs e)
        {

        }

        private void buttons_A8_ButtonClick(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.Links[0].LinkData = "http://www.u3dol.com";
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

    }
}