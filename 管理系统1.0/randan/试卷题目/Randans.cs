using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.IO;
using Common;
using ddss;
using randan.修改题目;

namespace 数据库操作子窗体
{
    public partial class Randans : Form
    {
        static Randans fc = null;
        public static Randans GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new Randans();
            }
            return fc;
        }
        public Randans()
        {
            InitializeComponent();
        }
        string sql = "";
        string currentTime;
        
        private void Button1_Click(object sender, EventArgs e)
        {
            Thread mythread = null;
            mythread = new Thread(new ThreadStart(scpaper));
            mythread.Start();


        }
        ArrayList ea = new ArrayList();
        ArrayList eb = new ArrayList();
        ArrayList ec = new ArrayList();
        public void scpaper()
        {
            int max = Int32.Parse(numericUpDown1.Value.ToString()) + Int32.Parse(numericUpDown2.Value.ToString()) + Int32.Parse(numericUpDown3.Value.ToString()) + Int32.Parse(numericUpDown4.Value.ToString()) + Int32.Parse(numericUpDown5.Value.ToString());
            progressBar1.Maximum =max;//设置最大长度值
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            int ppnum = 0;
            string opxxid="";
            bool ifcunzai = true;
            string xxid;
            string id = GetRnd(4, true, true, true, false, "");
            if (dataGridView1.Rows.Count - 1 != 0)
            {

                while (ifcunzai)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (id == dataGridView1.Rows[i].Cells[0].Value.ToString())
                        {
                            id = GetRnd(10, true, true, true, false, "");
                            break;

                        }
                        else
                        {
                            ifcunzai = false;
                        }
                    }

                }

            }
            currentTime = DateTime.Now.ToShortDateString();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (textBox1.Text != "")
            {
                sql = "insert into ExaminationPaper(ExaminationPaperID,ExaminationPaperDateTime,ExaminationPaperName,ExaminationPaperDescription) values('" + id + "','" + currentTime + "','" + textBox1.Text + "','" + textBox2.Text + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                xxid = id + GetRnd(6, true, true, true, false, "");
                bool quanju = true;

                if (numericUpDown1.Value != 0)
                {
                    //单选题抽题
                    ArrayList opnum = new ArrayList();
                    sql = "select ExaminationID from Examination where ExaminationType=1";
                    cmd.CommandText = sql;
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        opnum.Add(read["ExaminationID"]);
                    }
                    read.Close();

                    if (opnum.Count < numericUpDown1.Value)
                    {
                        MessageBox.Show("单选题数量超出题库内数量！");
                        quanju = false;
                    }
                    else
                    {
                        Random rd = new Random();
                        bool go = true;
                        ArrayList papernum = new ArrayList();
                        int x = rd.Next(0, opnum.Count);
                        for (int i = 0; i < numericUpDown1.Value; i++)
                        {

                            if (papernum.Count != 0)
                            {

                                while (go)
                                {
                                    bool exit = true;
                                    x = rd.Next(0, opnum.Count);
                                    for (int z = 0; z < papernum.Count; z++)
                                    {
                                        if (papernum[z].ToString() == x.ToString())
                                        {
                                            exit = false;
                                            break;
                                        }
                                    }
                                    if (exit == true)
                                    {
                                        papernum.Add(x);
                                        go = false;
                                    }

                                }

                            }
                            else
                            {
                                papernum.Add(x);
                            }
                            go = true;
                        }



                        for (int j = 0; j < papernum.Count; j++)
                        {
                            int a = Int32.Parse(papernum[j].ToString());
                            string tigan = "";
                            string jiexi = "";
                            int nandu = 1;
                            string zhishidian = "";
                            string type = "";
                            sql = "select ExaminationContent from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                tigan = read["ExaminationContent"].ToString();
                            }
                            read.Close();

                            sql = "select Analysis from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                jiexi = read["Analysis"].ToString();
                            }
                            read.Close();

                            sql = "select Difficulty from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                nandu = Int32.Parse(read["Difficulty"].ToString());
                            }
                            read.Close();

                            sql = "select ExaminationPoint from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {

                                zhishidian = read["ExaminationPoint"].ToString();
                            }
                            read.Close();



                            int b = Int32.Parse(opnum[a].ToString());
                            xxid = id + GetRnd(6, true, true, true, false, "");
                            ppnum++;
                            float scc = 1;
                            int ttt = 1;
                            sql = "insert into ExaminationPaperDetailed(ExaminationPaperDetailedID,ExaminationPaperID,ExaminationIndex,Score,ExaminationContent,Analysis,Difficulty,ExaminationPoint,ExaminationType) values('" + xxid + "','" + id + "'," + ppnum + "," + scc + ",'" + tigan + "','" + jiexi + "','" + nandu + "','" + zhishidian + "'," + ttt + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            progressBar1.Value += progressBar1.Step;

                            ea = new ArrayList();
                            eb = new ArrayList();
                            ec = new ArrayList();

                            sql = "select * from OptionList where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                ea.Add(read["OptionContent"]);
                                eb.Add(read["Correct"]);
                            }
                            read.Close();

                            sql = "select ExaminationOptionDetailedID from ExaminationOptionDetailed";
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            if (read.FieldCount > 1)
                            {

                                while (read.Read())
                                {
                                    ec.Add(read["ExaminationOptionDetailedID"]);
                                }
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    ifcunzai = true;
                                    while (ifcunzai)
                                    {
                                        int cz = 0;
                                        for (int i = 0; i < ec.Count; i++)
                                        {

                                            if (opxxid == ec[i].ToString())
                                            {
                                                cz++;
                                                opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                                break;
                                            }

                                        }
                                        if (cz == 0)
                                        {
                                            ifcunzai = false;
                                        }

                                    }
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                read.Close();
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                                read.Close();
                            }
                        }
                    }



                }


                if (numericUpDown2.Value != 0)
                {
                    //多选题抽题
                    ArrayList opnum = new ArrayList();
                    sql = "select ExaminationID from Examination where ExaminationType=2";
                    cmd.CommandText = sql;
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        opnum.Add(read["ExaminationID"]);
                    }
                    read.Close();


                    if (opnum.Count < numericUpDown2.Value)
                    {
                        MessageBox.Show("多选题数量超出题库内数量！");
                        quanju = false;
                    }
                    else
                    {
                        Random rd = new Random();
                        bool go = true;
                        ArrayList papernum = new ArrayList();
                        int x = rd.Next(0, opnum.Count);
                        for (int i = 0; i < numericUpDown2.Value; i++)
                        {

                            if (papernum.Count != 0)
                            {

                                while (go)
                                {
                                    bool exit = true;
                                    x = rd.Next(0, opnum.Count);
                                    for (int z = 0; z < papernum.Count; z++)
                                    {
                                        if (papernum[z].ToString() == x.ToString())
                                        {
                                            exit = false;
                                            break;
                                        }
                                    }
                                    if (exit == true)
                                    {
                                        papernum.Add(x);
                                        go = false;
                                    }

                                }

                            }
                            else
                            {
                                papernum.Add(x);
                            }
                            go = true;
                        }



                        for (int j = 0; j < papernum.Count; j++)
                        {
                            int a = Int32.Parse(papernum[j].ToString());
                            string tigan = "";
                            string jiexi = "";
                            int nandu = 1;
                            string zhishidian = "";
                            sql = "select ExaminationContent from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                tigan = read["ExaminationContent"].ToString();
                            }
                            read.Close();

                            sql = "select Analysis from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                jiexi = read["Analysis"].ToString();
                            }
                            read.Close();

                            sql = "select Difficulty from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                nandu = Int32.Parse(read["Difficulty"].ToString());
                            }
                            read.Close();

                            sql = "select ExaminationPoint from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {

                                zhishidian = read["ExaminationPoint"].ToString();
                            }
                            read.Close();
                            int b = Int32.Parse(opnum[a].ToString());
                            xxid = id + GetRnd(6, true, true, true, false, "");
                            ppnum++;

                            float scc = 1;
                            int ttt = 2;
                            sql = "insert into ExaminationPaperDetailed(ExaminationPaperDetailedID,ExaminationPaperID,ExaminationIndex,Score,ExaminationContent,Analysis,Difficulty,ExaminationPoint,ExaminationType) values('" + xxid + "','" + id + "'," + ppnum + "," + scc + ",'" + tigan + "','" + jiexi + "','" + nandu + "','" + zhishidian + "'," + ttt + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            progressBar1.Value += progressBar1.Step;

                            ea = new ArrayList();
                            eb = new ArrayList();
                            ec = new ArrayList();

                            sql = "select * from OptionList where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                ea.Add(read["OptionContent"]);
                                eb.Add(read["Correct"]);
                            }
                            read.Close();

                            sql = "select ExaminationOptionDetailedID from ExaminationOptionDetailed";
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            if (read.FieldCount > 1)
                            {

                                while (read.Read())
                                {
                                    ec.Add(read["ExaminationOptionDetailedID"]);
                                }
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    ifcunzai = true;
                                    while (ifcunzai)
                                    {
                                        int cz = 0;
                                        for (int i = 0; i < ec.Count; i++)
                                        {

                                            if (opxxid == ec[i].ToString())
                                            {
                                                cz++;
                                                opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                                break;
                                            }

                                        }
                                        if (cz == 0)
                                        {
                                            ifcunzai = false;
                                        }

                                    }
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                read.Close();
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                                read.Close();
                            }
                        }
                    }



                }


                if (numericUpDown3.Value != 0)
                {
                    //多选题抽题
                    ArrayList opnum = new ArrayList();
                    sql = "select ExaminationID from Examination where ExaminationType=3";
                    cmd.CommandText = sql;
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        opnum.Add(read["ExaminationID"]);
                    }
                    read.Close();

                    if (opnum.Count < numericUpDown3.Value)
                    {
                        MessageBox.Show("判断题数量超出题库内数量！");
                        quanju = false;
                    }
                    else
                    {
                        Random rd = new Random();
                        bool go = true;
                        ArrayList papernum = new ArrayList();
                        int x = rd.Next(0, opnum.Count);
                        for (int i = 0; i < numericUpDown3.Value; i++)
                        {

                            if (papernum.Count != 0)
                            {

                                while (go)
                                {
                                    bool exit = true;
                                    x = rd.Next(0, opnum.Count);
                                    for (int z = 0; z < papernum.Count; z++)
                                    {
                                        if (papernum[z].ToString() == x.ToString())
                                        {
                                            exit = false;
                                            break;
                                        }
                                    }
                                    if (exit == true)
                                    {
                                        papernum.Add(x);
                                        go = false;
                                    }

                                }

                            }
                            else
                            {
                                papernum.Add(x);
                            }
                            go = true;
                        }



                        for (int j = 0; j < papernum.Count; j++)
                        {
                            int a = Int32.Parse(papernum[j].ToString());
                            string tigan = "";
                            string jiexi = "";
                            int nandu = 1;
                            string zhishidian = "";
                            sql = "select ExaminationContent from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                tigan = read["ExaminationContent"].ToString();
                            }
                            read.Close();

                            sql = "select Analysis from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                jiexi = read["Analysis"].ToString();
                            }
                            read.Close();

                            sql = "select Difficulty from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                nandu = Int32.Parse(read["Difficulty"].ToString());
                            }
                            read.Close();

                            sql = "select ExaminationPoint from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {

                                zhishidian = read["ExaminationPoint"].ToString();
                            }
                            read.Close();
                            int b = Int32.Parse(opnum[a].ToString());
                            xxid = id + GetRnd(6, true, true, true, false, "");
                            ppnum++;

                            float scc = 1;
                            int ttt = 3;
                            sql = "insert into ExaminationPaperDetailed(ExaminationPaperDetailedID,ExaminationPaperID,ExaminationIndex,Score,ExaminationContent,Analysis,Difficulty,ExaminationPoint,ExaminationType) values('" + xxid + "','" + id + "'," + ppnum + "," + scc + ",'" + tigan + "','" + jiexi + "','" + nandu + "','" + zhishidian + "'," + ttt + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            progressBar1.Value += progressBar1.Step;
                            ea = new ArrayList();
                            eb = new ArrayList();
                            ec = new ArrayList();

                            sql = "select * from OptionList where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                ea.Add(read["OptionContent"]);
                                eb.Add(read["Correct"]);
                            }
                            read.Close();

                            sql = "select ExaminationOptionDetailedID from ExaminationOptionDetailed";
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            if (read.FieldCount > 1)
                            {

                                while (read.Read())
                                {
                                    ec.Add(read["ExaminationOptionDetailedID"]);
                                }
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    ifcunzai = true;
                                    while (ifcunzai)
                                    {
                                        int cz = 0;
                                        for (int i = 0; i < ec.Count; i++)
                                        {

                                            if (opxxid == ec[i].ToString())
                                            {
                                                cz++;
                                                opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                                break;
                                            }

                                        }
                                        if (cz == 0)
                                        {
                                            ifcunzai = false;
                                        }

                                    }
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                read.Close();
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                                read.Close();
                            }
                        }
                    }





                }
                string zhishidian1 = "";
                if (numericUpDown4.Value != 0)
                {
                    //操作题抽题
                    ArrayList opnum = new ArrayList();
                    sql = "select ExaminationID from Examination where ExaminationType=4";
                    cmd.CommandText = sql;
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        opnum.Add(read["ExaminationID"]);
                    }
                    read.Close();
                    if (opnum.Count < numericUpDown4.Value)
                    {
                        MessageBox.Show("操作题数量超出题库内数量！");
                        quanju = false;
                    }
                    else
                    {
                        Random rd = new Random();
                        bool go = true;
                        ArrayList papernum = new ArrayList();
                        int x = rd.Next(0, opnum.Count);
                        for (int i = 0; i < numericUpDown4.Value; i++)
                        {

                            if (papernum.Count != 0)
                            {

                                while (go)
                                {
                                    bool exit = true;
                                    x = rd.Next(0, opnum.Count);
                                    for (int z = 0; z < papernum.Count; z++)
                                    {
                                        if (papernum[z].ToString() == x.ToString())
                                        {
                                            exit = false;
                                            break;
                                        }
                                    }
                                    if (exit == true)
                                    {
                                        papernum.Add(x);
                                        go = false;
                                    }

                                }

                            }
                            else
                            {
                                papernum.Add(x);
                            }
                            go = true;
                        }



                        for (int j = 0; j < papernum.Count; j++)
                        {
                            int a = Int32.Parse(papernum[j].ToString());
                            string tigan = "";
                            string jiexi = "";
                            int nandu = 1;
                            
                            sql = "select ExaminationContent from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                tigan = read["ExaminationContent"].ToString();
                            }
                            read.Close();

                            sql = "select Analysis from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                jiexi = read["Analysis"].ToString();
                            }
                            read.Close();

                            sql = "select Difficulty from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                nandu = Int32.Parse(read["Difficulty"].ToString());
                            }
                            read.Close();
                            string zhishidian = "";
                            sql = "select ExaminationPoint from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {

                                zhishidian = read["ExaminationPoint"].ToString();
                                zhishidian1 = zhishidian;
                            }
                            read.Close();
                            int b = Int32.Parse(opnum[a].ToString());
                            xxid = id + GetRnd(6, true, true, true, false, "");
                            ppnum++;

                            float scc = 1;
                            int ttt = 4;
                            sql = "insert into ExaminationPaperDetailed(ExaminationPaperDetailedID,ExaminationPaperID,ExaminationIndex,Score,ExaminationContent,Analysis,Difficulty,ExaminationPoint,ExaminationType) values('" + xxid + "','" + id + "'," + ppnum + "," + scc + ",'" + tigan + "','" + jiexi + "','" + nandu + "','" + zhishidian + "'," + ttt + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            progressBar1.Value += progressBar1.Step;
                            ea = new ArrayList();
                            eb = new ArrayList();
                            ec = new ArrayList();

                            sql = "select * from OptionList where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                ea.Add(read["OptionContent"]);
                                eb.Add(read["Correct"]);
                            }
                            read.Close();

                            sql = "select ExaminationOptionDetailedID from ExaminationOptionDetailed";
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            if (read.FieldCount > 1)
                            {

                                while (read.Read())
                                {
                                    ec.Add(read["ExaminationOptionDetailedID"]);
                                }
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    ifcunzai = true;
                                    while (ifcunzai)
                                    {
                                        int cz = 0;
                                        for (int i = 0; i < ec.Count; i++)
                                        {

                                            if (opxxid == ec[i].ToString())
                                            {
                                                cz++;
                                                opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                                break;
                                            }

                                        }
                                        if (cz == 0)
                                        {
                                            ifcunzai = false;
                                        }

                                    }
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                read.Close();
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                                read.Close();
                            }
                        }
                        

                    }
                }

                if (numericUpDown5.Value != 0)
                {
                    //装配理论题抽题
                    ArrayList opnum = new ArrayList();
                    sql = "select ExaminationID from Examination where ExaminationType=5 and ExaminationPoint like '%"+zhishidian1+"%'";
                    cmd.CommandText = sql;
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        opnum.Add(read["ExaminationID"]);
                    }
                    read.Close();
                    if (opnum.Count < numericUpDown5.Value)
                    {
                        MessageBox.Show("装配理论题数量超出题库内数量！");
                        quanju = false;
                    }
                    else
                    {
                        Random rd = new Random();
                        bool go = true;
                        ArrayList papernum = new ArrayList();
                        int x = rd.Next(0, opnum.Count);
                        for (int i = 0; i < numericUpDown5.Value; i++)
                        {

                            if (papernum.Count != 0)
                            {

                                while (go)
                                {
                                    bool exit = true;
                                    x = rd.Next(0, opnum.Count);
                                    for (int z = 0; z < papernum.Count; z++)
                                    {
                                        if (papernum[z].ToString() == x.ToString())
                                        {
                                            exit = false;
                                            break;
                                        }
                                    }
                                    if (exit == true)
                                    {
                                        papernum.Add(x);
                                        go = false;
                                    }

                                }

                            }
                            else
                            {
                                papernum.Add(x);
                            }
                            go = true;
                        }



                        for (int j = 0; j < papernum.Count; j++)
                        {
                            int a = Int32.Parse(papernum[j].ToString());
                            string tigan = "";
                            string jiexi = "";
                            int nandu = 1;
                            sql = "select ExaminationContent from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                tigan = read["ExaminationContent"].ToString();
                            }
                            read.Close();

                            sql = "select Analysis from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                jiexi = read["Analysis"].ToString();
                            }
                            read.Close();

                            sql = "select Difficulty from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                nandu = Int32.Parse(read["Difficulty"].ToString());
                            }
                            read.Close();

                            string zhishidian = "";
                            sql = "select ExaminationPoint from Examination where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {

                                zhishidian = read["ExaminationPoint"].ToString();
                            }
                            read.Close();
                            int b = Int32.Parse(opnum[a].ToString());
                            xxid = id + GetRnd(6, true, true, true, false, "");
                            ppnum++;

                            float scc = 1;
                            int ttt = 5;
                            sql = "insert into ExaminationPaperDetailed(ExaminationPaperDetailedID,ExaminationPaperID,ExaminationIndex,Score,ExaminationContent,Analysis,Difficulty,ExaminationPoint,ExaminationType) values('" + xxid + "','" + id + "'," + ppnum + "," + scc + ",'" + tigan + "','" + jiexi + "','" + nandu + "','" + zhishidian + "'," + ttt + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            progressBar1.Value += progressBar1.Step;
                            ea = new ArrayList();
                            eb = new ArrayList();
                            ec = new ArrayList();

                            sql = "select * from OptionList where ExaminationID=" + opnum[a];
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                ea.Add(read["OptionContent"]);
                                eb.Add(read["Correct"]);
                            }
                            read.Close();

                            sql = "select ExaminationOptionDetailedID from ExaminationOptionDetailed";
                            cmd.CommandText = sql;
                            read = cmd.ExecuteReader();
                            if (read.FieldCount > 1)
                            {

                                while (read.Read())
                                {
                                    ec.Add(read["ExaminationOptionDetailedID"]);
                                }
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    ifcunzai = true;
                                    while (ifcunzai)
                                    {
                                        int cz = 0;
                                        for (int i = 0; i < ec.Count; i++)
                                        {

                                            if (opxxid == ec[i].ToString())
                                            {
                                                cz++;
                                                opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                                break;
                                            }

                                        }
                                        if (cz == 0)
                                        {
                                            ifcunzai = false;
                                        }

                                    }
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                read.Close();
                                for (int q = 0; q < ea.Count; q++)
                                {
                                    opxxid = xxid + GetRnd(5, true, true, true, false, "");
                                    int c = 0;
                                    if (eb[q].ToString() == "true" || eb[q].ToString() == "True")
                                    {
                                        c = 1;
                                    }
                                    sql = "insert into ExaminationOptionDetailed(ExaminationOptionDetailedID,ExaminationPaperDetailedID,OptionContent,Correct) values('" + opxxid + "','" + xxid + "','" + ea[q] + "'," + c + ")";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                                read.Close();
                            }
                        }


                    }
                }



                if (quanju)
                {
                    MessageBox.Show("生成试卷成功！");
                }
                else
                {
                    MessageBox.Show("请删除刚才所生成试卷！");
                }
                


            }
            else
            {
                MessageBox.Show("试卷名称不能为空白！");
            }
            con.Close();
            Randans_Load(null, null);
            progressBar1.Value = 0;
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
        public string constr = "";
        private void Randans_Load(object sender, EventArgs e)
        {
            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            sr2.Close();
            try
            {
                this.WindowState = FormWindowState.Maximized;
                dataGridView1.DataSource = dataSet11.ExaminationPaper;
                this.examinationPaperTableAdapter1.Connection = new SqlConnection(constr);
                this.examinationPaperTableAdapter1.Fill(this.dataSet11.ExaminationPaper);

            }
            catch
            {
                MessageBox.Show("该表不存在");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    examinationPaperTableAdapter1.Connection = new SqlConnection(constr);
                    examinationPaperTableAdapter1.Update(dataSet11.ExaminationPaper);
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

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                string num = dataGridView1.Rows[i].Cells[0].Value.ToString();
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;



                ArrayList nunm = new ArrayList();

                sql = "select ExaminationPaperDetailedID from ExaminationPaperDetailed where ExaminationPaperID ='" + num + "'";

                cmd.CommandText = sql;
                SqlDataReader read = cmd.ExecuteReader();
                string dtnum = "";
                while (read.Read())
                {
                    string a = read["ExaminationPaperDetailedID"].ToString();
                    nunm.Add(a);
                }
                read.Close();




                sql = "delete from ExaminationPaper where ExaminationPaperID='" + num + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "delete from ExaminationPaperDetailed where ExaminationPaperID='" + num + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();



                cmd = new SqlCommand();
                cmd.Connection = con;
                for (int j = 0; j < nunm.Count; j++)
                {
                    sql = "delete from ExaminationOptionDetailed where ExaminationPaperDetailedID like '" + nunm[j].ToString() + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                Randans_Load(null, null);
                MessageBox.Show("删除试卷成功！");
                
            }
            catch
            {
                MessageBox.Show("删除试卷失败！");
            }
            

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SelectPaper selectPaper = new SelectPaper();
            selectPaper.Show();

            this.Close();

        }
    }
}
