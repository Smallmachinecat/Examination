using Aspose.Cells;
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



namespace 数据库操作子窗体
{
    public partial class ExaminationPaper : Form
    {
        static int index1 = 0;
        static int count = 0;
        static int value = 0;
        public string constr = "";
        int str2 = 0;
        public ExaminationPaper()
        {
            InitializeComponent();
        }
        //----------------------------------------------------------------------------
        static ExaminationPaper fc = null; //创建一个静态对象
        public static ExaminationPaper GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new ExaminationPaper();
            }
            return fc;
        }
        //----------------------------------------------------------------------------
        private void ExaminationPaper_Load(object sender, EventArgs e)
        {

            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd()); 
            sr2.Close();

            try
            {
                this.WindowState = FormWindowState.Maximized;
                // TODO: 这行代码将数据加载到表“dataSet11.ExamType”中。您可以根据需要移动或删除它。
                examTypeTableAdapter.Connection = new SqlConnection(constr);
                this.examinationTableAdapter.Connection = new SqlConnection(constr);
                this.examTypeTableAdapter.Fill(this.dataSet11.ExamType);
                // TODO: 这行代码将数据加载到表“dataSet11.ExamType”中。您可以根据需要移动或删除它。
                this.examTypeTableAdapter.Fill(this.dataSet11.ExamType);

                // TODO: 这行代码将数据加载到表“dataSet1.OptionList”中。您可以根据需要移动或删除它。
                // this.optionListTableAdapter.Fill(this.dataSet1.OptionList);
                // TODO: 这行代码将数据加载到表“dataSet1.Examination”中。您可以根据需要移动或删除它。
                
                this.examinationTableAdapter.Fill(this.dataSet1.Examination);
                // TODO: 这行代码将数据加载到表“dataSet11.OptionList”中。您可以根据需要移动或删除它。
            }
            catch
            {
                MessageBox.Show("该表不存在");
            }

            if (dataGridView1.Rows[0].Cells[0].Value != null)
            {
                String strSwap = dataGridView1.Rows[0].Cells[0].Value.ToString();
                int str = Convert.ToInt32(strSwap);

                dataSet1.OptionList.Clear();
                optionListTableAdapter.Connection = new SqlConnection(constr);
                optionListTableAdapter.FillBy(dataSet1.OptionList, str);
                dataGridView2.DataSource = dataSet1.OptionList;





                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    if ((bool)dataGridView2.Rows[i].Cells[3].Value == true)
                    {
                        count++;
                    }

                }
            }



            //DataGridViewComboBoxColumn difficulty = new DataGridViewComboBoxColumn();

            //difficulty.DisplayMember ="1";
            //difficulty.ValueMember = "1";

            //dataGridView1.Columns.Add(difficulty);



            timer1.Start();




        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {



            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {

                    string strSwap = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    int str = Convert.ToInt32(strSwap);

                    dataSet1.OptionList.Clear();
                    optionListTableAdapter.Connection = new SqlConnection(constr);
                    optionListTableAdapter.FillBy(dataSet1.OptionList, str);
                    dataGridView2.DataSource = dataSet1.OptionList;
                    break;
                }
            }

            //int i = Int32.Parse(dataGridView1.CurrentCell.RowIndex.ToString());
            //int k = dataGridView2.RowCount;
            //if (i < k)
            //{
            //    for (int j = 0; j < dataGridView2.Rows.Count; j++)
            //    {
            //        dataGridView2.Rows[j].Selected = false;
            //    }
            //    dataGridView2.Rows[i].Selected = true;
            //}

            //((DataSet1.ExaminationRow)dataGridView2.Rows[0].DataBoundItem).ExaminationPoint = "tyi";


        }

        private void DataGridView1_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Thread mythread = null;
            string strlnfo = string.Empty;
            mythread = new Thread(up);
            mythread.Start();
        }
        public void up()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    examinationTableAdapter.Connection = new SqlConnection(constr);
                    examinationTableAdapter.Update(dataSet1.Examination);
                    optionListTableAdapter.Connection = new SqlConnection(constr);
                    optionListTableAdapter.Update(dataSet1.OptionList);
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

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dataGridView2.CurrentRow.Cells[1].Value = str;




        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = dataGridView1.CurrentRow.Index;
            string strSwap = dataGridView1.Rows[a].Cells[0].Value.ToString();
            int str = Convert.ToInt32(strSwap);
            dataSet1.OptionList.Clear();
            optionListTableAdapter.Connection = new SqlConnection(constr);
            optionListTableAdapter.FillBy(dataSet1.OptionList, str);
            dataGridView2.DataSource = dataSet1.OptionList;
            index1 = dataGridView1.CurrentRow.Index;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        Cells cells = null;
        string files = "";
        private void Button3_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            try
            {
                if (files == "")
                {
                    button3.Enabled = true;
                }
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;//该值确定是否可以选择多个文件
                dialog.Title = "请选择文件";
                dialog.Filter = "表格文件(*.xlsx)|*.xlsx";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    files = dialog.FileName;
                }

                if (files != "")
                {
                    button3.Enabled = false;
                }


                Thread mythread = null;
                string strlnfo = string.Empty;
                mythread = new Thread(new ParameterizedThreadStart(progressvalue));
                mythread.Start(files);



            }
            catch
            {

            }

        }

        public void progressvalue(object ob)
        {

            string file = ob.ToString();
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            try
            {
                Workbook workbook = new Workbook(file);
                cells = workbook.Worksheets[0].Cells;
                progressBar1.Maximum = cells.MaxDataRow;//设置最大长度值
                progressBar1.Value = 0;
                progressBar1.Step = 1;

                ArrayList id = new ArrayList();
                for (int i = 1; i <= cells.MaxDataRow; i++)//读取行
                {

                    int aa = 0;
                    //-----------------------------------------------------------------------------试题表属性
                    String point = "";
                    int ExType = 1;
                    String ExContent = "";
                    bool IsVisible = true;
                    //-----------------------------------------------------------------------------选项表属性
                    String OptionA = "";
                    String OptionB = "";
                    String OptionC = "";
                    String OptionD = "";
                    String OptionE = "";
                    String OptionF = "";
                    String Answer = "";

                    point = cells[i, 1].StringValue.Trim();
                    //-----------------------------------------------------------------------------把试题类型转化为int
                    switch (cells[i, 2].StringValue.Trim())
                    {
                        case "单选题": ExType = 1; break;
                        case "多选题": ExType = 2; break;
                        case "判断题": ExType = 3; break;
                        case "操作题": ExType = 4; break;
                        case "装配理论题": ExType = 5; break;
                    }
                    //-----------------------------------------------------------------------------读取excel表，并赋值给选项表属性
                    ExContent = cells[i, 3].StringValue.Trim();

                    OptionA = cells[i, 4].StringValue.Trim();
                    OptionB = cells[i, 5].StringValue.Trim();
                    OptionC = cells[i, 6].StringValue.Trim();
                    OptionD = cells[i, 7].StringValue.Trim();
                    OptionE = cells[i, 8].StringValue.Trim();
                    OptionF = cells[i, 9].StringValue.Trim();
                    //---------------------------------------------------------------------------------------！！！！！！！！！！！！在此插入试题表数据

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    string sql;
                    if (cells[i, 0].StringValue.Trim() == "" && cells[i, 1].StringValue.Trim() == "" && cells[i, 2].StringValue.Trim() == "" && cells[i, 3].StringValue.Trim() == "" && cells[i, 4].StringValue.Trim() == "")
                    {
                        continue;
                    }
                    else
                    {
                        sql = "insert into Examination(ExaminationContent,Analysis,Difficulty,ExaminationPoint,ExaminationType,IsVaild) values ('" + cells[i, 3].StringValue.Trim() + "'," + "''," + 1 + ",'" + cells[i, 1].StringValue.Trim() + "'," + ExType + "," + 1 + ");";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    sql = "select ExaminationID from Examination where ExaminationContent='" + cells[i, 3].StringValue.Trim() + "';";
                    cmd.CommandText = sql;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        aa = Convert.ToInt32(sdr[0].ToString());
                    }
                    sdr.Close();









                    //---------------------------------------------------------------------------------------！！！！！！！！！！！！


                    Answer = cells[i, 10].StringValue.Trim().ToUpper();//把选项小写转换为大写
                                                                       //-----------------------------------------------------------------------------判断每个选项是否正确
                    string[] shuzu = new string[] { "A", "B", "C", "D", "E", "F" };
                    for (int l = 0; l < 6; l++)
                    {
                        bool IsTrue = false;
                        if (point != "" && ExContent != "")
                        {
                            IsTrue = false;
                            for (int t = 0; t < Answer.Length; t++)
                            {
                                if (shuzu[l] == Answer.Substring(t, 1))
                                {
                                    IsTrue = true;
                                    break;
                                }
                            }
                            //-------------------------------------------------------------！！！！！！！！！！！！在此插入数据库选项表六条信
                        }

                        if (cells[i, l + 4].StringValue.Trim() != "")
                        {
                            int a;
                            if (IsTrue == true)
                            {
                                a = 1;
                            }
                            else
                            {
                                a = 0;
                            }
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            sql = "insert into OptionList(ExaminationID,OptionContent,Correct) values(" + aa + ",'" + cells[i, l + 4].StringValue.Trim() + "'," + a + ");";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                            //-------------------------------------------------------------！！！！！！！！！！！！
                        }
                    }
                    progressBar1.Value += progressBar1.Step;
                }
                MessageBox.Show("导入成功!");
                progressBar1.Value = 0;
                button3.Enabled = true;
                files = "";

            }
            catch
            {

            }






        }

        private void Button4_Click(object sender, EventArgs e)
        {

            String ItemNumber = "";
            String Content = "";
            String Analysis = "";
            String Point = "";
            String ExType = "";
            String Difficulty = "";
            int ExType1 = 1;
            ItemNumber = textBox1.Text;
            Content = "%" + textBox2.Text + "%";
            Analysis = "%" + textBox3.Text + "%";
            Point = "%" + textBox5.Text + "%";
            ExType = comboBox1.Text.ToString();

            if (ExType == "单选题")
            {
                ExType1 = 1;
            }
            if (ExType == "多选题")
            {
                ExType1 = 2;
            }
            if (ExType == "判断题")
            {
                ExType1 = 3;
            }
            if (ExType == "操作题")
            {
                ExType1 = 4;
            }
            if (ExType == "装配理论题")
            {
                ExType1 = 5;
            }

            Difficulty = textBox4.Text;
            dataSet1.Clear();
            if (ItemNumber != "")//只填了试题编号                                        ---------------------ok
            {
                examinationTableAdapter.Connection = new SqlConnection(constr);
                examinationTableAdapter.FillBy(dataSet1.Examination, Int32.Parse(ItemNumber), Content, Analysis, Point);
                dataGridView1.DataSource = dataSet1.Examination;

            }
            if (ExType != "" && Difficulty == "")//只填了试题类型                           ---------------------ok
            {
                examinationTableAdapter.Connection = new SqlConnection(constr);
                examinationTableAdapter.FillBy1(dataSet1.Examination, Content, Analysis, Point, ExType1);
                dataGridView1.DataSource = dataSet1.Examination;
            }
            if (ExType == "" && Difficulty != "")//只填了试题类型和知识点              ---------------------ok
            {
                examinationTableAdapter.Connection = new SqlConnection(constr);
                examinationTableAdapter.FillBy2(dataSet1.Examination, Content, Analysis, Point, ExType1);
                dataGridView1.DataSource = dataSet1.Examination;
            }

            if (ExType == "" && Difficulty == "" && ItemNumber == "")//只填了知识点或题干或解析--------------ok
            {
                examinationTableAdapter.Connection = new SqlConnection(constr);
                examinationTableAdapter.FillBy3(dataSet1.Examination, Content, Analysis, Point);
                dataGridView1.DataSource = dataSet1.Examination;
            }
            if (ExType == "" && ItemNumber == "" && Difficulty != "")//只填了难度                               --------------ok
            {
                examinationTableAdapter.Connection = new SqlConnection(constr);
                examinationTableAdapter.FillBy4(dataSet1.Examination, Analysis, Content, Int32.Parse(Difficulty), Point);
                dataGridView1.DataSource = dataSet1.Examination;
            }
            if (ItemNumber == "" && ExType != "" && Difficulty != "")//只填了难度和题目类型          -------------ok
            {
                examinationTableAdapter.Connection = new SqlConnection(constr);
                examinationTableAdapter.FillBy5(dataSet1.Examination, Content, Analysis, Int32.Parse(Difficulty), ExType1);
                dataGridView1.DataSource = dataSet1.Examination;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            examinationTableAdapter.Connection = new SqlConnection(constr);
            examinationTableAdapter.FillBy6(dataSet1.Examination);
            dataGridView1.DataSource = dataSet1.Examination;
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void DataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void DataGridView2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {


            value = Int32.Parse(dataGridView1.Rows[index1].Cells[5].Value.ToString());  //把题目类型存储进内存中


        }

        private void DataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null && (value == 1 || value == 3)&&Getcount()==1)
            {
                for (int j = 0; j < dataGridView2.RowCount - 1; j++)
                {
                    if ((bool)dataGridView2.Rows[j].Cells[3].Value == true&& Getcount() != 1)
                    {
                        dataGridView2.Rows[j].Cells[3].Value = false;
                       
                    }
                }
            }
        }
        public int Getcount()
        {
            for (int j = 0; j < dataGridView2.RowCount - 1; j++)
            {
                if ((bool)dataGridView2.Rows[j].Cells[3].Value == true)
                {
                    dataGridView2.Rows[j].Cells[3].Value = false;
                    count++;
                }
            }
            return count;
        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
