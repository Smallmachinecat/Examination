using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Aspose.Cells;
using System.IO;
using System.Data.OleDb;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using Common;
using System.Drawing;

namespace 数据库操作子窗体
{
    public partial class Examinee_Manage : Form
    {

        //----------------------------------------------------------------------------
        static Examinee_Manage fc = null; //创建一个静态对象
        public static Examinee_Manage GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new Examinee_Manage();
            }
            return fc;
        }
        //----------------------------------------------------------------------------

        public Examinee_Manage()
        {
            InitializeComponent();
        }
        public string constr = "";
        DataGridViewComboBoxColumn da = new DataGridViewComboBoxColumn();
        private void Examinee_Manage_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Maximized;
            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = sr2.ReadToEnd();
            sr2.Close();
            // TODO: 这行代码将数据加载到表“dataSet1.ExaminationRoom”中。您可以根据需要移动或删除它。
            try
            {
                examinationRoomTableAdapter1.Connection = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
                this.examinationRoomTableAdapter1.Fill(this.dataSet1.ExaminationRoom);
                examineeTableAdapter.Connection = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
                examineeTableAdapter.Fill(dataSet1.Examinee);
            }
            catch
            {
                MessageBox.Show("该表不存在");
            }




        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Thread upd = new Thread(upda);

            upd.Start();
        }
        public void upda()
        {
            int i = dataGridView1.RowCount - 1;
            bool flag = true;
            SqlConnection con = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
            try
            {

                con.Open();
                if (con.State == ConnectionState.Open)
                {

                    


                    for (int j = 0; j < i; j++)
                    {
                        
                        if (!CheckExaminationNumber(dataGridView1.Rows[j].Cells[0].Value.ToString()))
                        {
                            MessageBox.Show("考生ID不合法,行号" + (j + 1));
                            flag = false;
                            break;
                        }

                        if (checkBox1.Checked)
                        {
                            if (!CheckIDCard18(dataGridView1.Rows[j].Cells[2].Value.ToString()))
                            {
                                MessageBox.Show("身份证不合法,行号" + (j + 1));
                                flag = false;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        examineeTableAdapter.Connection = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
                        examineeTableAdapter.Update(dataSet1.Examinee);
                        MessageBox.Show("更新成功");
                    }
                    else
                    {
                        MessageBox.Show("数据错误");
                    }


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

        private void 考场ID1BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void BindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            String tx1 = "%" + textBox1.Text + "%";
            String tx2 = "%" + textBox2.Text + "%";
            String tx3 = "%" + textBox3.Text + "%";
            String tx4 = "%" + textBox4.Text + "%";
            if (tx1 == "") tx1 = "%%";
            if (tx2 == "") tx2 = "%%";
            if (tx3 == "") tx3 = "%%";
            if (tx4 == "") tx4 = "%%";
            dataSet1.Examinee.Clear();
            //examineeTableAdapter.FillBy(dataSet1.Examinee,tx1,tx2,tx3,tx4);  
            examineeTableAdapter.Connection = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
            examineeTableAdapter.FillBy1(dataSet1.Examinee, tx1, tx2, tx3, tx4);
            dataGridView1.DataSource = dataSet1.Examinee;


        }

        private void Button4_Click(object sender, EventArgs e)
        {

            examineeTableAdapter.Connection = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
            examineeTableAdapter.Fill(dataSet1.Examinee);
            dataGridView1.DataSource = dataSet1.Examinee;
        }

        private void ExamineeBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            dataSet1.RejectChanges();
            dataGridView1.DataSource = dataSet1.Examinee;
        }

        private void Button6_Click(object sender, EventArgs e)
        {

            //获取点击datagridview1的行的 行号
            int r = this.dataGridView1.CurrentRow.Index;
            //获取此行的 员工编号 的值

            //删除 datagridview1 的选中行
            this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[r]);
            //删除数据库的 员工编号 的对应行


        }
        string file;
        DataTable dt;
        Cells cells = null;
        private void Button7_Click(object sender, EventArgs e)
        {
            if (file == "")
            {
                button7.Enabled = true;
            }
            button7.Enabled = false;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "表格文件(*.xlsx;*.xls)|*.xlsx;*.xls";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
            }
            if (file != ""&&file!=null)
            {
                Thread mythread = null;
                string strlnfo = string.Empty;
                mythread = new Thread(new ParameterizedThreadStart(up));
                mythread.Start(file);
            }
            else
            {
                button7.Enabled = true;
            }
            
            
        }
        public void up(object obj)
        {
            Workbook workbook = new Workbook(obj.ToString());
            cells = workbook.Worksheets[0].Cells;

            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            ArrayList erroridcard = new ArrayList();
            int a = cells.MaxDataRow;
            for (int i = 1; i < a + 1; i++)
            {
                
                if (checkBox1.Checked == true)
                {
                    string idcard = cells[i, 2].StringValue.Trim();
                    if (CheckIDCard18(idcard) == false)
                    {
                        erroridcard.Add(i);
                    }

                }

            }
            if (erroridcard.Count == 0)
            {
                progressBar1.Maximum = a;
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                for (int i = 1; i < a + 1; i++)
                {
                    SqlDataReader read;
                    string sql = "";
                    sql = "select ExaminationRoomID from ExaminationRoom where ExaminationRoom='"+ cells[i, 6].StringValue.Trim() + "'";
                    cmd.CommandText = sql;
                    read = cmd.ExecuteReader();

                    string roomid = "";
                    while (read.Read())
                    {
                        roomid=read["ExaminationRoomID"].ToString();
                    }
                    read.Close();

                    sql = "insert into Examinee(ExamineeID,ExamineeName,CardID,ExaminationRoomID) values ('" + cells[i, 4].StringValue.Trim() + "','" + cells[i, 1].StringValue.Trim() + "','" + cells[i, 2].StringValue.Trim() + "','" + roomid + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    progressBar1.Value += progressBar1.Step;

                }
                MessageBox.Show("导入成功！");
                progressBar1.Value = 0;
               // examinationRoomTableAdapter1.Connection = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
                //this.examinationRoomTableAdapter1.Fill(this.dataSet1.ExaminationRoom);
               // examineeTableAdapter.Connection = new SqlConnection(Common.EncryptCode.DecryptStr(constr));
                examineeTableAdapter.Fill(dataSet1.Examinee);

                button7.Enabled = true;
            }
            else
            {
                string location = "";
                for(int x = 0; x < erroridcard.Count; x++)
                {
                    location += "第" + (int.Parse(erroridcard[x].ToString())+1).ToString() + "行  ";
                }
                MessageBox.Show("身份证格式错误！错误位置:" + location);
            }



        }



        public bool stuno(string no)
        {
            bool c = false;
            if (no.Length == 10)
            {
                string r1 = no.Substring(0, 4);
                for (int i = 0; i < dataSet1.ExaminationRoom.Rows.Count; i++)
                {
                    c = false;
                    if (r1 == dataSet1.ExaminationRoom.Rows[i][0].ToString())
                    {
                        c = true;
                        break;
                    }
                    else
                    {
                        c = false;
                    }
                }

            }
            else
            {
                c = false;
            }
            return c;
        }
        public bool position(string p)
        {
            bool c = false;
            for (int i = 0; i < dataSet1.ExaminationRoom.Rows.Count; i++)
            {
                if (p == dataSet1.ExaminationRoom.Rows[i][1].ToString())
                {
                    c = true;
                }

            }
            return c;
        }



        public static Object method1(string excelPath)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Workbook workBook = new Workbook(excelPath);
            Worksheet workSheet = workBook.Worksheets[0];
            Cells cell = workSheet.Cells;
            int count = cell.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string str = cell.GetRow(0)[i].StringValue;
                dt.Columns.Add(new DataColumn(str));
            }
            for (int i = 1; i < cell.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < count; j++)
                {
                    dr[j] = cell[i, j].StringValue;
                }
                dt.Rows.Add(dr);
                MessageBox.Show(dr[i].ToString());
            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);
            return ds;
        }
        public static Object ReadExcelData(string path)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            //验证license
            //Aspose.Cells.License li = new Aspose.Cells.License();
            //li.SetLicense("Aspose.Cells.lic");
            Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(path);
            Worksheet ws = wk.Worksheets["Sheet1"];
            dt = ws.Cells.ExportDataTable(0, 0, ws.Cells.MaxDataRow + 1, ws.Cells.MaxDataColumn + 1);
            Cells cell = ws.Cells;
            int count = cell.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                string str = cell.GetRow(0)[i].StringValue;
                dt.Columns.Add(new DataColumn(str));
            }
            for (int k = 1; k <= dt.Rows.Count; k++)
            {

                String id = dt.Rows[k][0].ToString();
                String name = dt.Rows[k][1].ToString();
                String idn = dt.Rows[k][2].ToString();
                String info = dt.Rows[k][3].ToString();
                String exrm = dt.Rows[k][4].ToString();
                dr[k] = (id + name + idn + info + exrm);
                MessageBox.Show($"code={id},subDept={name},dept={idn},userName={info},gender={exrm}");
                dt.Rows.Add(dr);

            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);
            return ds;
        }
        private DataTable GetTableFromExcel(string sheetName, string filePath)
        {
            const string connStrTemplate = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;\"";
            DataTable dt = null;
            if (!System.IO.File.Exists(filePath))
            {
                // don't find file
                return null;
            }
            OleDbConnection conn = new OleDbConnection(string.Format(connStrTemplate, filePath));
            try
            {
                conn.Open();
                if (sheetName == null || sheetName.Trim().Length == 0)
                {
                    DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
                }
                else
                {
                    sheetName += "$";
                }

                string strSQL = "Select * From [" + sheetName + "]";
                OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return dt;
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        public static bool CheckIDCard18(string Id)
        {
            long n = 0;
            try
            {
                if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证
                }
            }
            catch
            {
                return false;
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        public  bool CheckExaminationNumber(String Number)
        {
            bool a = false;
            if (Number.Length != 10)
            {
                return false;
            }


            ddss.DataSet1.ExaminationRoomDataTable dt = new ddss.DataSet1.ExaminationRoomDataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from ExaminationRoom", Common.EncryptCode.DecryptStr(constr).ToString());

            sqlDataAdapter.Fill(dt);
            String[] ExamRoomarr = new String[dt.Rows.Count];


            for (int i = 0; i < ExamRoomarr.Length; i++)
            {
                ExamRoomarr[i] = dt.Rows[i][0].ToString();
            }
            for (int i = 0; i < ExamRoomarr.Length; i++)
            {
                if (Number.Substring(0, 4) == ExamRoomarr[i])
                {
                    a = true;
                }
            }

            return a;
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }
    }



}
