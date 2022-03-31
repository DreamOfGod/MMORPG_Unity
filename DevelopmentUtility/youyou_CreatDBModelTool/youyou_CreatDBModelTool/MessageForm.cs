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
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
        }

        public MessageForm(string strMessage)
        {
            InitializeComponent();

            this.label1.Text = strMessage;
        }

        /// <summary>
        /// ��Χ�ü�
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
                //new Point(this.Width-1, this.Height-3),
                //new Point(this.Width-2, this.Height-2),
                //new Point(this.Width-3, this.Height-1),
                //new Point(this.Width-4, this.Height-0),

                //new Point(4, this.Height-0),
                //new Point(3, this.Height-1),
                //new Point(2, this.Height-2),
                //new Point(1, this.Height-3),
                new Point(0, this.Height-4),

                new Point(0, 4),
                new Point(1, 3),
                new Point(2, 2),
                new Point(3, 1),

                new Point(4, 0)
            };
            shape.AddPolygon(p);

            //���������ʾ������ΪGraphicsPath��ʵ��   
            this.Region = new System.Drawing.Region(shape);

            shape.Dispose();
        }

        /// <summary>
        /// ���Ʊ�����
        /// </summary>
        /// <param name="g"></param>
        private void DrawTop(Graphics g)
        {
            //���Ϸ�����
            Pen myPen = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(210)))), ((int)(((byte)(223))))));
            g.DrawLine(myPen, new Point(0, 1), new Point(this.Width - 1, 1));
            myPen.Dispose();

            //���Ϸ�������
            LinearGradientBrush myBrush = new LinearGradientBrush(new Point(0, 2), new Point(0, 24), ColorTranslator.FromHtml("#6995C7"), ColorTranslator.FromHtml("#CEDEF2"));
            g.FillRectangle(myBrush, 0, 2, this.Width - 1, 22);
            myBrush.Dispose();
        }

        /// <summary>
        /// �����м�
        /// </summary>
        /// <param name="g"></param>
        private void DrawMiddel(Graphics g)
        {
            //�ײ�����
            Pen myPenA = new Pen(ColorTranslator.FromHtml("#889AB1"));
            g.DrawLine(myPenA, new Point(1, 24), new Point(349, 24));
            myPenA.Dispose();

            //�м�ײ��Ľ���
            Rectangle myRect = new Rectangle(1, 25, 348, 152);
            LinearGradientBrush myBrushD = new LinearGradientBrush(myRect, ColorTranslator.FromHtml("#F1F5FA"), ColorTranslator.FromHtml("#D7E7F7"), LinearGradientMode.Vertical);
            g.FillRectangle(myBrushD, myRect);
            myBrushD.Dispose();
        }

        /// <summary>
        /// ���ƴ���߿�
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
                //new Point(this.Width-2, this.Height-4),
                //new Point(this.Width-3, this.Height-3),
                //new Point(this.Width-4, this.Height-2),
                //new Point(this.Width-5, this.Height-1),

                //new Point(4, this.Height-1),
                //new Point(3, this.Height-2),
                //new Point(2, this.Height-3),
                //new Point(1, this.Height-4),
                new Point(0, this.Height-5),

                new Point(0, 4),
                new Point(1, 3),
                new Point(2, 2),
                new Point(3, 1),

                new Point(4, 0)
            };
            shape.AddPolygon(p);
            g.DrawPath(new Pen(ColorTranslator.FromHtml("#465870"), 1), shape);

            shape.Dispose();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="g"></param>
        private void DrawString(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            SolidBrush myBrush = new SolidBrush(ColorTranslator.FromHtml("#2A436A"));
            g.DrawString("��ʾ - ���ο���", new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134))), myBrush, new PointF(25, 5));
            myBrush.Dispose();
        }

        private void MessageForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            this.DrawRegion();

            this.DrawTop(g);

            this.DrawMiddel(g);

            //this.DrawBottom(g);

            this.DrawFrame(g);

            this.DrawString(g);

            g.Dispose();
        }

        private void buttons_A1_ButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}