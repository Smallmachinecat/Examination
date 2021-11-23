using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 数据库操作子窗体 
{
    public partial class PaperNameForm : Form
    {
       public static String PaperName;
       public static String PaperExplain;
        public static String PaperID;
        public PaperNameForm()
        {
            InitializeComponent();
        }
        //----------------------------------------------------------------------------
        static PaperNameForm fc = null; //创建一个静态对象
        public static PaperNameForm GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new PaperNameForm();
            }
            return fc;
        }
        //----------------------------------------------------------------------------
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                StreamReader sr1 = new StreamReader(@"./constr.txt");

                string constr = Common.EncryptCode.DecryptStr(sr1.ReadToEnd());
               
                SqlConnection con = new SqlConnection(constr);
                sr1.Close();
                //--------------------------------------------------------------------------------------------------测试是否连接通
                try
                {
                    con.Open();
                }
                catch
                {
                    MessageBox.Show("数据库连接失败2");
                }
                //--------------------------------------------------------------------------------------------------测试是否连接通
            
                if (con.State == ConnectionState.Open)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    String insertsql = "insert into ExaminationPaper values('" +
                           PaperID + "','" +
                           DateTime.Now.Date.ToString() + "','" +
                           textBox1.Text + "','" +
                           textBox2.Text + "')";

                    cmd.CommandText = insertsql;
                    cmd.ExecuteNonQuery();
                }

                PaperName = textBox1.Text;
                Hand_Movement hand_Movement = Hand_Movement.GetWindow();
                hand_Movement.MdiParent = ActiveForm;
                hand_Movement.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入试卷名称");
            }
        }

        private void PaperNameForm_Load(object sender, EventArgs e)
        {
            PaperID = GetRnd(4, true, true, true, false, "");

            StreamReader sr1 = new StreamReader(@"./constr.txt");
            string constr = Common.EncryptCode.DecryptStr(sr1.ReadToEnd());
            SqlConnection con1 = new SqlConnection(constr);
           
            try
            {


                sr1.Close();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con1;
                con1.Open();
              

                String insertsql1 = "delete from ExaminationPaper where ExaminationPaperID='a'";
                cmd.CommandText = insertsql1;
                cmd.ExecuteNonQuery();
             
            }
            catch
            {
                MessageBox.Show("该表不存在");
            }


            }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<param name="length">目标字符串的长度</param>
                ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
                ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
                ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
                ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
                ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
                ///<returns>指定长度的随机字符串</returns>
        public string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
