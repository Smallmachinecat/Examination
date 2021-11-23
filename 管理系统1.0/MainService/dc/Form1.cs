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
using Common;
using System.Data.Sql;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Threading;
namespace dc
{
    public partial class Form1 : Form
    {
        string constr="";
        ArrayList tablename;
        string dataname = "";
        SqlCommand cmd=null;
        SqlConnection con;
        static Form1 fc = null; //创建一个静态对象
        
        public static Form1 GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new Form1();
            }
            return fc;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        string ip="";
        string User = "";
        string Password = "";
        string DuankouNum = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            try
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
                textBox1.Text = ip;
                textBox2.Text = DuankouNum;
                textBox3.Text = User;
                textBox4.Text = Password;
            }
            catch
            {

            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connstr;
            string file = "";
            button1.Text = "正在导入...";
            button1.Enabled = false;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "表格文件(*.sql;)|*.sql;";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
            }
            if (file != "")
            {
                Thread mythread = null;
                string strlnfo = string.Empty;
                
                connstr = "Data Source = " + textBox1.Text + "," + textBox2.Text + "; Persist Security Info = True; User ID = " + textBox3.Text + "; Password = " + textBox4.Text + "; Connection Timeout = 1";
                try
                {
                    SqlConnection con = new SqlConnection(connstr);
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        mythread = new Thread(new ParameterizedThreadStart(InvokeExcute));
                        mythread.Start(@"osql -S " + textBox1.Text + "," + textBox2.Text + " -U " + textBox3.Text + " -P " + textBox4.Text + " -i " + file);
                    }
                }
                catch
                {
                    MessageBox.Show("无法登陆数据库！请检查四项信息！");
                    button1.Enabled = true;
                    button1.Text = "导入全部结构";
                }
                
               
            }
            else
            {
                button1.Text = "导入全部结构";
                button1.Enabled = true;
            }
            

        }
        public void InvokeExcute(object Com)
        {
            string Command = Com.ToString();
            Command = Command.Trim().TrimEnd('&') + "&exit";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序
                          //向cmd窗口写入命令
                p.StandardInput.WriteLine(Command);
                p.StandardInput.AutoFlush = true;
                //获取cmd窗口的输出信息
                StreamReader reader = p.StandardOutput;//截取输出流
                StreamReader error = p.StandardError;//截取错误信息
                string stra = reader.ReadToEnd();
                string strb = error.ReadToEnd();
                string result;
                int a = stra.IndexOf("已存在");
                int b = stra.IndexOf("拒绝访问");
                int c = stra.IndexOf("登陆失败");
                result = "导入成功！";
                if (strb != "")
                {
                    result = "导入失败，请正确填写四项数据！";
                }
                if (a != -1)
                {
                    result = "导入失败，已存在该数据库！";
                }
                if (b != -1)
                {
                    result = "导入失败，无权限添加数据库！";
                }
                if (c != -1)
                {
                    result = "登录失败！请四项信息！";
                }
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                MessageBox.Show(result);

                //调用另一线程
                this.BeginInvoke(new Action(() =>
                {
                    button1.Text = "导入全部结构";
                    button1.Enabled = true;
                }));
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
