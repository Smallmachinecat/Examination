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
using System.Collections;
using System.Threading;
using System.IO;
using Common;

namespace 数据库操作子窗体
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }
        string sql = "";
        public string constr = "";
        private void Form2_Load(object sender, EventArgs e)
        {
            
            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            sr2.Close();
            this.WindowState = FormWindowState.Maximized;
            SqlConnection con1 = new SqlConnection(constr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con1;
                con1.Open();
                String insertsql1 = "delete from ExaminationVenueOrder where ExamineeID='a'";
                cmd.CommandText = insertsql1;
                cmd.ExecuteNonQuery();
                con1.Close();
            }
            catch
            {
                MessageBox.Show("该表不存在");
            }

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        static Form2 fc = null;
        public static Form2 GetWindow()
        {
            if (fc == null || fc.IsDisposed)
            {
                fc = new Form2();
            }
            return fc;
        }
        Thread mythread = null;
        private void Button1_Click(object sender, EventArgs e)
        {
            mythread = new Thread(new ThreadStart(kcmethod));
            mythread.Start();



        }
        public void kcmethod()
        {
            ArrayList opnum = new ArrayList();

            ArrayList nu = new ArrayList();
            int kcnum;
            ArrayList ite = new ArrayList();
            ArrayList papeiid = new ArrayList();
            ArrayList paperidb = new ArrayList();
            ArrayList errorpaper = new ArrayList();




            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //判断试卷是否存在于数据库中
            sql = "select ExaminationPaperID from ExaminationPaper";
            cmd.CommandText = sql;
            SqlDataReader read;
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                papeiid.Add(read["ExaminationPaperID"]);
            }
            read.Close();

            sql = "select ExaminationPaperID from ExaminationVenue";
            cmd.CommandText = sql;
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                paperidb.Add(read["ExaminationPaperID"]);
            }
            read.Close();






            for (int i = 0; i < paperidb.Count; i++)
            {
                bool presence = false;
                for (int j = 0; j < papeiid.Count; j++)
                {
                    if (papeiid[j].ToString() == paperidb[i].ToString())
                    {
                        presence = true;
                    }
                }
                if (presence == false)
                {
                    errorpaper.Add(paperidb[i]);
                }
            }


            if (errorpaper.Count != 0)
            {
                string error = "";
                foreach (string a in errorpaper)
                {
                    error += a + " ";
                }
                MessageBox.Show("试卷编号：" + error + "不存在，请删除对应的考试场次后再生成秩序！");
                return;
            }
            else
            {
                sql = "select ExaminationVenueID from ExaminationVenue ORDER BY ExaminationVenueName";
                cmd.CommandText = sql;
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ite.Add(read["ExaminationVenueID"]);
                }
                read.Close();
                button1.Enabled = false;
                cmd = new SqlCommand();
                cmd.Connection = con;
                sql = "select ExaminationRoomID from ExaminationRoom";
                cmd.CommandText = sql;
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    opnum.Add(read["ExaminationRoomID"]);
                }
                read.Close();
                kcnum = opnum.Count;



                progressBar1.Maximum = opnum.Count;
                progressBar1.Value = 0;
                progressBar1.Step = 1;


                for (int i = 0; i < opnum.Count; i++)
                {
                    ArrayList st = new ArrayList();
                    sql = "select ExamineeID from Examinee where ExaminationRoomID like '" + opnum[i].ToString() + "%'";
                    cmd.CommandText = sql;
                    read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        st.Add(read["ExamineeID"]);
                    }
                    read.Close();
                    int nummachine = 0;

                    sql = "select NumberMachines from ExaminationRoom where ExaminationRoomID='" + opnum[i].ToString() + "'";
                    cmd.CommandText = sql;
                    read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        nummachine = int.Parse(read["NumberMachines"].ToString());
                    }
                    read.Close();



                    int mainkcnum = nummachine * ite.Count;
                    if (mainkcnum < st.Count)
                    {
                        MessageBox.Show("考场号：" + opnum[i].ToString() + "人员总数超出可安排范围！请清除秩序册！");
                        progressBar1.Value = 0;
                        button1.Enabled = true;
                        Button2_Click(null, null);
                        return;
                    }
                    else
                    {
                        
                        for (int j = 0; j < ite.Count; j++)
                        {
                            
                            string tpnum = "";
                            sql = "select ExaminationPaperID from ExaminationVenue where ExaminationVenueID='" + ite[j].ToString() + "'";
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                tpnum = read["ExaminationPaperID"].ToString();
                            }

                            read.Close();


                            bool stcount = true;
                            int seat = 0;
                            Random r = new Random();
                            int pd = 0;
                            while (stcount)
                            {
                                
                                string itemms = "";
                                bool cc = true;

                                Random rd2 = new Random();
                                ArrayList ppnum = new ArrayList();
                                sql = "select ExaminationIndex from ExaminationPaperDetailed where ExaminationPaperID='" + tpnum + "'";
                                cmd.CommandText = sql;

                                read = cmd.ExecuteReader();
                                while (read.Read())
                                {
                                    ppnum.Add(read["ExaminationIndex"]);
                                }
                                read.Close();

                                if (st.Count != 0)
                                {
                                    int pds = 0;
                                    int xxxx = 0;
                                    while (cc)
                                    {

                                        if (ppnum.Count != 0)
                                        {

                                            int jh = rd2.Next(0, ppnum.Count);
                                            if (xxxx == 0)
                                            {
                                                if (pd != jh)
                                                {
                                                    if (pds == 0)
                                                    {
                                                        pd = jh;
                                                        pds++;
                                                    }
                                                    itemms += ppnum[jh].ToString() + "|";
                                                    ppnum.RemoveAt(jh);
                                                    xxxx++;
                                                }
                                            }
                                            else
                                            {
                                                itemms += ppnum[jh].ToString() + "|";
                                                ppnum.RemoveAt(jh);
                                            }

                                        }
                                        else
                                        {
                                            cc = false;
                                        }

                                    }

                                    int stnum = r.Next(0, st.Count);
                                    seat++;
                                    if (seat <= nummachine)
                                    {
                                        string istunum = st[stnum].ToString();
                                        //MessageBox.Show(istunum + "  " + st.Count.ToString() + "  " + stnum.ToString());
                                        st.RemoveAt(stnum);
                                        try
                                        {
                                            string checkcode = Hash_MD5_32(istunum + ite[j].ToString() + itemms, false);
                                            
                                            sql = "insert into ExaminationVenueOrder(ExamineeID,ExaminationVenueID,SeatNo,ExaminationRoomID,ExaminationItems,CheckCode) values('" + istunum + "','" + ite[j].ToString() + "','" + seat + "','" + opnum[i].ToString() + "','" + itemms + "','" + checkcode + "')";
                                            cmd.CommandText = sql;
                                            cmd.ExecuteNonQuery();
                                            itemms = "";
                                        }
                                        catch
                                        {
                                            MessageBox.Show("已生成对应的秩序册，请清空秩序册！");
                                            button1.Enabled = true;
                                            progressBar1.Value = 0;
                                            mythread.Suspend();
                                        }

                                    }
                                    else
                                    {
                                        stcount = false;
                                    }
                                }
                                else
                                {
                                    stcount = false;
                                }

                            }




                        }
                    }
                    progressBar1.Value += progressBar1.Step;

                }



                MessageBox.Show("生成成功！");
                con.Close();
                button1.Enabled = true;
                progressBar1.Value = 0;
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




        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            sql = "delete from ExaminationVenueOrder";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("清除成功！");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form test = Application.OpenForms["woc"];  //查找是否打开过about窗体  
            if ((test == null) || (test.IsDisposed)) //如果没有打开过
            {
                ordersystem.Form1 aboutus = new ordersystem.Form1(new SqlConnection(constr));
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
