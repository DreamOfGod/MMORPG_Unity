using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace youyou_CreatDBModelTool.AttributeBuild
{
    public partial class AttributeBuild : Form
    {
        private const int HTCAPTION = 0x0002;
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3; 

        public AttributeBuild()
        {
            InitializeComponent();
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
            //����
            Pen myPenA = new Pen(ColorTranslator.FromHtml("#889AB1"));
            g.DrawLine(myPenA, new Point(1, 24), new Point(this.Width - 2, 24));
            myPenA.Dispose();

            //�м�ײ��Ľ���
            Rectangle myRect = new Rectangle(1, 25, 798, 470);
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
            g.DrawString("���������� - Mmcoy", new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134))), myBrush, new PointF(25, 5));
            myBrush.Dispose();
        }

        private void AttributeBuild_Paint(object sender, PaintEventArgs e)
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
            if (this.richTextBox1.Text.Trim().Length == 0)
            {
                new MessageForm("������Ҫ���ɵ�����").ShowDialog();
                return;
            }
            try
            {
                this.richTextBox2.Text = "";
                string[] arrList = this.richTextBox1.Text.Split(new Char[] { '\n' });

                for (int i = 0; i < arrList.Length; i++)
                {
                    if (!arrList[i].Trim().Length.Equals(0))
                    {
                        string str = arrList[i];

                        int iPosition1 = str.IndexOf("e ");
                        int iPosition2 = str.IndexOf(" _", iPosition1 + 2);
                        int iLength = str.Length - 1;

                        string str1 = str.Substring(0, iPosition1 + 1);
                        string str2 = str.Substring(iPosition1 + 2, (iPosition2 - (iPosition1 + 2)));
                        string str3 = str.Substring(iPosition2 + 2, (iLength - (iPosition2 + 2)));


                        string str4 = "public " + str2 + " " + str3 + "\n";
                        str4 += "{" + "\n";

                        str4 += "\t" + "get { return _" + str3 + "; }" + "\n";
                        str4 += "\t" + "set { _" + str3 + " = value; }" + "\n";

                        str4 += "}" + "\n";

                        this.richTextBox2.Text += str4;
                    }
                }
            }
            catch
            {
                new MessageForm("ת��ʧ��").ShowDialog();
                return;
            }
        }

        private void AttributeBuild_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "private int _Id;" + "\n";
            this.richTextBox1.Text += "private string _Name;";
        }

        private void buttons_A4_ButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    m.Result = (IntPtr)HTCAPTION;
                    break;

                case WM_NCLBUTTONDBLCLK://˫��

                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}