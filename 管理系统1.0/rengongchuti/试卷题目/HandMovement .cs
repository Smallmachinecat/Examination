using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 数据库操作子窗体
{
    public partial class Hand_Movement : Form
    {

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
        static string ExaminationOptionDetailedID;                //详细试题选项编号
        static int dgv2;
        static bool [] dt5;
        static  bool[] IsRepetition1;
        static string connstr;
        static bool first1 = false;
        public Hand_Movement()
        {
            InitializeComponent();
        }
        //----------------------------------------------------------------------------
        static Hand_Movement fc = null; //创建一个静态对象
        public static Hand_Movement GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new Hand_Movement();
            }
            return fc;
        }
        //----------------------------------------------------------------------------
        private void Hand_Movement_Load(object sender, EventArgs e)
        {
         
            this.WindowState = FormWindowState.Maximized;


            StreamReader sr = new StreamReader(@"./constr.txt");
            connstr = Common.EncryptCode.DecryptStr(sr.ReadToEnd());


            try
            {

                this.examinationPaperDetailedTableAdapter.Connection = new SqlConnection(connstr);
                this.examinationPaperDetailedTableAdapter.Fill(this.dataSet1.ExaminationPaperDetailed);

                this.examTypeTableAdapter.Connection = new SqlConnection(connstr);
                this.examTypeTableAdapter.Fill(this.dataSet1.ExamType);


                this.examinationTableAdapter.Connection = new SqlConnection(connstr);
                this.examinationTableAdapter.Fill(this.dataSet1.Examination);


                this.examinationOptionDetailedTableAdapter.Connection = new SqlConnection(connstr);
                this.examinationOptionDetailedTableAdapter.Fill(this.dataSet1.ExaminationOptionDetailed);

                this.examinationPaperDetailedTableAdapter.Connection = new SqlConnection(connstr);
                this.examinationPaperDetailedTableAdapter.Fill(this.dataSet11.ExaminationPaperDetailed);

                this.optionListTableAdapter.Connection = new SqlConnection(connstr);
                this.optionListTableAdapter.Fill(this.dataSet1.OptionList);
            }
            catch
            {
                MessageBox.Show("该表不存在");
            }


            optionListTableAdapter.Connection = new SqlConnection(connstr);
            ExaminationPaperID = PaperNameForm.PaperID;
            label3.Text = PaperNameForm.PaperName;
            textBox1.Text = "1";
            ExaminationItemIndex = 1;  //原始序号
            PaperTime = DateTime.Now.ToString();
            label4.Text = PaperNameForm.PaperID;


            dataGridView6.DataSource = null;
            dataGridView7.DataSource = null;


            //-------------------------------------------------------------------------------------------------
            //ExaminationPaperID = GetRnd(4, true, true, true, false, "");//试卷编号
            //if (dataGridView3.Rows.Count - 1 != 0)
            //{

            //    for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            //    {
            //        if (dataGridView3.Rows[i].Cells[0].Value.ToString() == ExaminationPaperID)
            //        {
            //            ExaminationPaperID = GetRnd(4, true, true, true, false, "");
            //        }
            //    }
            //}

            //-------------------------------------------------------------------------------------------------
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            first1 = true;
            dt5 = new bool[dataGridView2.RowCount ];
            ExaminationPaperDetailedID = PaperNameForm.PaperID + GetRnd(6, true, true, true, false, "");//试题详细编号

            //题目类型
            Score = float.Parse(textBox1.Text);


            ExaminationContent = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Analysis1 = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Difficulty = Int32.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            ExaminationPoint = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            ExaminationType = Int32.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());


            int index = dataGridView2.Rows.Count ;
            dataGridView2.Rows.Add();

            dataGridView2.Rows[index].Cells[0].Value = ExaminationPaperDetailedID;
            dataGridView2.Rows[index].Cells[1].Value = ExaminationPaperID;
            dataGridView2.Rows[index].Cells[2].Value = ExaminationItemIndex;
            dataGridView2.Rows[index].Cells[3].Value = Score;
            dataGridView2.Rows[index].Cells[4].Value = ExaminationContent;
            dataGridView2.Rows[index].Cells[5].Value = Analysis1;
            dataGridView2.Rows[index].Cells[6].Value = Difficulty;
            dataGridView2.Rows[index].Cells[7].Value = ExaminationPoint;
            dataGridView2.Rows[index].Cells[8].Value = ExaminationType;
            dataGridView2.Rows[index].Cells[9].Value = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            textBox1.Text = "1";
            
            //--------------------------------------------------------------------------------------------------------------------------------------    
            //--------------------------------------------------------------------------------------------------------------------------------------          
            //--------------------------------------------------------------------------------------------------------------------------------------  

            //IsRepetition1 = new bool[dataGridView2.RowCount ];


            ////////////////////////////////////////////////////////////////存储重复的序列
            //for (int i = 0; i < dataGridView2.RowCount ; i++)
            //{
            //    dataGridView5.Rows.Add();
            //    if (dataGridView5.Rows[i].Cells[0].Value != null)
            //    {
            //        for (int j = 0; j < dataGridView5.RowCount ; j++)
            //        {
            //            if (dataGridView2.Rows[i].Cells[0].Value.ToString() == dataGridView5.Rows[j].Cells[0].Value.ToString())
            //            {
            //              
            //                IsRepetition1[i] = true;
            //                break;
            //            }
            //            else
            //            {
            //                IsRepetition1[i] = false;

            //            }
            //        }
            //    }
            //    else
            //    {
            //        IsRepetition1[i] = false;
            //    }

            //}
            ////////////////////////////////////////////////////////////////存储重复的序列

            //添加试题
            StreamReader sr1 = new StreamReader(@"./constr.txt");

            string constr = Common.EncryptCode.DecryptStr(sr1.ReadToEnd());

           
            

            SqlConnection con = new SqlConnection(constr);
            sr1.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {

               
                con.Open();
            }
            catch
            {
                MessageBox.Show("数据库连接失败");
            }


                String insertsql = "insert into ExaminationPaperDetailed values('" +
                    ExaminationPaperDetailedID + "','" +
                    ExaminationPaperID + "','" +
                    ExaminationItemIndex + "','" +
                    Score + "','" +
                    ExaminationContent + "','" +
                    Analysis1 + "','" +
                    Difficulty + "','" +
                    ExaminationPoint + "','" +
                    ExaminationType + "')";

                cmd.CommandText = insertsql;
                cmd.ExecuteNonQuery();


            
            //--------------------------------------------------------------------------------------------------------------------------------------          
            //添加试题选项
            for (int i = 0; i < dataGridView6.RowCount; i++)
                {
                    
                    ExaminationOptionDetailedID = dataGridView2.CurrentRow.Cells[0].Value.ToString() + GetRnd(6, true, true, true, false, "");//试题选项编号
                    
                    String insertsq3 = "insert into ExaminationOptionDetailed values('" +
                    ExaminationOptionDetailedID + "','" +
                    ExaminationPaperDetailedID + "','" +
                    dataGridView6.Rows[i].Cells[2].Value.ToString() + "','" +
                    (bool)dataGridView6.Rows[i].Cells[3].Value + "')";

                    cmd.CommandText = insertsq3;
                    cmd.ExecuteNonQuery();
                }



                con.Close();

            dataGridView2.Rows[dataGridView2.RowCount - 1].Selected = true;
            //--------------------------------------------------------------------------------------------------------------------------------------          
            //--------------------------------------------------------------------------------------------------------------------------------------          
            //--------------------------------------------------------------------------------------------------------------------------------------  
            for (int i = 0; i < dataGridView2.RowCount; i++)//存储选中的序列
            {
                dataGridView5.Rows.Add();
                dataGridView5.Rows[i].Cells[0].Value = dataGridView2.Rows[i].Cells[0].Value.ToString();
            }
            ExaminationItemIndex++;
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("连接服务器失败");
            }

            try
            {
                String SqlDelete = "delete from ExaminationPaperDetailed where ExaminationPaperDetailedID='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
                cmd.CommandText = SqlDelete;
                cmd.ExecuteNonQuery();

                String SqlOptionDelete = "delete from ExaminationOptionDetailed where ExaminationPaperDetailedID='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
                cmd.CommandText = SqlOptionDelete;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("删除失败");
            }



            try
            {
                dgv2 = dataGridView2.RowCount;
                int r = this.dataGridView2.CurrentRow.Index;
                this.dataGridView2.Rows.Remove(this.dataGridView2.Rows[r]);
            }
            catch
            {
                MessageBox.Show("不可删除空行");
            }
            con.Close();
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)//存储选中的序列
            {

                dataGridView5.Rows[i].Cells[0].Value = dataGridView2.Rows[i].Cells[0].Value.ToString();
            }


        }

        private void Button3_Click(object sender, EventArgs e)
        {

           
           
                Thread thread = new Thread(method);
                thread.Start();
                
            
       
        }
        public void method()
        {
            SqlConnection con = new SqlConnection(connstr);

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


            for (int a = 0; a < 2; a++)
            {
                try
                {

                    if (con.State == ConnectionState.Open)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    IsRepetition1 = new bool[dataGridView2.RowCount ];


                    //////////////////////////////////////////////////////////////存储重复的序列
                    for (int i = 0; i < dataGridView2.RowCount ; i++)
                    {
                        dataGridView5.Rows.Add();
                        if (dataGridView5.Rows[i].Cells[0].Value != null)
                        {
                            for (int j = 0; j < dataGridView5.RowCount ; j++)
                            {
                                if (dataGridView2.Rows[i].Cells[0].Value.ToString() == dataGridView5.Rows[j].Cells[0].Value.ToString())
                                {
                           
                                    IsRepetition1[i] = true;
                                    break;
                                }
                                else
                                {
                                    IsRepetition1[i] = false;

                                }
                            }
                        }
                        else
                        {
                            IsRepetition1[i] = false;
                        }

                    }
                        //////////////////////////////////////////////////////////////存储重复的序列
                        //examinationRoomTableAdapter.Connection = new SqlConnection(MDIParent1.connstr);
                        //examinationRoomTableAdapter.Update(dataSet1.ExaminationRoom);
                        //MessageBox.Show("更新成功");
                        examinationOptionDetailedTableAdapter.Connection = new SqlConnection(connstr);
                        examinationOptionDetailedTableAdapter.Update(dataSet1.ExaminationOptionDetailed);
                        




                    }
                else
                {
                    MessageBox.Show("数据库连接失败1");
                }
                }
                catch
                {
                    MessageBox.Show("插入失败");
                }
              
                for (int i = 0; i < dataGridView2.RowCount ; i++)//存储选中的序列
                {

                    dataGridView5.Rows[i].Cells[0].Value = dataGridView2.Rows[i].Cells[0].Value.ToString();
                }
               
            }

            con.Close();
            MessageBox.Show("更新成功");
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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
        
        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow.Cells[0].Value != null)
            {
                optionListTableAdapter.Connection = new SqlConnection(connstr);
                optionListTableAdapter.FillBy(dataSet1.OptionList, Int32.Parse(dataGridView2.CurrentRow.Cells[9].Value.ToString()));
                dataGridView4.DataSource = dataSet1.OptionList;

                examinationOptionDetailedTableAdapter.Connection = new SqlConnection(connstr);
                examinationOptionDetailedTableAdapter.FillBy(dataSet1.ExaminationOptionDetailed, dataGridView2.CurrentRow.Cells[0].Value.ToString());
                dataGridView7.DataSource = dataSet1.ExaminationOptionDetailed;
                

            }
          
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ddss.DataSet1 dataSet = new ddss.DataSet1();
            optionListTableAdapter.Connection = new SqlConnection(connstr);
            optionListTableAdapter.FillBy(dataSet.OptionList, Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            dataGridView6.DataSource = dataSet.OptionList;
        }

        private void DataGridView2_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView7_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            String ExaminationPaperDetailedID1 = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            String ExaminationOptionDetailedID1 = ExaminationPaperDetailedID1 + GetRnd(6, true, true, true, false, "");
            dataGridView7.CurrentRow.Cells[0].Value = ExaminationOptionDetailedID1;
            dataGridView7.CurrentRow.Cells[1].Value = ExaminationPaperDetailedID1;
        }

        private void DataGridView7_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void DataGridView7_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

        }

        private void DataGridView7_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void DataGridView7_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            
        }

        private void DataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (first1)
            {

                int count = 0;
                if (Int32.Parse(dataGridView2.CurrentRow.Cells[8].Value.ToString()) == 1 || Int32.Parse(dataGridView2.CurrentRow.Cells[8].Value.ToString()) == 3)
                {
                    for (int i = 0; i < dataGridView7.RowCount - 1; i++)
                    {
                        if ((bool)dataGridView7.Rows[i].Cells[3].Value == true)
                        {
                            count++;
                        }
                    }
                    if (count > 1)
                    {
                        MessageBox.Show("该题型只能有一个正确答案");
                        dataGridView7.CurrentRow.Cells[3].Value = false;
                        
                    }
                }
            }
        }

        private void DataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (first1)
            {
                int value =Int32.Parse( dataGridView2.CurrentRow.Cells[8].Value.ToString());
                if (dataGridView7.CurrentRow != null && (value == 1 || value == 3) && Getcount() == 1)
                {
                    for (int j = 0; j < dataGridView7.RowCount - 1; j++)
                    {
                        if ((bool)dataGridView7.Rows[j].Cells[3].Value == true && Getcount() != 1)
                        {
                            dataGridView7.Rows[j].Cells[3].Value = false;

                        }
                    }
                }
            }
        }
        public int Getcount()
        {
            int count = 0;
            for (int j = 0; j < dataGridView7.RowCount - 1; j++)
            {
                if ((bool)dataGridView7.Rows[j].Cells[3].Value == true)
                {
                    dataGridView7.Rows[j].Cells[3].Value = false;
                    count++;
                }
            }
            return count;
        }



    }
}
