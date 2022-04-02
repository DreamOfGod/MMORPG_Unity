using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace youyou_CreatDBModelTool
{
    public partial class UserControl_A_Left : UserControl
    {
        System.Threading.Thread myThread;

        public UserControl_A_Left()
        {
            InitializeComponent();

            this.txt_IP.Text = Config.DefaultIP;
            this.txt_UID.Text = Config.DefaultUID;
            this.txt_PWD.Text = Config.DefaultPwd;
        }

        /// <summary>
        /// 画矩形边框
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrame(Graphics g)
        {
            Pen myPenA = new Pen(ColorTranslator.FromHtml("#87A6D4"));
            g.DrawRectangle(myPenA, 0, 0, this.Width - 1, this.Height - 1);
            myPenA.Dispose();

            Pen myPenB = new Pen(ColorTranslator.FromHtml("#92A6C1"));
            g.DrawRectangle(myPenB, 2, 98, this.Width - 5, this.Height - 101);
            myPenB.Dispose();

            Pen myPenC = new Pen(ColorTranslator.FromHtml("#92A6C1"));
            g.DrawRectangle(myPenC, 62, 200, 102, 16);
            myPenC.Dispose();

            Pen myPenD = new Pen(ColorTranslator.FromHtml("#92A6C1"));
            g.DrawRectangle(myPenD, 62, 227, 102, 16);
            myPenD.Dispose();

            Pen myPenE = new Pen(ColorTranslator.FromHtml("#92A6C1"));
            g.DrawRectangle(myPenE, 62, 254, 102, 16);
            myPenE.Dispose();
        }

        private void DrawRec(Graphics g)
        {
            SolidBrush myBrush = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));
            g.FillRectangle(myBrush, 3, 99, 184, 493);
            myBrush.Dispose();
        }

        private void UserControl_A_Left_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(ColorTranslator.FromHtml("#F1F5FE"));

            //this.DrawRec(g);

            this.DrawFrame(g);

            g.Dispose();
        }

        private void UserControl_A_Left_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();

            this.treeView1.Size = new Size(184, this.Height - 102);
        }

        private void ReSetButtons()
        {
            this.buttons_B1.IsChecked = false;
            this.buttons_B2.IsChecked = false;
        }

        private void UserControl_A_Left_Load(object sender, EventArgs e)
        {
            this.ReSetButtons();

            this.buttons_B1.IsChecked = true;

            this.treeView1.Visible = false;
        }

        private void buttons_B1_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.buttons_B1.IsChecked = true;

            this.treeView1.Visible = false;

            this.label4.Visible = false;
        }

        private void buttons_B2_ButtonClick(object sender, EventArgs e)
        {
            this.ReSetButtons();
            this.buttons_B2.IsChecked = true;

            this.treeView1.Visible = true;

            this.label4.Visible = true;
        }

        private void buttons_A1_ButtonClick(object sender, EventArgs e)
        {
            //测试连接

            if (this.txt_IP.Text.Trim().Length.Equals(0))
            {
                this.txt_IP.Focus();
                return;
            }

            if (this.txt_UID.Enabled)
            {
                if (this.txt_UID.Text.Trim().Length.Equals(0))
                {
                    this.txt_UID.Focus();
                    return;
                }
            }

            if (this.txt_PWD.Enabled)
            {
                if (this.txt_PWD.Text.Trim().Length.Equals(0))
                {
                    this.txt_PWD.Focus();
                    return;
                }
            }

            if (this.radioButton2.Checked)
            {
                Config.DefaultConn = "server=" + this.txt_IP.Text.Trim() + ";uid=" + this.txt_UID.Text.Trim() + ";pwd=" + this.txt_PWD.Text.Trim() + ";database=master";
            }
            else
            {
                Config.DefaultConn = "Data Source=" + this.txt_IP.Text.Trim() + ";Initial Catalog=master;Integrated Security=True";
            }


            myThread = new System.Threading.Thread(new System.Threading.ThreadStart(ConnectionDataBase));
            myThread.Start();
            this.timer1.Enabled = true;
        }

        delegate void InitcomBoxDataBase(DataTable dt);

        private void Init_comBoxDataBase(DataTable dt)
        {
            this.comBox_DataBase.DataSource = null;
            this.comBox_DataBase.DataSource = dt;
            this.comBox_DataBase.DisplayMember = "name";
            this.comBox_DataBase.ValueMember = "name";
        }

        private void ConnectionDataBase()
        {
            try
            {
                SqlConnection myTestConnection = new SqlConnection(Config.DefaultConn);
                DataTable dt = SqlHelper.ExecuteDataTable(myTestConnection, CommandType.Text, "select [name] from [sysdatabases] order by [name]");

                InitcomBoxDataBase ibd = new InitcomBoxDataBase(Init_comBoxDataBase);
                this.Invoke(ibd, dt);
            }
            catch
            {
                new MessageForm("数据库无法打开").ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (myThread.ThreadState != System.Threading.ThreadState.Stopped)
            { //如果线程运行
                this.progressBars_A1.Visible = true;

            }
            else
            {
                this.progressBars_A1.Visible = false;

            }
        }

        private void buttons_A2_ButtonClick(object sender, EventArgs e)
        {
            //连接

            if (Config.DefaultConn == null)
            {
                new MessageForm("请先连接服务器").ShowDialog();
                return;
            }

            if (this.comBox_DataBase.SelectedValue == null)
            {
                new MessageForm("请先连接服务器").ShowDialog();
                return;
            }

            if (this.radioButton2.Checked)
            {
                Config.CurrentConn = "server=" + this.txt_IP.Text.Trim() + ";uid=" + this.txt_UID.Text.Trim() + ";pwd=" + this.txt_PWD.Text.Trim() + ";database=" + this.comBox_DataBase.SelectedValue.ToString().Trim();
            }
            else
            {
                Config.CurrentConn = "Data Source=" + this.txt_IP.Text.Trim() + ";Initial Catalog=" + this.comBox_DataBase.SelectedValue.ToString() + ";Integrated Security=True";
            }
            SqlConnection myConnection = new SqlConnection(Config.CurrentConn);

            DataTable dt = SqlHelper.ExecuteDataTable(myConnection, CommandType.Text, "select [id], [name] from [sysobjects] where [type] = 'u' order by [name]");

            Config.CurrentDataBaseName = this.comBox_DataBase.SelectedValue.ToString();

            this.treeView1.Nodes.Clear();

            TreeNode _TreeNode = new TreeNode(this.comBox_DataBase.SelectedValue.ToString(), 0, 0);

            foreach (DataRow drw in dt.Rows)
            {
                TreeNode myNode = new TreeNode();
                myNode.Tag = drw["id"];
                myNode.Text = drw["name"].ToString();
                myNode.ImageIndex = 1;
                myNode.SelectedImageIndex = 1;
                _TreeNode.Nodes.Add(myNode);
            }
            this.treeView1.Nodes.Add(_TreeNode);

            this.treeView1.Visible = true;

            this.buttons_B1.IsChecked = false;
            this.buttons_B2.IsChecked = true;

            this.label4.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_UID.Enabled = false;
            this.txt_PWD.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_UID.Enabled = true;
            this.txt_PWD.Enabled = true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                Config.CurrentTableName = this.treeView1.SelectedNode.Text.Trim();

                this.label4.Text = "当前表：" + Config.CurrentTableName;
            }
        }
    }
}
