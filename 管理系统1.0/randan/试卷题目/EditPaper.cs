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

namespace randan.修改题目
{
    public partial class EditPaper : Form
    {
        private string connstr;
        int max = 0;
        static float Score;                                       //分数
        static string ExaminationContent;                         //题干
        static string Analysis1;                                   //解析
        static int Difficulty;                                    //难度
        static string ExaminationPoint;                           //知识点
        static int ExaminationType;
        static String ExaminationPaperDetailedID;                 //试题详细编号
        static String ExaminationPaperID;                         //试卷编号
        static int ExaminationItemIndex;                          //原始序号
        static string PaperTime;                                  //试卷生成时间
       
        static string examinationPaperDetailedID;


        public EditPaper()
        {
            InitializeComponent();
        }

        private void EditPaper_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"./constr.txt");
            connstr = Common.EncryptCode.DecryptStr(sr.ReadToEnd());
            sr.Close();
            label2.Text = SelectPaper.selectPaper + "中的题目";
            //examTypeBindingSource.DataSource = dataSet11.ExamType;
            try
            {

                

                //DataSet dataSet = new DataSet();
                this.examinationTableAdapter1.Connection = new SqlConnection(connstr);
                dataGridView1.DataSource = dataSet11.Examination;
                this.examinationTableAdapter1.Fill(this.dataSet11.Examination);

                this.examinationPaperDetailedTableAdapter1.Connection = new SqlConnection(connstr);
                examinationPaperDetailedTableAdapter1.FillBy(dataSet11.ExaminationPaperDetailed, SelectPaper.selectPaper);
                dataGridView2.DataSource = dataSet11.ExaminationPaperDetailed;

                this.examTypeTableAdapter1.Connection= new SqlConnection(connstr);
                examTypeTableAdapter1.Fill(this.dataSet11.ExamType);
            }
            catch
            {
                MessageBox.Show("出现错误");
            }

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int type = Int32.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            double score = 0;
            switch (type)
            {
                case 1: score = 0.2;break;
                case 2: score = 0.2;break;
                case 3: score = 0.2;break;
                case 4:score = 12;break;
                case 5:score = 1;break;
            }

            try
            {
                examinationPaperDetailedID = SelectPaper.selectPaper + GetRnd(6, true, true, true, false, "");
                ExaminationPaperID = SelectPaper.selectPaper;

                ExaminationContent = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Analysis1 = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                Difficulty = Int32.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                ExaminationPoint = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                ExaminationType = Int32.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());














                int count = dataGridView2.Rows.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    if (max < Int32.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString()))
                    {
                        max = Int32.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                    }
                }


                //dataGridView2.Rows.Add();

                //dataGridView2.Rows[index].Cells[0].Value = ExaminationPaperDetailedID;
                //dataGridView2.Rows[index].Cells[1].Value = ExaminationPaperID;
                //dataGridView2.Rows[index].Cells[2].Value = ExaminationItemIndex;
                //dataGridView2.Rows[index].Cells[3].Value = Score;
                //dataGridView2.Rows[index].Cells[4].Value = ExaminationContent;
                //dataGridView2.Rows[index].Cells[5].Value = Analysis1;
                //dataGridView2.Rows[index].Cells[6].Value = Difficulty;
                //dataGridView2.Rows[index].Cells[7].Value = ExaminationPoint;
                //dataGridView2.Rows[index].Cells[8].Value = ExaminationType;
                //dataGridView2.Rows[index].Cells[9].Value = dataGridView1.CurrentRow.Cells[0].Value.ToString();


                SqlConnection con = new SqlConnection(connstr);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                string sql = "insert into ExaminationPaperDetailed(ExaminationPaperDetailedID,ExaminationPaperID,ExaminationIndex,Score,ExaminationContent,Analysis,Difficulty,ExaminationPoint,ExaminationType) values('" + examinationPaperDetailedID + "','" + ExaminationPaperID + "','" + max + "','" + score + "','" + ExaminationContent + "','" + Analysis1 + "','" + Difficulty + "','" + ExaminationPoint + "','" + ExaminationType + "'" + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();


                ddss.DataSet1 dataSet = new ddss.DataSet1();
                this.examinationPaperDetailedTableAdapter1.Connection = new SqlConnection(connstr);
                examinationPaperDetailedTableAdapter1.FillBy(dataSet.ExaminationPaperDetailed, SelectPaper.selectPaper);
                dataGridView2.DataSource = dataSet.ExaminationPaperDetailed;
                max++;
            }
            catch
            {
                MessageBox.Show("添加失败，请重试");
            }
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.RowCount > 1)
                {
                    SqlConnection con = new SqlConnection(connstr);
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    //MessageBox.Show(dataGridView2.CurrentRow.Cells[0]);
                    string sql = "delete from ExaminationPaperDetailed where ExaminationPaperDetailedID=  " + "'" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    dataGridView2.Rows.Remove(dataGridView2.CurrentRow);

                }
            }
            catch
            {
                MessageBox.Show("删除失败，请重试");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connstr);
            try
            {

                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    examinationPaperDetailedTableAdapter1.Connection = new SqlConnection(connstr);
                    examinationPaperDetailedTableAdapter1.Update(dataSet11.ExaminationPaperDetailed);

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
    }
}
