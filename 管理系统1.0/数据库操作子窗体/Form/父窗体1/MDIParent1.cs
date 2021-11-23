using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
namespace 数据库操作子窗体
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;


        public static String ip = "";
        public static String User = "";
        public static String Password = "";
        public static String connstr = "";
        public static String DataName = "";
        public static String DuankouNum = "";
        public String Getconnstr()
        {
            return connstr;
        }
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "窗口 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }



        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void 管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exam_room_manage child = Exam_room_manage.GetWindow();//调用方法
            child.MdiParent = this;//设置child的父窗体为当前窗体
            child.BringToFront();
            child.Show();

        }

        public void MDIParent1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CheckForIllegalCrossThreadCalls = false;
            timer1.Start();
            toolStripStatusLabel1.Text = "正在尝试连接...";
            toolStripStatusLabel1.ForeColor = Color.DarkOrange;
            ChangeButton1();

        }
        public void Connection_Condition(ToolStripStatusLabel label, String connstr)//判断是否连接的上数据库，且为参数内的标签复制，返回布尔类型的值
        {
            SqlConnection con = new SqlConnection(connstr);
            ChangeButton1();
            try
            {
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    label.Text = "已连接";
                    label.ForeColor = Color.Green;
                    con.Close();
                    ChangeButton2();

                }
                else
                {
                    label.Text = "未连接";
                    label.ForeColor = Color.Red;
                    ChangeButton1();
                    Changecolor();
                }
            }
            catch
            {
                int i = 0;
                i++;
                label.Text = "未连接";
                ChangeButton1();
                Changecolor();

            }





            con.Close();

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(Load1);
            thread2.Start();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeIP child = ChangeIP.GetWindow();//调用方法
            child.MdiParent = this;
            child.BringToFront();
            child.Show();

        }
        public void Load1()
        {
            StreamReader sr = new StreamReader(@"./IP.txt");
            StreamReader sr1 = new StreamReader(@"./User.txt");
            StreamReader sr2 = new StreamReader(@"./Password.txt");
            StreamReader sr3 = new StreamReader(@"./DuankouNum.txt");
            StreamReader sr4 = new StreamReader(@"./DataName.txt");
            StreamReader sr5 = new StreamReader(@"./constr.txt");
            ip = Common.EncryptCode.DecryptStr(sr.ReadToEnd());
            User = Common.EncryptCode.DecryptStr(sr1.ReadToEnd());
            Password = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            DuankouNum = Common.EncryptCode.DecryptStr(sr3.ReadToEnd());
            DataName = Common.EncryptCode.DecryptStr(sr4.ReadToEnd());
            connstr = Common.EncryptCode.DecryptStr(sr5.ReadToEnd());

            sr.Close();
            sr1.Close();
            sr2.Close();
            sr3.Close();
            sr4.Close();
            sr5.Close();
            Connection_Condition(toolStripStatusLabel1, connstr);
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
        public void click()
        {
            Thread thread1 = new Thread(Load1);

            thread1.Start();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void 管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Examinee_Manage child = Examinee_Manage.GetWindow();//调用方法
            child.MdiParent = this;//设置child的父窗体为当前窗体
            child.BringToFront();
            child.Show();
        }

        private void 考试场次ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 管理ToolStripMenuItem3_Click(object sender, EventArgs e)
        {

            ExaminationPaper child = ExaminationPaper.GetWindow();
            child.MdiParent = this;
            child.BringToFront();
            child.Show();

        }

        private void 考试信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void Changecolor()
        {
            toolStripStatusLabel1.ForeColor = Color.Red;
        }
        public void ChangeButton1()
        {

            数据维护ToolStripMenuItem.Enabled = false;
        }
        public void ChangeButton2()
        {
            系统ToolStripMenuItem.Enabled = true;
            数据维护ToolStripMenuItem.Enabled = true;
        }

        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 管理ToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void 随机出题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Randans child = Randans.GetWindow();
            child.MdiParent = this;
            child.BringToFront();
            child.Show();


        }

        private void 人工出题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaperNameForm paperNameForm = PaperNameForm.GetWindow();
            paperNameForm.MdiParent = this;
            paperNameForm.BringToFront();
            paperNameForm.Show();
        }

        private void 管理ToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.MdiParent = this;
            form1.BringToFront();
            form1.Show();
        }

        private void 生成秩序表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.MdiParent = this;
            form2.BringToFront();
            form2.Show();
        }
        public void method2()
        {
            //////////////////////////////////////////////////////////////////////////////////////判断是否数据库有所有表
            StreamReader sr1 = new StreamReader(@"./constr.txt");
            string constr = Common.EncryptCode.DecryptStr(sr1.ReadToEnd());
            SqlConnection con1 = new SqlConnection(constr);
            int a = 0;
            try
            {


                sr1.Close();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con1;
                con1.Open();
                a++;

                String insertsql1 = "select * from ExaminationRoom";
                cmd.CommandText = insertsql1;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql2 = "select * from Examinee";
                cmd.CommandText = insertsql2;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql3 = "select * from Examination";
                cmd.CommandText = insertsql3;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql4 = "select * from OptionList";
                cmd.CommandText = insertsql4;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql5 = "select * from ExaminationPaper";
                cmd.CommandText = insertsql5;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql6 = "select * from ExaminationPaperDetailed";
                cmd.CommandText = insertsql6;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql7 = "select * from ExaminationOptionDetailed";
                cmd.CommandText = insertsql7;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql8 = "select * from AnswerSheetDetailed";
                cmd.CommandText = insertsql8;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql9 = "select * from ExaminationVenue";
                cmd.CommandText = insertsql9;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql10 = "select * from ExaminationVenueOrder";
                cmd.CommandText = insertsql10;
                cmd.ExecuteNonQuery();
                a++;

                String insertsql11 = "select * from ExamType";
                cmd.CommandText = insertsql11;
                cmd.ExecuteNonQuery();
                a++;
            }
            catch
            {
                string[] tablelist = new string[] { "ExaminationRoom", "Examinee", "Examination", "OptionList", "ExaminationPaper", "ExaminationPaperDetailed", "ExaminationOptionDetailed", "AnswerSheetDetailed", "ExaminationVenue", "ExaminationVenueOrder", "ExamType" };
                MessageBox.Show(tablelist[a] + "表不存在");
            }
            con1.Close();
        }

        private void 离线生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form test = Application.OpenForms["FrmRS2DSExportData"];  //查找是否打开过about窗体  
            if ((test == null) || (test.IsDisposed)) //如果没有打开过
            {
                RS2DSSwapData.FrmRS2DSExportData aboutus = new RS2DSSwapData.FrmRS2DSExportData();
                aboutus.MdiParent = this;
                aboutus.BringToFront();
                aboutus.Show();   //打开子窗体出来
            }
            else
            {
                test.Activate(); //如果已经打开过就让其获得焦点  
                test.WindowState = FormWindowState.Normal;//使Form恢复正常窗体大小
            }


        }

        private void 数据收集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form test = Application.OpenForms["FrmDS2RSImportData"];  //查找是否打开过about窗体  
            if ((test == null) || (test.IsDisposed)) //如果没有打开过
            {
                RS2DSSwapData.FrmDS2RSImportData aboutus = new RS2DSSwapData.FrmDS2RSImportData();
                aboutus.MdiParent = this;
                aboutus.BringToFront();
                aboutus.Show();   //打开子窗体出来
            }
            else
            {
                test.Activate(); //如果已经打开过就让其获得焦点  
                test.WindowState = FormWindowState.Normal;//使Form恢复正常窗体大小
            }
        }

        private void 创建数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //dc.Form1 f1 = dc.Form1.GetWindow();
            //f1.MdiParent = this;
            //f1.BringToFront();
            //f1.Show();
        }

        private void 成绩查询打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form test = Application.OpenForms["Form1"];  //查找是否打开过about窗体  
            if ((test == null) || (test.IsDisposed)) //如果没有打开过
            {
                ExamineSystem.Form1 aboutus = new ExamineSystem.Form1(new SqlConnection(connstr));
                aboutus.MdiParent = this;
                aboutus.BringToFront();
                aboutus.Show();   //打开子窗体出来
            }
            else
            {
                test.Activate(); //如果已经打开过就让其获得焦点  
                test.WindowState = FormWindowState.Normal;//使Form恢复正常窗体大小
            }
        }
    }
}
