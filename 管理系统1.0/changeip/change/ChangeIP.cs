using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.NetworkInformation;
using Common;

namespace 数据库操作子窗体
{

    public partial class ChangeIP : Form
    {

        //----------------------------------------------------------------------------
        static ChangeIP fc = null; //创建一个静态对象
        public static ChangeIP GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new ChangeIP();
            }
            return fc;
        }
        //----------------------------------------------------------------------------







        public static string Path1 = "";
        public ChangeIP()
        {
            InitializeComponent();
        }

        private void ChangeIP_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            



            String IP1 = "";
            StreamReader sr = new StreamReader(@"./IP.txt");
            IP1 = sr.ReadToEnd();
            textBox1.Text = Common.EncryptCode.DecryptStr(IP1);
            sr.Close();

            String User = "";
            StreamReader sr1 = new StreamReader(@"./User.txt");
            User = sr1.ReadToEnd();
            textBox2.Text = Common.EncryptCode.DecryptStr(User);
            sr1.Close();

            String Password = "";
            StreamReader sr2 = new StreamReader(@"./Password.txt");
            Password = sr2.ReadToEnd();
            textBox3.Text = Common.EncryptCode.DecryptStr(Password);
            sr2.Close();

            String DuankouNum = "";
            StreamReader sr3 = new StreamReader(@"./DuankouNum.txt");
            DuankouNum = sr3.ReadToEnd();
            textBox4.Text = Common.EncryptCode.DecryptStr(DuankouNum);
            sr3.Close();

            String DataName = "";
            StreamReader sr4 = new StreamReader(@"./DataName.txt");
            DataName = sr4.ReadToEnd();
            textBox5.Text = Common.EncryptCode.DecryptStr(DataName);
            sr4.Close();



        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.Text = "正在连接...";
            Thread thread3 = new Thread(Function2);
            thread3.Start();





        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        private bool IsRightIP(string strLocalIP)
        {
            bool bFlag = false;
            bool Result = true;


            Regex regex = new Regex("^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
            bFlag = regex.IsMatch(strLocalIP);
            if (bFlag == true)
            {
                string[] strTemp = strLocalIP.Split(new char[] { '.' });
                int nDotCount = strTemp.Length - 1; //字符串中.的数量，若.的数量小于3，则是非法的ip地址
                if (3 == nDotCount)//判断 .的数量
                {
                    for (int i = 0; i < strTemp.Length; i++)
                    {
                        if (Convert.ToInt32(strTemp[i]) > 255)  //大于255不符合返回false

             {
                            Result = false;
                        }
                    }
                }
                else
                {
                    Result = false;
                }
            }
            else
            {
                //输入非数字则提示，不符合IP格式
                //MessageBox.Show("不符合IP格式");
                Result = false;
            }
            return Result;
        }

        public void Function()
        {
            String ipstr = "";
            String Userstr = "";
            String Passwordstr = "";
            String DuankouNum = "";
            String DataName = "";
            String connstr = "";
            if (IsRightIP(textBox1.Text)||textBox1.Text==".")
            {
                ipstr = Common.EncryptCode.EncryptStr(textBox1.Text);
                Userstr = Common.EncryptCode.EncryptStr(textBox2.Text);
                Passwordstr = Common.EncryptCode.EncryptStr(textBox3.Text);
                DuankouNum = Common.EncryptCode.EncryptStr(textBox4.Text);
                DataName = Common.EncryptCode.EncryptStr(textBox5.Text);


                connstr = "Data Source = " + textBox1.Text+","+textBox4.Text + "; Initial Catalog =" + textBox5.Text + "; Persist Security Info = True; User ID = " + textBox2.Text + "; Password = " + textBox3.Text + "; Connection Timeout = 1";


                System.IO.File.WriteAllText(@"./IP.txt", ipstr);
                System.IO.File.WriteAllText(@"./User.txt", Userstr);
                System.IO.File.WriteAllText(@"./Password.txt", Passwordstr);
                System.IO.File.WriteAllText(@"./DuankouNum.txt", DuankouNum);
                System.IO.File.WriteAllText(@"./DataName.txt", DataName);
                System.IO.File.WriteAllText(@"./constr.txt", Common.EncryptCode.EncryptStr(connstr));
                MessageBox.Show("已更新！");
                button1.Enabled = true;
                button1.Text = "确定";
                this.Close();

            }
            else
            {
                MessageBox.Show("ip地址不合法");
                button1.Enabled = true;
                button1.Text = "确定";
            }

        }

        public void Function2()
        {
            
            bool online = false; //是否在线
            Ping ping = new Ping();
            if (IsRightIP(textBox1.Text))
            {
                PingReply pingReply = ping.Send(textBox1.Text);
                if (pingReply.Status == IPStatus.Success)
                {
                    online = true;
                    Thread thread = new Thread(Function);
                    thread.Start();

                }
                else
                {
                    MessageBox.Show("无法连接该ip");
                    button1.Enabled = true;
                    button1.Text = "确定";
                }
            }else if (textBox1.Text == ".")
            {
                online = true;
                Thread thread = new Thread(Function);
                thread.Start();
            }
            else
            {
                MessageBox.Show("IP地址不合法！");
                button1.Enabled = true;
                button1.Text = "确定";
            }
            

            ChangeButton();

        }
        public void ChangeButton()
        {
            button1.Enabled = true;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }

    //-----------------------------------------------------------------------------------

}
