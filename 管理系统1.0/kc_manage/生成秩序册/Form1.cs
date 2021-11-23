using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Threading;
using System.IO;
using Common;

namespace 数据库操作子窗体
{
    
    public partial class Form1 : Form
    {
        string paperid="";
        int dccc;
        int kg;
        int kslx;
        static Form1 fc = null;
        public static Form1 GetWindow()
        {  
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
        string sql = "";
        int gg = 0;
        ArrayList ite = new ArrayList();
        public string constr = "";
        ArrayList itemindex =new ArrayList();
        ArrayList nullpaper = new ArrayList();
        private void Form1_Load(object sender, EventArgs e)
        {

            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            sr2.Close();

            this.WindowState = FormWindowState.Maximized;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                sql = "select ExaminationPaperID from ExaminationPaper";
                cmd.CommandText = sql;


                if (gg == 0)
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ite.Add(read["ExaminationPaperID"]);
                    }
                    read.Close();
                    for (int i = 0; i < ite.Count; i++)
                    {
                        nullpaper = new ArrayList();
                        int ppinformation = 0;
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        sql = "select ExaminationPaperDetailedID from ExaminationPaperDetailed where ExaminationPaperID='" + ite[i].ToString() + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            nullpaper.Add(read["ExaminationPaperDetailedID"].ToString());
                        }
                        read.Close();
                        
                        
                        if (nullpaper.Count != 0)
                        {
                            comboBox1.Items.Add(ite[i]);
                        }
                        else
                        {
                            comboBox1.Items.Add(ite[i] + "(空试卷)");
                            itemindex.Add(i.ToString());
                        }

                    }
                    gg++;





                }
                con.Close();
                dataGridView1.DataSource = dataSet11.ExaminationVenue; ;
                this.examinationVenueTableAdapter.Connection = new SqlConnection(constr);
                this.examinationVenueTableAdapter.Fill(this.dataSet11.ExaminationVenue);
            }
            catch
            {
                MessageBox.Show("该表不存在");
            }

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool a = true;
            for(int i = 0; i < itemindex.Count; i++)
            {
                if (comboBox1.SelectedIndex.ToString()== itemindex[i].ToString())
                {
                    a = false;
                }
            }
            if (a == true)
            {
                Thread mythread = null;
                mythread = new Thread(new ThreadStart(zsc));
                mythread.Start();
            }
            else
            {
                MessageBox.Show("不能生成空试卷场次！");
            }
            
        }
        public void zsc()
        {
            string ksnum = GetRnd(10, true, true, true, false, "");
            string ksname = textBox2.Text.ToString();
            string dt;
            dt = dateTimePicker1.Value.ToString("yyyy-MM-dd ");

            
            if (textBox1.Text != "")
            {
                dt += " " + textBox1.Text + ":" + textBox3.Text+":00";
            }
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            sql = "insert into ExaminationVenue(ExaminationVenueID,ExaminationVenueName,ExaminationTime,ExaminationPaperID,IsCurExaminationVenue,Password,IsTested,VenueType) values('" + ksnum + "','" + ksname + "','" + dt + "','" + paperid + "'," + dccc + ",'" + Hash_MD5_16(ksnum+textBox6.Text,false) + "'," + kg + "," + kslx + ")";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            Form1_Load(null, null);


        }
        public static string Hash_MD5_16(string word, bool toUpper = true)
        {
            try
            {
                string sHash = Hash_MD5_32(word).Substring(8, 16);
                return toUpper ? sHash : sHash.ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string Hash_MD5_32(string word, bool toUpper = true)
        {
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP
                    = new System.Security.Cryptography.MD5CryptoServiceProvider();

                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(word);
                byte[] bytHash = MD5CSP.ComputeHash(bytValue);
                MD5CSP.Clear();

                //根据计算得到的Hash码翻译为MD5码
                string sHash = "", sTemp = "";
                for (int counter = 0; counter < bytHash.Count(); counter++)
                {
                    long i = bytHash[counter] / 16;
                    if (i > 9)
                    {
                        sTemp = ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp = ((char)(i + 0x30)).ToString();
                    }
                    i = bytHash[counter] % 16;
                    if (i > 9)
                    {
                        sTemp += ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp += ((char)(i + 0x30)).ToString();
                    }
                    sHash += sTemp;
                }

                //根据大小写规则决定返回的字符串
                return toUpper ? sHash : sHash.ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




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

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            paperid = comboBox1.Items[i].ToString();

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox2.SelectedIndex;
            if (i == 0)
            {
                dccc = 1;
            }
            else
            {
                dccc = 0;
            }

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox4.SelectedIndex;
            if (i == 0)
            {
                kg = 1;
            }
            else
            {
                kg = 0;
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            kslx= comboBox3.SelectedIndex;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    examinationVenueTableAdapter.Connection = new SqlConnection(constr);
                    examinationVenueTableAdapter.Update(dataSet11.ExaminationVenue);
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show("数据库连接失败");
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("更新失败");
            }
            finally
            {
                con.Close();
            }
        }

        private void FillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.examinationVenueTableAdapter.FillBy1(this.dataSet11.ExaminationVenue);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
